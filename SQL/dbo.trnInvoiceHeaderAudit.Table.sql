USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[trnInvoiceHeaderAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trnInvoiceHeaderAudit](
	[InvoiceID] [bigint] IDENTITY(1,1) NOT NULL,
	[DataUniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[IsCancelled] [varchar](1) NULL,
	[IsReturned] [varchar](1) NULL,
	[ReturnedOn] [datetime] NULL,
	[InvoiceType] [varchar](1) NULL,
	[InvoiceDate] [datetime] NULL,
	[CusID] [bigint] NULL,
	[CusName] [varchar](100) NULL,
	[CusEmail] [varchar](250) NULL,
	[CusAddress] [varchar](500) NULL,
	[CusState] [int] NULL,
	[CusCountry] [int] NULL,
	[BranchID] [varchar](500) NULL,
	[BranchAddress] [varchar](500) NULL,
	[BranchState] [int] NULL,
	[BranchCountry] [int] NULL,
	[TotalAmount] [numeric](18, 2) NULL,
	[Discount] [numeric](18, 2) NULL,
	[DiscountFormat] [varchar](10) NULL,
	[AmountAfterDiscount] [numeric](18, 2) NULL,
	[AmountPayable] [numeric](18, 2) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [bigint] NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[Duedate] [datetime] NULL,
	[InvoiceNumber] [varchar](50) NULL,
	[OrganizationId] [bigint] NULL,
	[ShipAddess] [varchar](500) NULL,
	[ShipCity] [varchar](50) NULL,
	[ShipState] [int] NULL,
	[ShipCountry] [int] NULL,
	[ChangedCurrency] [varchar](10) NULL,
	[ConversionRate] [varchar](10) NULL,
	[AmountDue] [varchar](20) NULL,
	[PrevConversionRate] [varchar](20) NULL,
	[SumAmount] [varchar](20) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[trnInvoiceHeaderAudit] ON 

INSERT [dbo].[trnInvoiceHeaderAudit] ([InvoiceID], [DataUniqueID], [ActivityType], [IsCancelled], [IsReturned], [ReturnedOn], [InvoiceType], [InvoiceDate], [CusID], [CusName], [CusEmail], [CusAddress], [CusState], [CusCountry], [BranchID], [BranchAddress], [BranchState], [BranchCountry], [TotalAmount], [Discount], [DiscountFormat], [AmountAfterDiscount], [AmountPayable], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [Duedate], [InvoiceNumber], [OrganizationId], [ShipAddess], [ShipCity], [ShipState], [ShipCountry], [ChangedCurrency], [ConversionRate], [AmountDue], [PrevConversionRate], [SumAmount]) VALUES (5, 1, N'A', N'Y', N'Y', CAST(0x0000A997015975E1 AS DateTime), N'B', CAST(0x0000A99700000000 AS DateTime), 20044, NULL, NULL, NULL, NULL, NULL, N'0', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A9C9018308BA AS DateTime), 10, CAST(0x0000A997015975E1 AS DateTime), N'0', CAST(0x0000000000000000 AS DateTime), N'5E2B8020-E5F1-478B-870C-A96748CE55A5', NULL, NULL, NULL, NULL, NULL, N'BZD', N'12.11', NULL, NULL, NULL)
INSERT [dbo].[trnInvoiceHeaderAudit] ([InvoiceID], [DataUniqueID], [ActivityType], [IsCancelled], [IsReturned], [ReturnedOn], [InvoiceType], [InvoiceDate], [CusID], [CusName], [CusEmail], [CusAddress], [CusState], [CusCountry], [BranchID], [BranchAddress], [BranchState], [BranchCountry], [TotalAmount], [Discount], [DiscountFormat], [AmountAfterDiscount], [AmountPayable], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [Duedate], [InvoiceNumber], [OrganizationId], [ShipAddess], [ShipCity], [ShipState], [ShipCountry], [ChangedCurrency], [ConversionRate], [AmountDue], [PrevConversionRate], [SumAmount]) VALUES (7, 2, N'M', N'N', N'N', NULL, N'B', CAST(0x0000A9E700000000 AS DateTime), 20044, NULL, NULL, NULL, NULL, NULL, N'0', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(166.00 AS Numeric(18, 2)), CAST(0x0000A9C9018B71BB AS DateTime), 10, CAST(0x0000AAFB0010D02A AS DateTime), 10, CAST(0x0000A99701819167 AS DateTime), N'0', CAST(0x0000A9E7018B71BB AS DateTime), N'4BA70482-9C7F-4C73-B8F9-8B8011A2274C', 4, NULL, NULL, NULL, NULL, N'EUR', N'', N'501.29999999999995', N'', N'2228.649')
INSERT [dbo].[trnInvoiceHeaderAudit] ([InvoiceID], [DataUniqueID], [ActivityType], [IsCancelled], [IsReturned], [ReturnedOn], [InvoiceType], [InvoiceDate], [CusID], [CusName], [CusEmail], [CusAddress], [CusState], [CusCountry], [BranchID], [BranchAddress], [BranchState], [BranchCountry], [TotalAmount], [Discount], [DiscountFormat], [AmountAfterDiscount], [AmountPayable], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [Duedate], [InvoiceNumber], [OrganizationId], [ShipAddess], [ShipCity], [ShipState], [ShipCountry], [ChangedCurrency], [ConversionRate], [AmountDue], [PrevConversionRate], [SumAmount]) VALUES (10008, 1, N'A', N'N', N'N', NULL, N'B', CAST(0x0000AB1B00000000 AS DateTime), 30054, NULL, NULL, NULL, NULL, NULL, N'0', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(515.00 AS Numeric(18, 2)), NULL, NULL, CAST(0x0000AB1B00C5D645 AS DateTime), 10036, CAST(0x0000AB1B00C1A91A AS DateTime), N'10036', CAST(0x0000000000000000 AS DateTime), N'4180ABF8-39B8-4914-B9AC-D25E13DEB54F', 20031, NULL, NULL, NULL, NULL, N'', N'', N'', N'', N'515')
INSERT [dbo].[trnInvoiceHeaderAudit] ([InvoiceID], [DataUniqueID], [ActivityType], [IsCancelled], [IsReturned], [ReturnedOn], [InvoiceType], [InvoiceDate], [CusID], [CusName], [CusEmail], [CusAddress], [CusState], [CusCountry], [BranchID], [BranchAddress], [BranchState], [BranchCountry], [TotalAmount], [Discount], [DiscountFormat], [AmountAfterDiscount], [AmountPayable], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [Duedate], [InvoiceNumber], [OrganizationId], [ShipAddess], [ShipCity], [ShipState], [ShipCountry], [ChangedCurrency], [ConversionRate], [AmountDue], [PrevConversionRate], [SumAmount]) VALUES (6, 1, N'A', N'N', N'N', NULL, N'B', CAST(0x0000A99700000000 AS DateTime), 10043, NULL, NULL, NULL, NULL, NULL, N'0', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(166.00 AS Numeric(18, 2)), NULL, NULL, CAST(0x0000A9C9018B71AC AS DateTime), 10, CAST(0x0000A99701819167 AS DateTime), N'0', CAST(0x0000000000000000 AS DateTime), N'4BA70482-9C7F-4C73-B8F9-8B8011A2274C', NULL, NULL, NULL, NULL, NULL, N'EUR', N'1', N'501.29999999999995', N'', N'667.3')
INSERT [dbo].[trnInvoiceHeaderAudit] ([InvoiceID], [DataUniqueID], [ActivityType], [IsCancelled], [IsReturned], [ReturnedOn], [InvoiceType], [InvoiceDate], [CusID], [CusName], [CusEmail], [CusAddress], [CusState], [CusCountry], [BranchID], [BranchAddress], [BranchState], [BranchCountry], [TotalAmount], [Discount], [DiscountFormat], [AmountAfterDiscount], [AmountPayable], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [Duedate], [InvoiceNumber], [OrganizationId], [ShipAddess], [ShipCity], [ShipState], [ShipCountry], [ChangedCurrency], [ConversionRate], [AmountDue], [PrevConversionRate], [SumAmount]) VALUES (10011, 1, N'A', N'N', N'N', NULL, N'B', CAST(0x0000AB1B00000000 AS DateTime), 40060, NULL, NULL, NULL, NULL, NULL, N'0', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(280.00 AS Numeric(18, 2)), NULL, NULL, CAST(0x0000AB1B00D1CD82 AS DateTime), 10036, CAST(0x0000AB1B00C48E45 AS DateTime), N'10036', CAST(0x0000000000000000 AS DateTime), N'C59E4B74-4DDB-4C19-B45C-75499023BD03', 20031, NULL, NULL, NULL, NULL, N'', N'', N'', N'', N'280')
SET IDENTITY_INSERT [dbo].[trnInvoiceHeaderAudit] OFF
