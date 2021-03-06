USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMstEpenseSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

[dbo].[spMsteXPENSESave] 'B', '25/10/2018', '', '0', '', '', '1', '10042', 'Amar chakraborty road', 41, 101, 
'[row]  [col       pid="11"        qty="12"       price="12"       total="144"       tax="18"       totaltax="162"   /] [/row][row]  [col       pid="15"        qty="18"       price="13"       total="234"       tax="11"       totaltax="245"   /] [/row][row]  [col       pid="19"        qty="23"       price="21"       total="483"       tax="18"       totaltax="501"   /] [/row]'
, '', '', 'Y', 'Y', '', 'N', 0, ''

*/
CREATE PROCEDURE [dbo].[spMstEpenseSave] (
	@InvoiceType				VARCHAR(1) = '',
	@InvoiceDate				VARCHAR(20) = '',
	@InvoiceDueDate				VARCHAR(20) = '',
	@InvoiceID					BIGINT = 0, 
	@InvoiceNumber				VARCHAR(50) = '', 
	@BranchID					BIGINT = 0, 
	@OrganizationCode			VARCHAR(50) = '',
	@CusID						BIGINT = 0, 
	@CusAddress					VARCHAR(500) = '', 
	@CusState					BIGINT = 0,
	@CusCountry					BIGINT = 0,
	@XMLProductData				VARCHAR(max) = '', 
	@Prices						VARCHAR(500) = '', 
	@Taxes						VARCHAR(500) = '', 
	@IsReturned  				VARCHAR(1) = '',
	@IsCancelled  				VARCHAR(1) = '',
	@UserID						bigint = 0,
	@isOnlyDelete				VARCHAR(1) = '',
	@NewDatauniqueID 			BIGINT OUT,
	@ErrorMessage 				VARCHAR(500) OUT
)
AS 
BEGIN 

	DECLARE @NewInvoiceID BIGINT
	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	SELECT  @AllowSaveDeleteData = 'Y'
	DECLARE @idoc int
	DECLARE @IsExists INT
	DECLARE @OrganizationID BIGINT
	SELECT  @OrganizationID = OrganizationID FROM urMstOrganizationMaster WHERE OrganizationCode = @OrganizationCode
	
	IF @InvoiceNumber = ''
	BEGIN
		SELECT @InvoiceNumber = NEWID()
	END

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
			SELECT COUNT(InvoiceID) x FROM trnExpenseHeader WHERE InvoiceID = @InvoiceID OR InvoiceNumber = @InvoiceNumber
			UNION
			SELECT COUNT(InvoiceID) x FROM trnExpenseHeaderAudit WHERE InvoiceID = @InvoiceID OR InvoiceNumber = @InvoiceNumber)A

		IF (@IsExists > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
		IF ((SELECT COUNT(InvoiceID) x FROM trnExpenseHeader WHERE InvoiceNumber = @InvoiceNumber AND ((@InvoiceID < 1) OR (@InvoiceID > 0 AND InvoiceID <> @InvoiceID))
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
			
			INSERT INTO trnExpenseHeader ([DataUniqueID],[ActivityType],[InvoiceNumber],[IsCancelled],[IsReturned],[ReturnedOn]
				,[InvoiceType],[InvoiceDate],[BranchID]
				,[CusID],[CusAddress],[CusState],[CusCountry]
				   
				/*,[TotalAmount],[Discount],[DiscountFormat],[AmountAfterDiscount],[AmountPayable]*/
				,[Duedate],[OrganizationId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
			VALUES(@NewDatauniqueID, 'A', @InvoiceNumber, @IsCancelled, @IsReturned, CASE WHEN (ISNULL(@IsReturned, '') = 'Y') THEN GETDATE() ELSE NULL END
				,@InvoiceType, convert(datetime, @InvoiceDate, 103), @BranchID
				,@CusID, @CusAddress, @CusState, @CusCountry
				,@InvoiceDueDate, @OrganizationID, GETDATE(), @UserID, null, null)

			SELECT @NewInvoiceID = @@IDENTITY
			
			--Create an internal representation of the XML document.
			EXEC sp_xml_preparedocument @idoc OUTPUT, @XMLProductData

			INSERT INTO [trnExpenseproduct]
					(InvoiceID,[DataUniqueID],[ProductId],[ProductName],[ServiceGood],[SACHCNCode],
					[PricePerUnit],[Quantity],[TotalAmount],[TaxOnProduct],[isBreakupNeed]
					,[CreatedOn],[CreatedBy])
			select @NewInvoiceID, @NewDatauniqueID, pid, (select ProductName from prMstproductMaster where ProductId = pid) ProductName
			, (select c.ServiceOrGoods from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) [SACHCNCode]
			, (select case when(c.ServiceOrGoods='G') then c.HSNCode else c.SACCode end from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) ProductName
			, price, qty, total, tax, isBreakupNeed
			, GETDATE(), @UserID

			FROM OPENXML (@idoc, 'root/row/col',1)
				
			WITH (pid varchar(50), price varchar(50), qty varchar(50), total varchar(50), tax varchar(50), isBreakupNeed varchar(1))
				 
		END
		ELSE
		BEGIN 
			SELECT @OldDatauniqueID = DatauniqueID FROM trnExpenseHeader WHERE InvoiceID = @InvoiceID
					
			INSERT INTO trnExpenseHeaderAudit ([DataUniqueID],[ActivityType],[InvoiceNumber],[IsCancelled],[IsReturned],[ReturnedOn]
				,[InvoiceType],[InvoiceDate],[BranchID]
				,[CusID],[CusAddress],[CusState],[CusCountry]
				,[TotalAmount],[Discount],[DiscountFormat],[AmountAfterDiscount],[AmountPayable]
				,[Duedate],[OrganizationId],[LastModifiedOn],[LastModifiedBy],[CreatedOn],[CreatedBy], AuditOperationOn, AuditOperationBy)
			SELECT [DataUniqueID],CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END, [InvoiceNumber],[IsCancelled],[IsReturned],[ReturnedOn]
				,[InvoiceType],[InvoiceDate],[BranchID]
				,[CusID],[CusAddress],[CusState],[CusCountry]
				,[TotalAmount],[Discount],[DiscountFormat],[AmountAfterDiscount],[AmountPayable]
				,[Duedate],[OrganizationId],[LastModifiedOn],[LastModifiedBy],[CreatedOn],[CreatedBy], GETDATE(), @UserID 
			FROM trnExpenseHeader
			WHERE InvoiceID = @InvoiceID

			/*Child and breakup table insert*/
				
			delete from [trnExpenseproduct] where InvoiceID = @InvoiceID
			delete FROM trnExpenseTravelling 
			WHERE InvoiceProductID in (
					select InvoiceProductID from trnExpenseProduct where ExpenseID = @InvoiceID
			);

			IF @isOnlyDelete <> 'Y'
			BEGIN 
				SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM [trnExpenseproduct] where InvoiceID = @InvoiceID

				--Create an internal representation of the XML document.
				EXEC sp_xml_preparedocument @idoc OUTPUT, @XMLProductData

				INSERT INTO [trnExpenseproduct]
					([InvoiceID],[DataUniqueID],[ProductId],[ProductName],[ServiceGood],[SACHCNCode]
					,[PricePerUnit],[Quantity],[TotalAmount],[TaxOnProduct]
					,[CreatedOn],[CreatedBy])

				SELECT @InvoiceID, @NewDatauniqueID, pid, (select ProductName from prMstproductMaster where ProductId = pid) ProductName
				, (select c.ServiceOrGoods from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) ProductName
				, (select case when(c.ServiceOrGoods='G') then c.HSNCode else c.SACCode end from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) ProductName
				, price, qty, totalamout, tax
				, GETDATE(), @UserID

				FROM OPENXML (@idoc, 'root/row/col',1)
				
				WITH (pid varchar(50), price varchar(50), qty varchar(50), totalamout varchar(50), tax varchar(50))

				--Create an internal representation of the XML document.
				EXEC sp_xml_preparedocument @idoc OUTPUT, @XMLProductData

				INSERT INTO [trnExpenseproduct]
						(InvoiceID,[DataUniqueID],[ProductId],[ProductName],[ServiceGood],[SACHCNCode],
						[PricePerUnit],[Quantity],[TotalAmount],[TaxOnProduct],[isBreakupNeed]
						,[CreatedOn],[CreatedBy])
				select @NewInvoiceID, @NewDatauniqueID, pid, (select ProductName from prMstproductMaster where ProductId = pid) ProductName
				, (select c.ServiceOrGoods from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) [SACHCNCode]
				, (select case when(c.ServiceOrGoods='G') then c.HSNCode else c.SACCode end from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) ProductName
				, price, qty, total, tax, isBreakupNeed
				, GETDATE(), @UserID

				FROM OPENXML (@idoc, 'root/row/col',1)
				
				WITH (pid varchar(50), price varchar(50), qty varchar(50), total varchar(50), tax varchar(50), isBreakupNeed varchar(1))
			
			END 
			ELSE
				DELETE FROM [trnExpenseHeader] where InvoiceID = @InvoiceID
				DELETE FROM [trnExpenseproduct] where InvoiceID = @InvoiceID
				DELETE FROM trnExpenseTravelling 
				WHERE InvoiceProductID in (
					select InvoiceProductID from trnExpenseProduct where ExpenseID = @InvoiceID
			);

		END
	END
END





GO
