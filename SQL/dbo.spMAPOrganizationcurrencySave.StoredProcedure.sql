USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMAPOrganizationcurrencySave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
[dbo].[spMAPOrganizationcurrencySave] 0, 'AFN', '1C0E80ED-A137-49EC-9152-2D3FBAF0C42A', '10', 'Y', 0, ''
*/

CREATE PROCEDURE [dbo].[spMAPOrganizationcurrencySave] (
	@OrganizationCurrencyId BIGINT,
	@CurrencyId				VARCHAR(50), 
	@OrganizationCode		VARCHAR(50),
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

	INSERT INTO prMAPOrganizationcurrencyAudit(CurrencyId, OrganizationID, DatauniqueID, LastModifiedOn, LastModifiedBy)
	SELECT CurrencyId, OrganizationID, DatauniqueID, LastModifiedOn, LastModifiedBy
	FROM prMAPOrganizationcurrency
			WHERE OrganizationCurrencyId = @OrganizationCurrencyId

	IF (@OrganizationCurrencyId > 0 and @isOnlyDelete = 'Y')
	BEGIN 
		DELETE FROM prMAPOrganizationcurrency WHERE OrganizationCurrencyId = @OrganizationCurrencyId
	END 
	ELSE IF (@OrganizationCurrencyId > 0 and @isOnlyDelete <> 'Y')
	BEGIN
		UPDATE prMAPOrganizationcurrency set CurrencyId = @CurrencyId
			, OrganizationID = @OrganizationId
			, DatauniqueID = (select DatauniqueID+1 from prMAPOrganizationcurrency where OrganizationCurrencyId = @OrganizationCurrencyId) 
			, LastModifiedOn = GETDATE()
			, LastModifiedBy = @UserID	
		WHERE OrganizationCurrencyId = @OrganizationCurrencyId
	END
	ELSE IF (@OrganizationCurrencyId = 0)
	BEGIN 
		IF ((SELECT COUNT(*) FROM prMAPOrganizationcurrency WHERE CurrencyId = @CurrencyId AND isnull(OrganizationID, 0) = isnull(@OrganizationId, 0)) > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be saved as the same currecy is already added by you in your account'
		END
		ELSE
		BEGIN
			INSERT INTO prMAPOrganizationcurrency (CurrencyId, OrganizationID, DatauniqueID, LastModifiedOn, LastModifiedBy)
			VALUES(@CurrencyId, @OrganizationId, @NewDatauniqueID, GETDATE(), @UserID)	
		END 
	END 
END



GO
