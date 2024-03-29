USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceList]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetInvoiceList]
(
	@InvoiceID BIGINT,
	@InvoiceDateFrom VARCHAR(10),
	@InvoiceDateTo  VARCHAR(10),
	@OptionsForLastDays int = -1, -- 30, 90, 180, 365 
	@OptionsForLastMonths int = -1, -- 1, 3, 6, 12
	@IsInvoiceRequiredInDetails varchar(1) = 0,
	@IsInvoiceRequiredInSummary varchar(1) = 0
)
AS
BEGIN
	DECLARE @TotalInvoices bigint, @TotalAmount numeric, @TotalPaidAmount numeric, @TotalDueAmount numeric, @TotalOverDueInvoices bigint, @TotalOverDueAmount numeric 
	DECLARE @tblInvoiceIDs as table (InvoiceId BIGINT)
	DECLARE @InvoiceFromdate datetime, @InvoiceTodate datetime
	
	IF (@OptionsForLastDays > -1)
	BEGIN
		SELECT @InvoiceFromdate = DATEADD(DAY, @OptionsForLastDays, GETDATE())
		SELECT @InvoiceTodate = GETDATE()
	END
	ELSE IF (@OptionsForLastDays > -1)
	BEGIN
		SELECT @InvoiceFromdate = DATEADD(MONTH, @OptionsForLastDays, GETDATE())
		SELECT @InvoiceTodate = GETDATE()
	END
	ELSE 
	BEGIN
		SELECT @InvoiceFromdate = CASE WHEN(ISNULL(@InvoiceDateFrom, '') = '') THEN CONVERT(DATE, '19/01/1921', 103) ELSE CONVERT(DATE, @InvoiceDateFrom, 103) END
		SELECT @InvoiceTodate = CASE WHEN(ISNULL(@InvoiceDateTo, '') = '') THEN CONVERT(DATE, '19/01/2050', 103) ELSE CONVERT(DATE, @InvoiceDateTo, 103) END
	END

	insert into @tblInvoiceIDs (InvoiceId)
	select h.InvoiceID
	from trnInvoiceHeader h
	where h.InvoiceID = CASE WHEN(ISNULL(InvoiceID, 0)=0) THEN H.InvoiceID ELSE @InvoiceID END
	AND H.InvoiceDate BETWEEN @InvoiceFromdate AND @InvoiceTodate
	and h.InvoiceType = 'I'

	select @TotalInvoices = count(h.InvoiceID) from @tblInvoiceIDs h

	select  @TotalAmount = sum(cast(h.AmountPayable as numeric(11,2)))
	from trnInvoiceHeader h 
	where h.InvoiceID in (select InvoiceId from @tblInvoiceIDs)

	select @TotalPaidAmount = sum(cast(p.PaidAmount as numeric(11,2))) 
	from trnInvoicePayment p
	where p.InvoiceID in (select InvoiceId from @tblInvoiceIDs)

	select @TotalDueAmount = @TotalAmount - @TotalPaidAmount

	SELECT @TotalOverDueAmount = 
		(SELECT ISNULL(sum(ISNULL(cast(p.TotalAmount as numeric(11,2)), 0)), 0) 
			FROM trnInvoiceproduct p 
			WHERE p.InvoiceID in (select p.InvoiceID 
						FROM trnInvoiceHeader h 
						WHERE h.InvoiceID in (select InvoiceId from @tblInvoiceIDs)
						AND h.duedate < GETDATE()
			)
		) - 
		(SELECT ISNULL(sum(ISNULL(cast(p.PaidAmount as numeric(11,2)), 0)), 0) 
			FROM trnInvoicePayment p 
			WHERE p.InvoiceID in (select p.InvoiceID 
						FROM trnInvoiceHeader h 
						WHERE h.InvoiceID in (select InvoiceId from @tblInvoiceIDs)
						AND h.duedate < GETDATE()
			)
		)

	SELECT @TotalAmount TotalAmount, @TotalPaidAmount TotalPaidAmount, @TotalDueAmount TotalDueAmount, 
		@TotalInvoices TotalInvoices, @TotalOverDueInvoices TotalOverDueInvoices, @TotalOverDueAmount TotalOverDueAmount
	
	IF(@IsInvoiceRequiredInDetails = 1)
	BEGIN
		select h.InvoiceID, h.AmountPayable, h.Duedate, h.InvoiceDate
		from trnInvoiceHeader h
		where h.InvoiceID in (select InvoiceId from @tblInvoiceIDs)

		select p.InvoiceID, p.ProductId, p.ProductName, p.ServiceGood, p.SACHCNCode, p.Quantity, p.PricePerUnit, p.TaxOnProduct, p.TotalAmount
		from trnInvoiceproduct p 
		where p.InvoiceID in (select InvoiceId from @tblInvoiceIDs)

		select p.InvoiceID, p.PaymentID, p.PaidAmount, p.PaidOn, p.PaymentMethod, p.PaymentStatus 
		from trnInvoicePayment p
		where p.InvoiceID in (select InvoiceId from @tblInvoiceIDs)
	END
END


GO
