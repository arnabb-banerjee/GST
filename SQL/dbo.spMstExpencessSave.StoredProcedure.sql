USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMstExpencessSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spMstExpencessSave] (
	@InvoiceType				VARCHAR(1) = '',
	@InvoiceDate				VARCHAR(20) = '',
	@InvoiceDueDate				VARCHAR(20) = '',
	@InvoiceID					BIGINT = 0, 
	@InvoiceNumber				VARCHAR(50) = '', 
	@BranchID					BIGINT = 0, 
	@OrganizationCode			VARCHAR(50) = '',
	@SupID						BIGINT = 0, 
	@SupAddress					VARCHAR(500) = '', 
	@SupState					BIGINT = 0,
	@SupCountry					BIGINT = 0,
	@XMLProductData				VARCHAR(max) = '', 
	@Prices						VARCHAR(500) = '', 
	@Taxes						VARCHAR(500) = '', 
	@IsReturned  				VARCHAR(1) = '',
	@IsCancelled  				VARCHAR(1) = '',
	@UserID						bigint = 0,
	@isOnlyDelete				VARCHAR(1) = '',
	@NewDatauniqueID 			BIGINT OUT,
	@ErrorMessage 				VARCHAR(50) OUT
)
AS 
BEGIN 

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	SELECT  @AllowSaveDeleteData = 'Y'
	DECLARE @idoc int
	DECLARE @IsExists INT
	DECLARE @OrganizationID BIGINT
	SELECT  @OrganizationID = OrganizationID FROM urMstOrganizationMaster WHERE OrganizationCode = @OrganizationCode
	
	select @XMLProductData = replace(@XMLProductData, '[', '<')
	select @XMLProductData = replace(@XMLProductData, ']', '>')
	select @XMLProductData = '<root>' + @XMLProductData + '</root>'

	/*SELECT @XMLProductData ='
			<row>
				<col pid="" price="" qty="", totalamout = "", tax="" />
			</row>'*/



	IF (@isOnlyDelete = 'Y')
	BEGIN 
		SELECT @IsExists = MAX(x) FROM (
			SELECT COUNT(ExpenceID) x FROM trnExpenceHeader WHERE ExpenceID = @InvoiceID
			UNION
			SELECT COUNT(ExpenceID) x FROM trnExpenceHeader WHERE ExpenceID = @InvoiceID
			UNION
			SELECT COUNT(ExpenceID) x FROM trnExpenceHeader WHERE ExpenceNumber = @InvoiceNumber
			UNION
			SELECT COUNT(ExpenceID) x FROM trnExpenceHeader WHERE ExpenceNumber = @InvoiceNumber)A

		IF (@IsExists > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
		IF ((SELECT COUNT(ExpenceID) x FROM trnExpenceHeader WHERE ExpenceNumber = @InvoiceNumber AND ((@InvoiceID < 1) OR (@InvoiceID > 0 AND ExpenceID <> @InvoiceID))
			) > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be saved as the same name is assigned for another record'
		END
	END 
	
	IF (@AllowSaveDeleteData = 'Y')
	BEGIN			
		IF @InvoiceID < 1
		BEGIN 
			SELECT @NewDatauniqueID = 1
			
			INSERT INTO trnExpenceHeader ([DataUniqueID],[ActivityType],[ExpenceNumber],[IsCancelled],[IsReturned],[ReturnedOn]
				,[ExpenceType],[ExpenceDate],[BranchID]
				,[CusID],[CusAddress],[CusState],[CusCountry]
				   
				/*,[TotalAmount],[Discount],[DiscountFormat],[AmountAfterDiscount],[AmountPayable]*/
				,[Duedate],[OrganizationId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
			VALUES(@NewDatauniqueID, 'A', @InvoiceNumber, @IsCancelled, @IsReturned, CASE WHEN (ISNULL(@IsReturned, '') = 'Y') THEN GETDATE() ELSE NULL END
				,@InvoiceType, convert(datetime, @InvoiceDate, 103), @BranchID
				,@SupID, @SupAddress, @SupState, @SupCountry
				,@InvoiceDueDate, @OrganizationID, GETDATE(), @UserID, null, null)

			--Create an internal representation of the XML document.
			EXEC sp_xml_preparedocument @idoc OUTPUT, @XMLProductData

			INSERT INTO [trnExpenceproduct]
					([DataUniqueID],[ProductId],[ProductName],[ServiceGood],[SACHCNCode],
					[PricePerUnit],[Quantity],[TotalAmount],[TaxOnProduct]
					,[CreatedOn],[CreatedBy])
			select @NewDatauniqueID, pid, (select ProductName from prMstproductMaster where ProductId = pid) ProductName
			, (select c.ServiceOrGoods from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) [SACHCNCode]
			, (select case when(c.ServiceOrGoods='G') then c.HSNCode else c.SACCode end from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) ProductName
			, price, qty, totalamout, tax
			, GETDATE(), @UserID

			FROM OPENXML (@idoc, '/row/col',1)
				
			WITH (pid varchar(50), price varchar(50), qty varchar(50), totalamout varchar(50), tax varchar(50))

				 
		END
		ELSE
		BEGIN 
			SELECT @OldDatauniqueID = DatauniqueID FROM trnExpenceHeader WHERE ExpenceID = @InvoiceID
					
			INSERT INTO trnExpenceHeaderAudit ([DataUniqueID],[ActivityType],[ExpenceNumber],[IsCancelled],[IsReturned],[ReturnedOn]
				,[ExpenceType],[ExpenceDate],[BranchID]
				,[CusID],[CusAddress],[CusState],[CusCountry]
				,[TotalAmount],[Discount],[DiscountFormat],[AmountAfterDiscount],[AmountPayable]
				,[Duedate],[OrganizationId],[LastModifiedOn],[LastModifiedBy],[CreatedOn],[CreatedBy], AuditOperationOn, AuditOperationBy)
			SELECT [DataUniqueID],CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END, [ExpenceNumber],[IsCancelled],[IsReturned],[ReturnedOn]
				,[ExpenceType],[ExpenceDate],[BranchID]
				,[CusID],[CusAddress],[CusState],[CusCountry]
				,[TotalAmount],[Discount],[DiscountFormat],[AmountAfterDiscount],[AmountPayable]
				,[Duedate],[OrganizationId],[LastModifiedOn],[LastModifiedBy],[CreatedOn],[CreatedBy], GETDATE(), @UserID 
			FROM trnExpenceHeader
			WHERE ExpenceID = @InvoiceID
				
			delete from [trnExpenceproduct] where ExpenceID = @InvoiceID

			IF @isOnlyDelete <> 'Y'
			BEGIN 
				SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM [trnExpenceproduct] where ExpenceID = @InvoiceID

				--Create an internal representation of the XML document.
				EXEC sp_xml_preparedocument @idoc OUTPUT, @XMLProductData

				INSERT INTO [trnExpenceproduct]
					([DataUniqueID],[ProductId],[ProductName],[ServiceGood],[SACHCNCode]
					,[PricePerUnit],[Quantity],[TotalAmount],[TaxOnProduct]
					,[CreatedOn],[CreatedBy])

				SELECT @NewDatauniqueID, pid, (select ProductName from prMstproductMaster where ProductId = pid) ProductName
				, (select c.ServiceOrGoods from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) ProductName
				, (select case when(c.ServiceOrGoods='G') then c.HSNCode else c.SACCode end from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) ProductName
				, price, qty, totalamout, tax
				, GETDATE(), @UserID

				FROM OPENXML (@idoc, '/row/col',1)
				
				WITH (pid varchar(50), price varchar(50), qty varchar(50), totalamout varchar(50), tax varchar(50))

			END 
			ELSE
				DELETE FROM [trnExpenceproduct] where ExpenceID = @InvoiceID
		END
	END
END




GO
