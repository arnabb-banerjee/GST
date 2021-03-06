USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spBankTransactionsGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[spMSTBanksGet] '', ''
CREATE PROCEDURE [dbo].[spBankTransactionsGet]
(
	@ID						varchar(50),
	@OrganizationCode 		varchar(50) = ''
)
AS 
BEGIN
	DECLARE @OrganizationId BIGINT

	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	SELECT M.[Id],M.TransactionID,M.[DatauniqueID],M.[ActivityType],M.[TransactionDate],
		M.[ChqNo],M.[Particulars],M.[Debit],M.[Credit],M.[Balance],M.[InitBr],
		M.[Products],'' [CustomerId],'' [CustomerName],'' [ProductIds],M.[IsSellExpense],M.[Tax],
		M.[IsActive],M.[LastModifiedOn],M.[LastModifiedBy],O.[OrganizationCode],O.[OrganizationName],
		M.[CreatedOn],M.[CreatedBy]
	FROM [mstBankTransactions] M
	--LEFT JOIN cusMstcustomerMaster C ON C.CusID = M.[CustomerId] AND M.[OrganizationID] = @OrganizationId
	LEFT JOIN URMSTOrganizationMaster O ON O.OrganizationID = M.OrganizationID AND M.OrganizationID = @OrganizationId
	WHERE ISNULL(M.OrganizationID, 0) = CASE WHEN(ISNULL(NULLIF(@OrganizationID, ''), 0) = 0) Then ISNULL(M.OrganizationID, 0) Else ISNULL(NULLIF(@OrganizationID, ''), 0) End
	AND ISNULL(ID, 0) = CASE WHEN(ISNULL(NULLIF(@ID, ''), 0) = 0) Then ISNULL(ID, 0) Else ISNULL(NULLIF(@ID, ''), 0) End
END



GO
