USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spPRMSTCategorySave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPRMSTCategorySave] (
	@CategoryId			BIGINT, 
	@ParentCategoryId	BIGINT, 
	@CategoryName		VARCHAR(5000),
	@CountryId			BIGINT,
	@ServiceOrGoods		VARCHAR(1), 
	@HSNSACCode			VARCHAR(50), 
	@WillCarryContent	VARCHAR(1),
	@IsActive  			VARCHAR(1),
	@IsExpenseType  	VARCHAR(1),
	@UserCode			VARCHAR(50),
	@isOnlyDelete		VARCHAR(1),
	@NewDatauniqueID 	BIGINT OUT,
	@ErrorMessage 		VARCHAR(500) OUT
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
			SELECT COUNT(CategoryId) x FROM PRMSTCategoryMaster WHERE CategoryId = @CategoryId and CountryID  = @CountryId
		)A

		IF (@IsExists > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
		IF ((SELECT COUNT(CategoryId) x FROM PRMSTCategoryMaster WHERE CategoryName = @CategoryName AND ((@CategoryId < 1) OR (@CategoryId > 0 AND CategoryId <> @CategoryId))) > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be saved as the same name is assigned for another record'
		END
	END 
	
	IF (@AllowSaveDeleteData = 'Y')
	BEGIN	
		IF((SELECT COUNT(*) FROM [dbo].[prMstCategoryMaster] WHERE CountryID = @CountryId AND HSNCode = @HSNSACCode AND CategoryId <> isnull(@CategoryId, 0)) > 0)
		BEGIN
			select @ErrorMessage = 'HSN [' + @HSNSACCode + '] is already used by anoter record in this country'
		END
		ELSE IF((SELECT COUNT(*) FROM [dbo].[prMstCategoryMaster] WHERE CountryID = @CountryId AND SACCode = @HSNSACCode AND CategoryId <> isnull(@CategoryId, 0)) > 0)
		BEGIN
			select @ErrorMessage = 'SAC [' + @HSNSACCode + '] is already used by anoter record in this country'
		END

		IF @CategoryId  < 1
		BEGIN 
			SELECT @NewDatauniqueID = 1
				 
			INSERT INTO PRMSTCategoryMaster (ParentCategoryId, CountryID, CategoryName, WillCarryContent, 
				DatauniqueID, ActivityType, IsActive, IsExpenseType, LastModifiedOn, LastModifiedBy,
				ServiceOrGoods, HSNCode, SACCode)
			VALUES(@ParentCategoryId, @CountryID, @CategoryName, @WillCarryContent, 
				@NewDatauniqueID, 'A', @IsActive, @IsExpenseType, GETDATE(), @UserID,
				@ServiceOrGoods, CASE WHEN(UPPER(RTRIM(LTRIM(@ServiceOrGoods))) = 'G') THEN @HSNSACCode ELSE NULL END, 
					CASE WHEN(UPPER(RTRIM(LTRIM(@ServiceOrGoods))) = 'S') THEN @HSNSACCode ELSE NULL END)
		END
		ELSE
		BEGIN 
			SELECT @OldDatauniqueID = DatauniqueID FROM PRMSTCategoryMaster WHERE CategoryId = @CategoryId
					
			INSERT INTO PRMSTCategoryMasterAudit (CategoryId, ParentCategoryId, CountryID, CategoryName, WillCarryContent, 
				DatauniqueID, ActivityType, IsExpenseType, IsActive, LastModifiedOn, LastModifiedBy,
				ServiceOrGoods, HSNCode, SACCode, AuditOperationOn, AuditOperationBy)
			SELECT CategoryId, ParentCategoryId, @CountryId, CategoryName, WillCarryContent, 
				DatauniqueID, CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END, IsExpenseType, IsActive, LastModifiedOn, LastModifiedBy,
				ServiceOrGoods, HSNCode, SACCode, GETDATE(), @UserID 
			FROM PRMSTCategoryMaster
				WHERE CategoryId = @CategoryId
				   				  
			IF @isOnlyDelete <> 'Y'
			BEGIN 
				  
				SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM PRMSTCategoryMaster WHERE CategoryId = @CategoryId and CountryID = @CountryId
				  
				UPDATE PRMSTCategoryMaster SET 
					ParentCategoryId = @ParentCategoryId, 
					CategoryName = @CategoryName, 
					DatauniqueID = @NewDatauniqueID, 
					CountryID = @CountryId, 
					WillCarryContent = @WillCarryContent,
					ActivityType = 'M', 
					IsActive = @IsActive, 
					IsExpenseType = @IsExpenseType,
					LastModifiedOn = GETDATE(), 
					LastModifiedBy = @UserID,
					ServiceOrGoods = @ServiceOrGoods, 
					HSNCode = CASE WHEN(UPPER(RTRIM(LTRIM(@ServiceOrGoods))) = 'G') THEN @HSNSACCode ELSE NULL END, 
					SACCode = CASE WHEN(UPPER(RTRIM(LTRIM(@ServiceOrGoods))) = 'S') THEN @HSNSACCode ELSE NULL END 
					WHERE CategoryId = @CategoryId
				END 
			ELSE 
				DELETE FROM PRMSTCategoryMaster WHERE CategoryId = @CategoryId
		END 
	END
END




GO
