USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceListMasters]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[GetInvoiceListMasters]
(
	@InvoiceID BIGINT,
	@OrganizationCode VARCHAR(50),
	@BranchID BIGINT = 0,
	@CusID BIGINT = 0,
	@InvoiceDateFrom VARCHAR(10),
	@InvoiceDateTo  VARCHAR(10)
)
AS
BEGIN
	DECLARE @OrganizationID BIGINT
	SELECT @OrganizationID = 0

	IF @OrganizationCode <> '' 
		SELECT @OrganizationID = OrganizationID FROM urMstOrganizationMaster WHERE OrganizationCode = @OrganizationCode
	
	select h.InvoiceID, h.InvoiceNumber, h.AmountPayable, h.Duedate, h.InvoiceDate, H.BranchID, H.CusID, 
			o.OrganizationCode, o.OrganizationName, ob.BranchName, h.LastModifiedBy, h.LastModifiedOn, h.ActivityType, h.IsCancelled, h.IsReturned, h.AmountAfterDiscount, 
			h.AmountPayable, h.AmountDue, h.TotalAmount, h.ReturnedOn, h.BranchAddress, h.BranchCountry, h.BranchState, 
			h.ShipAddess, h.ShipCity, h.ShipState, h.ShipCountry, h.CusEmail, H.DataUniqueID, 'Y' IsActive, ChangedCurrency, 
			PrevConversionRate, ConversionRate, SumAmount,
			ISNULL(C.FirstName, '') + 
				CASE WHEN(ISNULL(C.MiddleName, '')<>'') THEN ' ' + C.MiddleName ELSE '' END + 
				CASE WHEN(ISNULL(C.LastName, '')<>'') THEN ' ' + C.LastName ELSE '' END CustomerName,

			(SELECT round(sum(case when isnumeric(Quantity) <> 0 then cast(Quantity as numeric(10,2)) else 0 end), 2) FROM trnInvoiceProduct I WHERE H.InvoiceID = I.InvoiceID) TotalQuantity, 
			(SELECT round(sum(case when isnumeric(AmountIncludeTax) <> 0 then cast(AmountIncludeTax as numeric(10,2)) else 0 end), 2) FROM trnInvoiceProduct I WHERE H.InvoiceID = I.InvoiceID) AmountIncludeTax, 
			(SELECT round(sum(case when isnumeric(TaxOnProduct) <> 0 then cast(TaxOnProduct as numeric(10,2)) else 0 end), 2) FROM trnInvoiceProduct I WHERE H.InvoiceID = I.InvoiceID) TaxOnProduct, 
			(SELECT round(sum(case when isnumeric(Quantity) <> 0 and isnumeric(PricePerUnit) = 1 then (cast(Quantity as numeric(10,2)) * cast(PricePerUnit as numeric(10,2))) else 0 end), 2) FROM trnInvoiceProduct I WHERE H.InvoiceID = I.InvoiceID) AmountExcludeTax




		
	from trnInvoiceHeader h
	LEFT JOIN urMstOrganizationMaster O ON O.OrganizationID = H.OrganizationID
	LEFT JOIN urMstOrganizationBranch OB ON OB.OrganizationID = O.OrganizationID AND OB.BranchID = H.BranchID 
	LEFT JOIN cusMstcustomerMaster C ON C.CusID = H.CusID 
	where h.InvoiceID = case when(isnull(@InvoiceID, 0) = 0) then h.InvoiceID else @InvoiceID end
	AND ISNULL(h.OrganizationID, 0) = case when(isnull(@OrganizationID, 0) = 0) then ISNULL(h.OrganizationID, 0) else @OrganizationID end
	AND ISNULL(h.CusID, 0) = case when(isnull(@CusID, 0) = 0) then ISNULL(h.CusID, 0) else @CusID end
	AND ISNULL(h.BranchID, 0) = case when(isnull(@BranchID, 0) = 0) then ISNULL(h.BranchID, 0) else @BranchID end
	AND H.InvoiceType = 'B'

	if(isnull(@InvoiceID, 0) <> 0)
	begin
		select p.InvoiceID, p.ProductId, p.ProductName, p.ServiceGood, p.SACHCNCode, p.Quantity, p.PricePerUnit, 
				p.TaxOnProduct, p.TotalAmount, p.AmountIncludeTax, p.DataUniqueID
		from trnInvoiceproduct p 
		where p.InvoiceID = case when(isnull(@InvoiceID, 0) = 0) then p.InvoiceID else @InvoiceID end

		select p.InvoiceID, p.PaymentID, p.PaidAmount, p.PaidOn, p.PaymentMethod, p.PaymentStatus 
		from trnInvoicePayment p
		where p.InvoiceID = case when(isnull(@InvoiceID, 0) = 0) then p.InvoiceID else @InvoiceID end
	END
END




GO
