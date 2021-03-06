USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[trnExpenseProduct]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trnExpenseProduct](
	[InvoiceProductID] [bigint] IDENTITY(1,1) NOT NULL,
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
SET IDENTITY_INSERT [dbo].[trnExpenseProduct] ON 

INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (1, 1, 1, 1301, NULL, NULL, NULL, N'12.00', N'10.000', N'0.000', CAST(0x0000AAEA0127A58A AS DateTime), N'10', N'12.00', N'24.00', NULL)
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (5, 20, 1, 1301, NULL, NULL, NULL, N'12.00', N'11.100', N'0.000', CAST(0x0000AAFF0134ACDE AS DateTime), N'10', N'10.00', N'143.20', N'')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (8, 23, 1, 1301, NULL, NULL, NULL, N'20.00', N'11.100', N'0.000', CAST(0x0000AAFF0152ECEB AS DateTime), N'10', N'5.00', N'227.00', N'')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (15, 31, 1, 30, N'jkfhekjwhk22', NULL, NULL, N'12.00', N'11.100', N'0.000', CAST(0x0000AB0000C98E1F AS DateTime), N'10', N'5.00', N'138.20', N'')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (16, 32, 1, 30, N'jkfhekjwhk22', NULL, NULL, N'12.00', N'11.100', N'0.000', CAST(0x0000AB0000C9943E AS DateTime), N'10', N'5.00', N'138.20', N'')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (10015, 10031, 1, 24, N'jkfhekjwhk16', NULL, NULL, N'1.00', N'1.000', N'1.000', CAST(0x0000AB0A0116BD9F AS DateTime), N'10', N'1.00', N'2.00', N'')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (10017, 10035, 1, 23, N'jkfhekjwhk15', NULL, NULL, N'11', N'11', N'121.00', CAST(0x0000AB0F00FF6D29 AS DateTime), N'10', N'11', N'132.00', N'')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (10018, 10036, 1, 26, N'jkfhekjwhk18', NULL, NULL, N'10', N'11.10', N'111.00', CAST(0x0000AB1500C4132D AS DateTime), N'10', N'0', N'111.00', N'')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (10019, 10037, 1, 26, N'jkfhekjwhk18', NULL, NULL, N'10', N'11.10', N'111.00', CAST(0x0000AB1500C41377 AS DateTime), N'10', N'0', N'111.00', N'')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (10042, 10038, 3, 15, N'jkfhekjwhk7', NULL, NULL, N'15', N'11.10', NULL, CAST(0x0000AB1B017EB034 AS DateTime), N'10036', N'11', N'177.50', N'')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (10046, 10039, 15, 16, N'jkfhekjwhk8', NULL, NULL, N'15', N'11.10', NULL, CAST(0x0000AB1D000C7D6A AS DateTime), N'10036', N'45', N'211.50', N'Y')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (10028, 10040, 1, 16, N'jkfhekjwhk8', NULL, NULL, N'12.11', N'11.10', N'134.42', CAST(0x0000AB1B00EC36F7 AS DateTime), N'10036', N'45', N'179.42', N'')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (10029, 10041, 1, 15, N'jkfhekjwhk7', NULL, NULL, N'20', N'50', N'1000.00', CAST(0x0000AB1B0165141C AS DateTime), N'10036', N'10', N'1010.00', N'')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (10030, 10042, 1, 17, N'jkfhekjwhk9', NULL, NULL, N'50', N'29', N'1450.00', CAST(0x0000AB1B016A4DFA AS DateTime), N'10036', N'10', N'1460.00', N'')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (10031, 10043, 1, 17, N'jkfhekjwhk9', NULL, NULL, N'50', N'29', N'1450.00', CAST(0x0000AB1B016A52D8 AS DateTime), N'10036', N'10', N'1460.00', N'')
INSERT [dbo].[trnExpenseProduct] ([InvoiceProductID], [InvoiceID], [DataUniqueID], [ProductId], [ProductName], [ServiceGood], [SACHCNCode], [PricePerUnit], [Quantity], [TotalAmount], [CreatedOn], [CreatedBy], [TaxOnProduct], [AmountIncludeTax], [isBreakupNeed]) VALUES (10016, 10032, 1, 21, N'jkfhekjwhk13', NULL, NULL, N'0.00', N'11.000', N'132.000', CAST(0x0000AB0A011F1E9E AS DateTime), N'10', N'0.00', N'144.00', N'')
SET IDENTITY_INSERT [dbo].[trnExpenseProduct] OFF
