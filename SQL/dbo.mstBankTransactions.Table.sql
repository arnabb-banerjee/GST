USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[mstBankTransactions]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mstBankTransactions](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
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
SET IDENTITY_INSERT [dbo].[mstBankTransactions] ON 

INSERT [dbo].[mstBankTransactions] ([Id], [DatauniqueID], [ActivityType], [TransactionDate], [ChqNo], [Particulars], [Debit], [Credit], [Balance], [InitBr], [Products], [CustomerId], [IsSellExpense], [Tax], [IsActive], [LastModifiedOn], [LastModifiedBy], [OrganizationID], [CreatedOn], [CreatedBy], [TransactionID], [BankName]) VALUES (18, 1, N'A', N'26-02-2018 00:00:00', N'8376add14728', N'JSHDFLSKDAFJS AFASLKJFHS', N'1000.9', N'123432', N'12743629843', N'248', NULL, NULL, NULL, NULL, N'Y', CAST(0x0000A9C500837B75 AS DateTime), N'20037', 30032, CAST(0x0000A9C500837B75 AS DateTime), N'20037', N'a1341234', N'')
INSERT [dbo].[mstBankTransactions] ([Id], [DatauniqueID], [ActivityType], [TransactionDate], [ChqNo], [Particulars], [Debit], [Credit], [Balance], [InitBr], [Products], [CustomerId], [IsSellExpense], [Tax], [IsActive], [LastModifiedOn], [LastModifiedBy], [OrganizationID], [CreatedOn], [CreatedBy], [TransactionID], [BankName]) VALUES (19, 1, N'A', N'10-03-2018 00:00:00', N'8376add14728', N'ASDFKDJFHSDALFKS', N'876878768.98', N'1234323', N'1283423842', N'8768', NULL, NULL, NULL, NULL, N'Y', CAST(0x0000A9C500837F6E AS DateTime), N'20037', 30032, CAST(0x0000A9C500837F6E AS DateTime), N'20037', N'123424', N'')
INSERT [dbo].[mstBankTransactions] ([Id], [DatauniqueID], [ActivityType], [TransactionDate], [ChqNo], [Particulars], [Debit], [Credit], [Balance], [InitBr], [Products], [CustomerId], [IsSellExpense], [Tax], [IsActive], [LastModifiedOn], [LastModifiedBy], [OrganizationID], [CreatedOn], [CreatedBy], [TransactionID], [BankName]) VALUES (20, 1, N'A', N'10-03-2018 00:00:00', N'8376add14728', N'ASKDFHSLFKSAFH', N'87683327.78', N'13423141234', N'1234629384', N'1355', NULL, NULL, NULL, NULL, N'Y', CAST(0x0000A9C500838466 AS DateTime), N'20037', 30032, CAST(0x0000A9C500838466 AS DateTime), N'20037', N'214323', N'')
INSERT [dbo].[mstBankTransactions] ([Id], [DatauniqueID], [ActivityType], [TransactionDate], [ChqNo], [Particulars], [Debit], [Credit], [Balance], [InitBr], [Products], [CustomerId], [IsSellExpense], [Tax], [IsActive], [LastModifiedOn], [LastModifiedBy], [OrganizationID], [CreatedOn], [CreatedBy], [TransactionID], [BankName]) VALUES (21, 1, N'A', N'26-02-2018 00:00:00', N'8376add14728', N'JSHDFLSKDAFJS AFASLKJFHS', N'1000.9', N'123432', N'12743629843', N'248', NULL, NULL, NULL, NULL, N'Y', CAST(0x0000A9D300916507 AS DateTime), N'10033', 20025, CAST(0x0000A9D300916507 AS DateTime), N'10033', N'a1341234', N'')
INSERT [dbo].[mstBankTransactions] ([Id], [DatauniqueID], [ActivityType], [TransactionDate], [ChqNo], [Particulars], [Debit], [Credit], [Balance], [InitBr], [Products], [CustomerId], [IsSellExpense], [Tax], [IsActive], [LastModifiedOn], [LastModifiedBy], [OrganizationID], [CreatedOn], [CreatedBy], [TransactionID], [BankName]) VALUES (22, 1, N'A', N'10-03-2018 00:00:00', N'8376add14728', N'ASDFKDJFHSDALFKS', N'876878768.98', N'1234323', N'1283423842', N'8768', NULL, NULL, NULL, NULL, N'Y', CAST(0x0000A9D30091652A AS DateTime), N'10033', 20025, CAST(0x0000A9D30091652A AS DateTime), N'10033', N'123424', N'')
INSERT [dbo].[mstBankTransactions] ([Id], [DatauniqueID], [ActivityType], [TransactionDate], [ChqNo], [Particulars], [Debit], [Credit], [Balance], [InitBr], [Products], [CustomerId], [IsSellExpense], [Tax], [IsActive], [LastModifiedOn], [LastModifiedBy], [OrganizationID], [CreatedOn], [CreatedBy], [TransactionID], [BankName]) VALUES (23, 1, N'A', N'10-03-2018 00:00:00', N'8376add14728', N'ASKDFHSLFKSAFH', N'87683327.78', N'13423141234', N'1234629384', N'1355', NULL, NULL, NULL, NULL, N'Y', CAST(0x0000A9D30091652F AS DateTime), N'10033', 20025, CAST(0x0000A9D30091652F AS DateTime), N'10033', N'214323', N'')
SET IDENTITY_INSERT [dbo].[mstBankTransactions] OFF
