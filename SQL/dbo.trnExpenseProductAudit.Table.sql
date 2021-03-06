USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[trnExpenseProductAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trnExpenseProductAudit](
	[InvoiceProductID] [bigint] NOT NULL,
	[InvoiceID] [bigint] NOT NULL,
	[DataUniqueID] [int] NULL,
	[ProductId] [bigint] NULL,
	[ProductName] [varchar](250) NULL,
	[ServiceGood] [varchar](1) NULL,
	[SACHCNCode] [varchar](20) NULL,
	[PricePerUnit] [varchar](30) NULL,
	[Quantity] [varchar](30) NULL,
	[TotalAmount] [varchar](30) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[TaxOnProduct] [varchar](30) NULL,
	[AmountIncludeTax] [varchar](30) NULL,
	[isBreakupNeed] [varchar](1) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
