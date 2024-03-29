USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[trnInvoiceproductTax]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trnInvoiceproductTax](
	[InvoiceID] [bigint] IDENTITY(1,1) NOT NULL,
	[DataUniqueID] [int] NULL,
	[TaxDefinationID] [bigint] NULL,
	[ProductId] [varchar](1) NULL,
	[TAX] [numeric](2, 2) NULL,
	[TAXAmount] [numeric](18, 2) NULL,
	[TAXName] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
