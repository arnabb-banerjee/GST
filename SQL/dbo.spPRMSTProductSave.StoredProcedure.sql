USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spPRMSTProductSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: SQLQuery2.sql|7|0|C:\Users\DMTECH~1\AppData\Local\Temp\~vs9380.sql
-- Batch submitted through debugger: SQLQuery1.sql|7|0|C:\Users\DMTECH~1\AppData\Local\Temp\~vs1D34.sql
CREATE PROCEDURE [dbo].[spPRMSTProductSave] (
	@ProductId						BIGINT = 0, 
	@ProductName					VARCHAR(5000) = '',
	/*@IsExpense  					VARCHAR(1) = '',*/
	@CategoryId						BIGINT = 0,
	@CountryID						BIGINT = 0,
	@IsActive  						VARCHAR(1) = '',
	@UserCode						VARCHAR(50) = '',
	@isOnlyDelete					VARCHAR(1) = '',
	@NewDatauniqueID 				BIGINT OUT,
	@ErrorMessage 					VARCHAR(50) OUT
)
AS 
BEGIN 

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	select @AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT
	DECLARE @UserID BIGINT
	SELECT @UserID = 0

	IF(ISNULL(RTRIM(LTRIM(@UserCode)), '') <> '')
	BEGIN
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	END

	IF (@isOnlyDelete = 'Y')
	BEGIN 
		SELECT @IsExists = MAX(x) FROM (
			SELECT COUNT(ProductId) x FROM PRMAPOrganizationProduct WHERE ProductId = @ProductId
			UNION
			SELECT COUNT(ProductId) x FROM trnInvoiceProduct WHERE ProductId = @ProductId)A

		IF (@IsExists > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
			IF ((SELECT COUNT(ProductId) x FROM PRMSTProductMaster WHERE ProductName = @ProductName AND ((@ProductId < 1) OR (@ProductId > 0 AND ProductId <> @ProductId))
				) > 0)
			BEGIN 
					SELECT @AllowSaveDeleteData = 'N'
					SELECT @ErrorMessage = 'Data can not be saved as the same name is assigned for another record'
		

	END
	END 
	
	IF (@AllowSaveDeleteData = 'Y')
	BEGIN			
			IF @ProductId < 1
			BEGIN 
				 SELECT @NewDatauniqueID = 1
			
			  INSERT INTO PRMSTProductMaster (ProductName, CategoryId, CountryID, 
				  DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy)
			  VALUES(@ProductName, @CategoryId, @CountryID, 
				  @NewDatauniqueID, 'A', @IsActive, GETDATE(), @UserID)
			END
			ELSE
			BEGIN 
				SELECT @OldDatauniqueID = DatauniqueID FROM PRMSTProductMaster WHERE ProductId = @ProductId
			
		
				INSERT INTO PRMSTProductMasterAudit (ProductId, ProductName, CategoryId, CountryID, 
					  DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy,
					  AuditOperationOn, AuditOperationBy)
				SELECT ProductId, ProductName, CategoryId, CountryID,
					  DatauniqueID, CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END, IsActive, LastModifiedOn, LastModifiedBy,
					  GETDATE(), @UserID 
				FROM PRMSTProductMaster
				WHERE ProductId = @ProductId
		
		   
				IF @isOnlyDelete <> 'Y'
				BEGIN 
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM PRMSTProductMaster WHERE ProductId = @ProductId
		
				  	UPDATE PRMSTProductMaster SET 
				  		ProductName = @ProductName, 
				  		DatauniqueID = @NewDatauniqueID, 
	  					CategoryId = @CategoryId,
						CountryID = @CountryID,
				  		ActivityType = 'M', 
				  		IsActive = @IsActive, 
				  		LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
					WHERE ProductId = @ProductId
				END 
				ELSE
					DELETE FROM PRMSTProductMaster WHERE ProductId = @ProductId
			END 
	END
END





GO
