USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMstBillsSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

[dbo].[spMstBillsSave] 'B', '25/10/2018', '', '0', '', '', '1', '10042', 'Amar chakraborty road', 41, 101, 
'[row]  [col       pid="11"        qty="12"       price="12"       total="144"       tax="18"       totaltax="162"   /] [/row][row]  [col       pid="15"        qty="18"       price="13"       total="234"       tax="11"       totaltax="245"   /] [/row][row

]  [col       pid="19"        qty="23"       price="21"       total="483"       tax="18"       totaltax="501"   /] [/row]'
, '', '', 'Y', 'Y', '', 'N', 0, ''

*/
CREATE PROCEDURE [dbo].[spMstBillsSave] (
	@InvoiceType				VARCHAR(1) = '',
	@InvoiceDate				VARCHAR(20) = '',
	@InvoiceDueDate				VARCHAR(20) = '',
	@InvoiceID					BIGINT = 0, 
	@InvoiceNumber				VARCHAR(50) = '', 
	@BranchID					BIGINT = 0, 
	@OrganizationCode			VARCHAR(50) = '',
	@CusID						BIGINT = 0, 
	@ShipAddress				VARCHAR(500) = '', 
	@ShipCity					VARCHAR(50) = '',
	@ShipState					INT = 0,
	@ShipCountry				INT = 0,
	@XMLProductData				VARCHAR(max) = '', 
	@Prices						VARCHAR(500) = '', 
	@Taxes						VARCHAR(500) = '', 
	@ChangedCurrency			VARCHAR(20) = '', 
	@PrevConversionRate			VARCHAR(10) = '', 
	@ConversionRate				VARCHAR(10) = '', 
	@SumAmount					VARCHAR(20) = '', 
	@AmountPayable				VARCHAR(20) = '', 
	@AmountDue					VARCHAR(20) = '', 
	@IsReturned  				VARCHAR(1) = '',
	@IsCancelled  				VARCHAR(1) = '',
	@UserCode					VARCHAR(50) = '',
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
	
	DECLARE @UserID BIGINT
	SELECT @UserID = 0

	IF(ISNULL(RTRIM(LTRIM(@UserCode)), '') <> '')
	BEGIN
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	END

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
			SELECT COUNT(InvoiceID) x FROM trnInvoiceHeader WHERE InvoiceID = @InvoiceID OR InvoiceNumber = @InvoiceNumber
			UNION
			SELECT COUNT(InvoiceID) x FROM trnInvoiceHeaderAudit WHERE InvoiceID = @InvoiceID OR InvoiceNumber = @InvoiceNumber)A

		IF (@IsExists > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
		IF ((SELECT COUNT(InvoiceID) x FROM trnInvoiceHeader WHERE InvoiceNumber = @InvoiceNumber AND ((@InvoiceID < 1) OR (@InvoiceID > 0 AND InvoiceID <> @InvoiceID))
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
			
			INSERT INTO trnInvoiceHeader ([DataUniqueID],[ActivityType],[InvoiceNumber],[IsCancelled],[IsReturned],[ReturnedOn]
				,[InvoiceType],[InvoiceDate],[BranchID]
				,[CusID],[ShipAddess],[ShipCity],[ShipState],[ShipCountry],[ChangedCurrency],[PrevConversionRate],[ConversionRate]
				,[SumAmount],[AmountPayable], [AmountDue]
				   
				/*,[TotalAmount],[Discount],[DiscountFormat],[AmountAfterDiscount],[AmountPayable]*/
				,[Duedate],[OrganizationId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
			VALUES(@NewDatauniqueID, 'A', @InvoiceNumber, @IsCancelled, @IsReturned, CASE WHEN (ISNULL(@IsReturned, '') = 'Y') THEN GETDATE() ELSE NULL END
				,@InvoiceType, convert(datetime, @InvoiceDate, 103), @BranchID
				,@CusID, @ShipAddress, @ShipCity, @ShipState, @ShipCountry,@ChangedCurrency, @PrevConversionRate, @ConversionRate
				,@SumAmount, @AmountPayable, @AmountDue
				,@InvoiceDueDate, @OrganizationID, GETDATE(), @UserID, null, null)

			SELECT @NewInvoiceID = @@IDENTITY
			
			--Create an internal representation of the XML document.
			EXEC sp_xml_preparedocument @idoc OUTPUT, @XMLProductData

			INSERT INTO [trnInvoiceproduct]
					(InvoiceID,[DataUniqueID],[ProductId],[ProductName],[ServiceGood],[SACHCNCode],
					[PricePerUnit],[Quantity],[TotalAmount],[TaxOnProduct],[AmountIncludeTax]
					,[CreatedOn],[CreatedBy])
			select @NewInvoiceID, @NewDatauniqueID, pid, (select ProductName from prMstproductMaster where ProductId = pid) ProductName
			, (select c.ServiceOrGoods from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) [SACHCNCode]
			, (select case when(c.ServiceOrGoods='G') then c.HSNCode else c.SACCode end from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) ProductName
			, price, qty, total, tax, AmtInclTax
			, GETDATE(), @UserID

			FROM OPENXML (@idoc, 'root/row/col',1)
				
			WITH (pid varchar(50), price varchar(50), qty varchar(50), total varchar(50), tax varchar(50), AmtInclTax varchar(50))
				 
		END
		ELSE
		BEGIN 
			SELECT @OldDatauniqueID = DatauniqueID FROM trnInvoiceHeader WHERE InvoiceID = @InvoiceID
					
			INSERT INTO trnInvoiceHeaderAudit ([DataUniqueID],[ActivityType],[InvoiceNumber],[IsCancelled],[IsReturned],[ReturnedOn]
				,[InvoiceType],[InvoiceDate],[BranchID]
				,[CusID],[CusAddress],[CusState],[CusCountry]
				,[TotalAmount],[Discount],[DiscountFormat],[AmountAfterDiscount],[ChangedCurrency],[PrevConversionRate],[ConversionRate]
				,[SumAmount],[AmountPayable],[AmountDue]
				,[Duedate],[OrganizationId],[LastModifiedOn],[LastModifiedBy],[CreatedOn],[CreatedBy], AuditOperationOn, AuditOperationBy)
			SELECT [DataUniqueID],CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END, [InvoiceNumber],[IsCancelled],[IsReturned],[ReturnedOn]
				,[InvoiceType],[InvoiceDate],[BranchID]
				,[CusID],[CusAddress],[CusState],[CusCountry]
				,[TotalAmount],[Discount],[DiscountFormat],[AmountAfterDiscount],[ChangedCurrency],[PrevConversionRate],[ConversionRate]
				,[SumAmount],[AmountPayable],[AmountDue]
				,[Duedate],[OrganizationId],[LastModifiedOn],[LastModifiedBy],[CreatedOn],[CreatedBy], GETDATE(), @UserID 
			FROM trnInvoiceHeader
			WHERE InvoiceID = @InvoiceID
				
			delete from [trnInvoiceproduct] where InvoiceID = @InvoiceID

			IF @isOnlyDelete <> 'Y'
			BEGIN 
				SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM [trnInvoiceHeader] where InvoiceID = @InvoiceID

				update trnInvoiceHeader set [DataUniqueID] = @NewDatauniqueID,
						[ActivityType] = 'M'
						,[IsCancelled] = @IsCancelled
						,[IsReturned] = @IsReturned
						,[ReturnedOn] = CASE WHEN(@IsReturned = 'Y') THEN GETDATE() ELSE NULL END
						,[InvoiceType] = @InvoiceType
						,[InvoiceDate] = @InvoiceDate
						,[BranchID] = @BranchID
						,[CusID] = @CusID
						,[ShipAddess] = @ShipAddress
						,[ShipCity] = @ShipCity
						,[ShipState] = @ShipState
						,[ShipCountry] = @ShipCountry
						,[ChangedCurrency] = @ChangedCurrency
						,[ConversionRate] = @ConversionRate
						,[PrevConversionRate] = @PrevConversionRate
						,[SumAmount] = @SumAmount
						,[AmountPayable] = @AmountPayable
						,[AmountDue] = @AmountDue
						,[Duedate] = DATEADD(DAY, 30, GETDATE())
				   
					/*,[TotalAmount],[Discount],[DiscountFormat],[AmountAfterDiscount],[AmountPayable]*/
						,[OrganizationId] = @OrganizationID
						,[LastModifiedOn] = GETDATE()
						,[LastModifiedBy] = @UserID

					where InvoiceID = @InvoiceID
				
				
				--Create an internal representation of the XML document.
				EXEC sp_xml_preparedocument @idoc OUTPUT, @XMLProductData

				INSERT INTO [trnInvoiceproduct]
					([InvoiceID],[DataUniqueID],[ProductId],[ProductName],[ServiceGood],[SACHCNCode]
					,[PricePerUnit],[Quantity],[TotalAmount],[TaxOnProduct], [AmountIncludeTax]
					,[CreatedOn],[CreatedBy])

				SELECT @InvoiceID, @NewDatauniqueID, pid, (select ProductName from prMstproductMaster where ProductId = pid) ProductName
				, (select c.ServiceOrGoods from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) ProductName
				, (select case when(c.ServiceOrGoods='G') then c.HSNCode else c.SACCode end from prMstCategoryMaster c, prMstproductMaster p where c.CategoryId = p.CountryID and ProductId = pid) ProductName
				, price, qty, totalamout, tax, AmtInclTax
				, GETDATE(), @UserID

				FROM OPENXML (@idoc, 'root/row/col',1)
				
				WITH (pid varchar(50), price varchar(50), qty varchar(50), totalamout varchar(50), tax varchar(50), AmtInclTax varchar(50))

			END 
			ELSE
				DELETE FROM [trnInvoiceproduct] where InvoiceID = @InvoiceID
		END
	END
END








GO
