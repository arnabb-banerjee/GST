USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[DashBoardValues]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DashBoardValues] (
	@FromDate				VARCHAR(20) = '',
	@ToDate					VARCHAR(20) = '',
	@OrganizationCode		VARCHAR(50) = '',
	@Type					VARCHAR(1) = '' /*I,E*/
)
AS 
BEGIN 
	DECLARE @OrganizationID BIGINT, @dFromDate Date, @dToDate Date
	
	select @dFromDate = case when isdate(@FromDate) = 0 then convert(date, '', 103) else convert(date, @FromDate, 103) end, 
		   @dToDate = case when isdate(@ToDate) = 0 then convert(date, '02/02/2050', 103) else convert(date, @ToDate, 103) end, 
		   @OrganizationID = OrganizationID FROM urMstOrganizationMaster WHERE OrganizationCode = @OrganizationCode

	SELECT 'I' [Type], c.OrganizationName, c.OrganizationCode, month(B.InvoiceDate) Months, Count(distinct a.InvoiceID) NoOfInvoice, sum(isnumeric(a.PricePerUnit)) PricePerUnit, 
		sum(isnumeric(Quantity)) Quantity, sum(isnumeric(a.TotalAmount)) TotalAmount, sum(isnumeric([TaxOnProduct])) TotalTax, 
		sum(isnumeric(AmountIncludeTax)) AmountIncludeTax
			FROM [trnInvoiceProduct] a
			inner join [trnInvoiceHeader] b on b.InvoiceID = a.InvoiceID
			inner join urMstOrganizationMaster c on c.OrganizationID = b.OrganizationId
			WHERE ISNULL(B.OrganizationID, 0) = CASE WHEN(ISNULL(NULLIF(@OrganizationID, ''), 0) = 0) Then ISNULL(B.OrganizationID, 0) Else ISNULL(NULLIF(@OrganizationID, ''), 0) End
			AND (@Type = '' OR @Type = 'I')
			AND B.InvoiceDate BETWEEN @dFromDate AND @dToDate
			AND UPPER(ISNULL(IsReturned, 'N')) <> 'Y'
			group by c.OrganizationName, c.OrganizationCode, month(B.InvoiceDate)

	union all

	SELECT 'R' [Type], c.OrganizationName, c.OrganizationCode, month(B.InvoiceDate) Months, Count(distinct a.InvoiceID) NoOfInvoice, sum(isnumeric(a.PricePerUnit)) PricePerUnit, 
		sum(isnumeric(Quantity)) Quantity, sum(isnumeric(a.TotalAmount)) TotalAmount, sum(isnumeric([TaxOnProduct])) TotalTax, 
		sum(isnumeric(AmountIncludeTax)) AmountIncludeTax
			FROM [trnInvoiceProduct] a
			inner join [trnInvoiceHeader] b on b.InvoiceID = a.InvoiceID
			inner join urMstOrganizationMaster c on c.OrganizationID = b.OrganizationId
			WHERE ISNULL(B.OrganizationID, 0) = CASE WHEN(ISNULL(NULLIF(@OrganizationID, ''), 0) = 0) Then ISNULL(B.OrganizationID, 0) Else ISNULL(NULLIF(@OrganizationID, ''), 0) End
			AND (@Type = '' OR @Type = 'I')
			AND B.InvoiceDate BETWEEN @dFromDate AND @dToDate
			AND UPPER(ISNULL(IsReturned, 'N')) = 'Y'
			group by c.OrganizationName, c.OrganizationCode, month(B.InvoiceDate)

	union all

	SELECT 'E' [Type], c.OrganizationName, c.OrganizationCode, month(B.InvoiceDate) Months, Count(distinct a.InvoiceID) NoOfInvoice, sum(isnumeric(a.PricePerUnit)) PricePerUnit, 
		sum(isnumeric(Quantity)) Quantity, sum(isnumeric(a.TotalAmount)) TotalAmount, sum(isnumeric([TaxOnProduct])) TotalTax, 
		sum(isnumeric(AmountIncludeTax)) AmountIncludeTax
			FROM [trnExpenseproduct] a
			inner join [trnExpenseHeader] b on b.InvoiceID = a.InvoiceID
			inner join urMstOrganizationMaster c on c.OrganizationID = b.OrganizationId
			WHERE ISNULL(B.OrganizationID, 0) = CASE WHEN(ISNULL(NULLIF(@OrganizationID, ''), 0) = 0) Then ISNULL(B.OrganizationID, 0) Else ISNULL(NULLIF(@OrganizationID, ''), 0) End
			AND (@Type = '' OR @Type = 'E')
			AND B.InvoiceDate BETWEEN @dFromDate AND @dToDate
			group by c.OrganizationName, c.OrganizationCode, month(B.InvoiceDate)
	
END





GO
