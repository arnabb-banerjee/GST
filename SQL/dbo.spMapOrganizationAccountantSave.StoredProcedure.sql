USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMapOrganizationAccountantSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------- > -----------------------------------
------------------------------------------------------------
/*
[dbo].[spMapOrganizationAccountantSave] '', '9D3BB59F-AFF5-491B-BA45-727DF0321E9E', '', 'Y', '1C0E80ED-A137-49EC-9152-2D3FBAF0C42A', ''


*/
------------------------------------------------------------
------------------------------------------------------------

CREATE PROCEDURE [dbo].[spMapOrganizationAccountantSave] (
	@ID							BIGINT,
	@AccountantCode				VARCHAR(50),
	@OrganizationCode			VARCHAR(50),
	@isOnlyDelete				VARCHAR(1),
	@UserCode					VARCHAR(50),
	@ErrorMessage				VARCHAR(50) OUT
)
AS 
BEGIN 
	DECLARE @OrganizationId BIGINT
	DECLARE @AccountantId BIGINT
	DECLARE @UserId BIGINT

	SELECT @OrganizationId = 0, @AccountantId = 0, @UserId = 0

	SELECT @ErrorMessage = ''

	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	if(isnull(rtrim(ltrim(@AccountantCode)), '') <> '')
	begin
		SELECT @AccountantId = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @AccountantCode
	end

	/*
	  SELECT @AccountantId
	  SELECT @OrganizationId */
	
	if(isnull(rtrim(ltrim(@UserCode)), '') <> '')
	begin
		SELECT @UserId = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	end

	BEGIN TRY
		INSERT INTO trnMapOrganizationAccountantAudit (ID, OrganizationID, AccountantID, UserID, LastModifiedOn, LastModifiedBy) 
		SELECT	ID, OrganizationID, AccountantID, UserID, LastModifiedOn, LastModifiedBy
		FROM	trnMapOrganizationAccountant
		WHERE   AccountantID = @AccountantId
		AND isnull(OrganizationID, 0) = @OrganizationId

		DELETE FROM trnMapOrganizationAccountant WHERE isnull(OrganizationID, 0) = @OrganizationId AND AccountantID = @AccountantId

		IF (ISNULL(@isOnlyDelete, '') <> 'Y')
		BEGIN
			INSERT INTO trnMapOrganizationAccountant (OrganizationID, AccountantID, UserID, LastModifiedOn, LastModifiedBy) 
			VALUES(@OrganizationId, @AccountantId, @UserID, GETDATE(), @UserID)
		END
	END TRY
	BEGIN CATCH
		SELECT @ErrorMessage = @@ERROR
		RETURN		
	END CATCH
END




GO
