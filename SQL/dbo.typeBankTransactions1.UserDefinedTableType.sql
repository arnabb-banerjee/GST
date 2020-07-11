USE [GST_DEV_V2]
GO
/****** Object:  UserDefinedTableType [dbo].[typeBankTransactions1]    Script Date: 08-12-2019 17:51:43 ******/
CREATE TYPE [dbo].[typeBankTransactions1] AS TABLE(
	[TransactionDate] [varchar](30) NULL,
	[ChqNo] [varchar](50) NULL,
	[Particulars] [varchar](500) NULL,
	[Debit] [varchar](21) NULL,
	[Credit] [varchar](21) NULL,
	[Balance] [varchar](21) NULL,
	[InitBr] [varchar](21) NULL
)
GO
