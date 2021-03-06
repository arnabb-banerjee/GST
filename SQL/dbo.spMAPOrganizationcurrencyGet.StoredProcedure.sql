USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMAPOrganizationcurrencyGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- select * from PRMSTCategoryMaster
--[spMAPOrganizationcurrencyGet] 1, 0, '', 0
--[spMAPOrganizationcurrencyGet] 0, 0, '', 0
CREATE PROCEDURE [dbo].[spMAPOrganizationcurrencyGet] (
	@Mode					INT = 0, 
	@OrganizationCurrencyId	BIGINT = 0,
	@CurrencyId				VARCHAR(50), 
	@OrganizationCode		VARCHAR(50)
)
AS 
BEGIN 
	DECLARE @OrganizationId BIGINT
	SELECT @OrganizationId = OrganizationID FROM urMstOrganizationMaster WHERE OrganizationCode = @OrganizationCode
	
	IF @Mode = 0
	BEGIN
		SELECT A.OrganizationCurrencyId, a.CurrencyId, b.name, b.symbol, 'Y' isExist
		FROM prMAPOrganizationcurrency A
		inner join currency b on a.CurrencyId = b.code
		WHERE ISNULL(a.CurrencyId, '') = CASE WHEN(ISNULL(@CurrencyId, '') = '') Then ISNULL(a.CurrencyId, '') Else @CurrencyId End
		AND ISNULL(A.OrganizationID, 0) = CASE WHEN(ISNULL(@OrganizationId, 0) = 0) Then ISNULL(A.OrganizationID, 0) Else ISNULL(@OrganizationId, 0) End
		AND ISNULL(A.OrganizationCurrencyId, 0) = CASE WHEN(ISNULL(@OrganizationCurrencyId, 0) = 0) Then ISNULL(A.OrganizationCurrencyId, 0) Else ISNULL(@OrganizationCurrencyId, 0) End
	END
	ELSE IF @Mode = 1
	BEGIN
		SELECT A.OrganizationCurrencyId, b.code CurrencyId, b.name, b.symbol,
		CASE WHEN(ISNULL(A.CurrencyId, 'N')) <> 'Y' THEN 'N' ELSE 'Y' END isExist
		FROM currency b
		left join prMAPOrganizationcurrency a on a.CurrencyId = b.code
		WHERE ISNULL(a.CurrencyId, '') = CASE WHEN(ISNULL(@CurrencyId, '') = '') Then ISNULL(a.CurrencyId, '') Else @CurrencyId End
		AND ISNULL(A.OrganizationID, 0) = CASE WHEN(ISNULL(@OrganizationId, 0) = 0) Then ISNULL(A.OrganizationID, 0) Else ISNULL(@OrganizationId, 0) End
		AND ISNULL(A.OrganizationCurrencyId, 0) = CASE WHEN(ISNULL(@OrganizationCurrencyId, 0) = 0) Then ISNULL(A.OrganizationCurrencyId, 0) Else ISNULL(@OrganizationCurrencyId, 0) End
	END
END

-- exec spMAPOrganizationproductGet 1, 0, 0, '', 0, 0, ''





GO
