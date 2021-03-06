USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[trnInvoiceProductAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trnInvoiceProductAudit](
	[InvoiceProductID] [bigint] NOT NULL,
	[InvoiceID] [bigint] NOT NULL,
	[DataUniqueID] [int] NULL,
	[ProductId] [bigint] NULL,
	[ProductName] [varchar](250) NULL,
	[ServiceGood] [varchar](1) NULL,
	[SACHCNCode] [varchar](20) NULL,
	[PricePerUnit] [varchar](50) NULL,
	[Quantity] [varchar](50) NULL,
	[TotalAmount] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[TaxOnProduct] [varchar](50) NULL,
	[AmountIncludeTax] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
