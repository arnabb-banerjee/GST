USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[mstBankTransactionsAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mstBankTransactionsAudit](
	[Id] [bigint] NOT NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[TransactionDate] [varchar](50) NULL,
	[ChqNo] [varchar](50) NULL,
	[Particulars] [varchar](500) NULL,
	[Debit] [varchar](50) NULL,
	[Credit] [varchar](50) NULL,
	[Balance] [varchar](50) NULL,
	[InitBr] [varchar](50) NULL,
	[Products] [varchar](500) NULL,
	[CustomerId] [bigint] NULL,
	[IsSellExpense] [varchar](50) NULL,
	[Tax] [varchar](500) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[OrganizationID] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[TransactionID] [varchar](50) NULL,
	[BankName] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
