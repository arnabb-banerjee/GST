USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMAPOrganizationproductSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Batch submitted through debugger: SQLQuery3.sql|7|0|C:\Users\DMTECH~1\AppData\Local\Temp\~vsF145.sql
/*
[dbo].[spMAPOrganizationproductSave] 0, 11, '', 'Y', '1C0E80ED-A137-49EC-9152-2D3FBAF0C42A', 'Y', 0, ''
*/

CREATE PROCEDURE [dbo].[spMAPOrganizationproductSave] (
	@OrganizationproductId  BIGINT,
	@CategoryId				BIGINT, 
	@ProductId				BIGINT, 
	@OrganizationCode		VARCHAR(50),
	@Name					VARCHAR(50),
	@Description			VARCHAR(50),
	@SKU					VARCHAR(50),
	@Unit					BIGINT,
	@Class					BIGINT,
	@AbatementPercentage	NUMERIC(18,2),
	@ServiceType			BIGINT, 
	@SalePrice				NUMERIC(18,2),
	@isInclusiveTax			VARCHAR(1),
	@AvailableQty			NUMERIC(18,2),
	@IncomeAccount			VARCHAR(50),
	@SupplierId				BIGINT,
	@PreferredSupplierId	BIGINT,
	@ReverseCharge			NUMERIC(18,2),
	@PurchaseTax			NUMERIC(18,2),
	@SaleTax				NUMERIC(18,2),
	@IsActive  				VARCHAR(1),
	@UserCode				VARCHAR(50),
	@isOnlyDelete			VARCHAR(1),
	@NewDatauniqueID 		BIGINT OUT,
	@ErrorMessage 			VARCHAR(500) OUT
)
AS 
BEGIN 
	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	SELECT @AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT
	DECLARE @OrganizationId BIGINT
	DECLARE @UserID BIGINT
	SELECT @UserID = 0

	IF(ISNULL(RTRIM(LTRIM(@UserCode)), '') <> '')
	BEGIN
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	END

	SELECT @OrganizationId = OrganizationID FROM urMstOrganizationMaster WHERE OrganizationCode = @OrganizationCode

	INSERT INTO prMAPOrganizationproductAudit(ProductId, CategoryId, OrganizationID, DatauniqueID, LastModifiedOn, LastModifiedBy,
		[Name],[Description],[SKU],[Unit],[Class],[AbatementPercentage],[ServiceType], 
		[SalePrice],[isInclusiveTax],[AvailableQty],[IncomeAccount],[SupplierId],
		[PreferredSupplierId],[ReverseCharge],[PurchaseTax],[SaleTax],[isPersonal])
	SELECT ProductId, CategoryId, OrganizationID, DatauniqueID, LastModifiedOn, LastModifiedBy,
		[Name],[Description],[SKU],[Unit],[Class],[AbatementPercentage],[ServiceType], 
		[SalePrice],[isInclusiveTax],[AvailableQty],[IncomeAccount],[SupplierId],
		[PreferredSupplierId],[ReverseCharge],[PurchaseTax],[SaleTax],[isPersonal]
	FROM prMAPOrganizationproduct
			WHERE OrganizationproductId = @OrganizationproductId

	IF (@OrganizationproductId > 0 and @isOnlyDelete = 'Y')
	BEGIN 
		SELECT @IsExists = MAX(x) FROM (
			SELECT COUNT(InvoiceID) x FROM trnInvoiceproduct A WHERE a.ProductId = @ProductId and a.InvoiceID in (select InvoiceID from trnInvoiceHeader where OrganizationId = @OrganizationId)
			UNION
			SELECT COUNT(InvoiceID) x FROM trnInvoiceproductAudit A WHERE a.ProductId = @ProductId and a.InvoiceID in (select InvoiceID from trnInvoiceHeader where OrganizationId = @OrganizationId)
		)A

		IF (@IsExists > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
		ELSE
		BEGIN
			DELETE FROM prMAPOrganizationproduct WHERE OrganizationproductId = @OrganizationproductId
		END
	END
	ELSE IF (@ProductId > 0 AND @OrganizationId > 0 and @isOnlyDelete = 'Y')
	BEGIN 
		SELECT @IsExists = MAX(x) FROM (
			SELECT COUNT(InvoiceID) x FROM trnInvoiceproduct A WHERE a.ProductId = @ProductId and a.InvoiceID in (select InvoiceID from trnInvoiceHeader where OrganizationId = @OrganizationId)
			UNION
			SELECT COUNT(InvoiceID) x FROM trnInvoiceproductAudit A WHERE a.ProductId = @ProductId and a.InvoiceID in (select InvoiceID from trnInvoiceHeader where OrganizationId = @OrganizationId)
		)A

		IF (@IsExists > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
		ELSE
		BEGIN
			DELETE FROM prMAPOrganizationproduct WHERE ProductId = @ProductId AND OrganizationID = @OrganizationId
		END
	END 
	ELSE IF (@OrganizationproductId > 0 and @isOnlyDelete <> 'Y')
	BEGIN
		
		IF('Y' = (SELECT UPPER(ISNULL(RTRIM(LTRIM(isPersonal)), '')) FROM prMAPOrganizationproduct WHERE OrganizationproductId = @OrganizationproductId))
		BEGIN
			UPDATE prMAPOrganizationproduct set ProductId = @ProductId
				, OrganizationID = @OrganizationId
				, DatauniqueID = (select DatauniqueID+1 from prMAPOrganizationproduct where OrganizationproductId = @OrganizationproductId) 
				, [Name] = @Name
				, [Description] = @Description
				, [CategoryId] = @CategoryId
				, [SKU] = @SKU
				, [Unit] = @Unit
				, [Class] = @Class
				, [AbatementPercentage] = @AbatementPercentage
				, [ServiceType] = @ServiceType
				, [SalePrice] = @SalePrice
				, [isInclusiveTax] = @isInclusiveTax
				, [AvailableQty] = @AvailableQty
				, [IncomeAccount] = @IncomeAccount
				, [SupplierId] = @SupplierId
				, [PreferredSupplierId] = @PreferredSupplierId
				, [ReverseCharge] = @ReverseCharge
				, [PurchaseTax] = @PurchaseTax
				, [SaleTax] = @SaleTax
				, LastModifiedOn = GETDATE()
				, LastModifiedBy = @UserID	
				WHERE OrganizationproductId = @OrganizationproductId
		END	
		ELSE
		BEGIN
			UPDATE prMAPOrganizationproduct set ProductId = @ProductId
				, OrganizationID = @OrganizationId
				, DatauniqueID = (select DatauniqueID+1 from prMAPOrganizationproduct where OrganizationproductId = @OrganizationproductId) 
				/* Added by Partha on 27/09/2019 */
				, [Name] = @Name
				/* End: Added by Partha on 27/09/2019 */
				, [Unit] = @Unit
				, [Class] = @Class
				, [CategoryId] = @CategoryId --Category can be deleted in case personal
				, [AbatementPercentage] = @AbatementPercentage
				, [ServiceType] = @ServiceType
				, [SalePrice] = @SalePrice
				, [isInclusiveTax] = @isInclusiveTax
				, [AvailableQty] = @AvailableQty
				, [IncomeAccount] = @IncomeAccount
				, [SupplierId] = @SupplierId
				, [PreferredSupplierId] = @PreferredSupplierId
				, [ReverseCharge] = @ReverseCharge
				, [PurchaseTax] = @PurchaseTax
				, [SaleTax] = @SaleTax
				, LastModifiedOn = GETDATE()
				, LastModifiedBy = @UserID	
			WHERE OrganizationproductId = @OrganizationproductId
		END
	END
	ELSE IF (@OrganizationproductId = 0)
	BEGIN 
		DECLARE @Count1 INT, @isPersonal varchar(1)

		SELECT @isPersonal = ''

		IF (@ProductId = 0)
		BEGIN
			SELECT @isPersonal = 'Y'
			
			IF ((SELECT COUNT(*) FROM prMstproductMaster WHERE ProductName = @Name) > 0)
			BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Data can not be saved as the same product is already available in the global product list. \nClick on manage product and select'
			END
			ELSE IF ((SELECT COUNT(*) FROM prMAPOrganizationproduct WHERE name = @Name AND isnull(OrganizationID, 0) = isnull(@OrganizationId, 0)) > 0)
			BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Data can not be saved as the same product is already added by you in your account'
			END
			ELSE
			BEGIN
				--SELECT @ProductId = 1
				SELECT @Count1 = max(Count1) FROM (
					SELECT COUNT(OrganizationproductId) Count1 FROM prMAPOrganizationproduct WHERE ISNULL(isPersonal, '')  = 'Y' AND isnull(OrganizationID, 0) = isnull(@OrganizationId, 0)
					UNION ALL
					SELECT COUNT(OrganizationproductId) Count1 FROM prMAPOrganizationproductAudit WHERE ISNULL(isPersonal, '')  = 'Y' AND isnull(OrganizationID, 0) = isnull(@OrganizationId, 0)
				) A

				IF @Count1 > 0
				BEGIN
					SELECT @ProductId = ISNULL(MAX(ISNULL(ProductId, 0)), 0) + 1 FROM (
						SELECT ProductId FROM prMAPOrganizationproduct WHERE ISNULL(isPersonal, '')  = 'Y' AND isnull(OrganizationID, 0) = isnull(@OrganizationId, 0)
						UNION ALL
						SELECT ProductId FROM prMAPOrganizationproductAudit WHERE ISNULL(isPersonal, '')  = 'Y' AND isnull(OrganizationID, 0) = isnull(@OrganizationId, 0)
					) A

					SELECT @AllowSaveDeleteData = 'Y'
				END
			END
		END 
		ELSE IF ((SELECT COUNT(*) x FROM prMAPOrganizationproduct WHERE ProductId = @ProductId AND isnull(OrganizationID, 0) = isnull(@OrganizationId, 0)) > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be saved as the same product is already available in your account1'
		END
		
		IF (@AllowSaveDeleteData = 'Y')
		BEGIN
			SELECT @NewDatauniqueID = 1

			INSERT INTO prMAPOrganizationproduct (ProductId, CategoryId, OrganizationID, DatauniqueID, LastModifiedOn, LastModifiedBy,
				[Name],[Description],[SKU],[Unit],[Class],[AbatementPercentage],[ServiceType], 
				[SalePrice],[isInclusiveTax],[AvailableQty],[IncomeAccount],[SupplierId],
				[PreferredSupplierId],[ReverseCharge],[PurchaseTax],[SaleTax], isPersonal)
			VALUES(@ProductId, @CategoryId, @OrganizationId, @NewDatauniqueID, GETDATE(), @UserID,
				@Name,@Description,@SKU,@Unit,@Class,@AbatementPercentage,@ServiceType, 
				@SalePrice,@isInclusiveTax,@AvailableQty,@IncomeAccount,@SupplierId,
				@PreferredSupplierId,@ReverseCharge,@PurchaseTax,@SaleTax, @isPersonal)
		END 
	END 
END



GO
