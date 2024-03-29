USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[trnExpensePaymentAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trnExpensePaymentAudit](
	[PaymentID] [bigint] NULL,
	[InvoiceID] [bigint] NULL,
	[DataUniqueID] [int] NULL,
	[PaymentMethod] [varchar](50) NULL,
	[PaidAmount] [varchar](30) NULL,
	[DueAmount] [varchar](30) NULL,
	[DueOn] [date] NULL,
	[ProductId] [varchar](1) NULL,
	[TAX] [varchar](30) NULL,
	[TAXAmount] [varchar](30) NULL,
	[TAXName] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[PaymentStatus] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
