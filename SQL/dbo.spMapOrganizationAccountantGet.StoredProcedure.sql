USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMapOrganizationAccountantGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

[dbo].[spMapOrganizationAccountantGet] 0, 0, '', ''

[dbo].[spMapOrganizationAccountantGet] 1, 0, '', ''

*/

CREATE PROCEDURE [dbo].[spMapOrganizationAccountantGet] (
	@Mode						INT = 0,
	@ID							BIGINT = 0,
	@AccountantCode				VARCHAR(50) = '',
	@OrganizationCode			VARCHAR(50) = ''
)
AS 
BEGIN 
	DECLARE @OrganizationId BIGINT
	DECLARE @AccountantId BIGINT
	SELECT @OrganizationId = 0, @AccountantId = 0

	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	if(isnull(rtrim(ltrim(@AccountantCode)), '') <> '')
	begin
		SELECT @AccountantId = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @AccountantCode
	end

	IF @Mode = 0
	BEGIN

		SELECT	A.ID, o.OrganizationCode, u.UserCode AccountantCode, A.LastModifiedOn, A.LastModifiedBy,
				O.OrganizationName, 'N' isSelected,
				ISNULL(U.FirstName, '') + 
				CASE WHEN(ISNULL(U.MiddleName, '') <> '') THEN ' ' + U.MiddleName ELSE '' END + 
				CASE WHEN(ISNULL(U.LastName, '') <> '') THEN ' ' + U.LastName ELSE '' END AccountantName 

		FROM	trnMapOrganizationAccountant A
		LEFT JOIN [URMSTOrganizationMaster] O ON A.OrganizationID = O.OrganizationID
		LEFT JOIN [urMstUserRegisteredMaster] U ON A.AccountantID = U.UserID
		WHERE   U.UserType = 'A'
		AND		isnull(A.ID, 0) = case when(isnull(@ID, 0) <> 0) then @ID else isnull(A.ID, 0) end
		AND		isnull(A.AccountantID, 0) = case when(isnull(@AccountantID, 0) <> 0) then @AccountantID else isnull(A.AccountantID, 0) end
		AND		isnull(A.OrganizationID, 0) = case when(isnull(@OrganizationID, 0) <> 0) then @OrganizationID else isnull(A.OrganizationID, 0) end
	END
	ELSE IF @Mode = 1
	BEGIN
		SELECT	A.ID, o.OrganizationCode, u.UserCode AccountantCode, A.LastModifiedOn, A.LastModifiedBy,
				O.OrganizationName,
				CASE WHEN(ISNULL(CAST(A.AccountantID AS BIGINT), 0)) > 0 THEN 'Y' ELSE 'N' END isSelected,
				ISNULL(U.FirstName, '') + 
				CASE WHEN(ISNULL(U.MiddleName, '') <> '') THEN ' ' + U.MiddleName ELSE '' END + 
				CASE WHEN(ISNULL(U.LastName, '') <> '') THEN ' ' + U.LastName ELSE '' END AccountantName 

		FROM  [urMstUserRegisteredMaster] U
		LEFT JOIN trnMapOrganizationAccountant A ON A.AccountantID = U.UserID
		LEFT JOIN [URMSTOrganizationMaster] O ON A.OrganizationID = O.OrganizationID
		WHERE   U.UserType = 'A'
		AND		isnull(A.ID, 0) = case when(isnull(@ID, 0) <> 0) then @ID else isnull(A.ID, 0) end
		AND		isnull(A.AccountantID, 0) = case when(isnull(@AccountantID, 0) <> 0) then @AccountantID else isnull(A.AccountantID, 0) end
		AND		isnull(A.OrganizationID, 0) = case when(isnull(@OrganizationID, 0) <> 0) then @OrganizationID else isnull(A.OrganizationID, 0) end
	END
END




GO
