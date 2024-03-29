USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[trnExpenseTravelling]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trnExpenseTravelling](
	[ExpenseID] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceProductID] [bigint] NOT NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[Distance] [varchar](150) NULL,
	[Price] [varchar](30) NULL,
	[Remarks] [varchar](300) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[trnExpenseTravelling] ON 

INSERT [dbo].[trnExpenseTravelling] ([ExpenseID], [InvoiceProductID], [FromDate], [ToDate], [Distance], [Price], [Remarks]) VALUES (1, 1, CAST(0x5D400B00 AS Date), CAST(0x5D400B00 AS Date), N'12', N'12.11', N'ok')
INSERT [dbo].[trnExpenseTravelling] ([ExpenseID], [InvoiceProductID], [FromDate], [ToDate], [Distance], [Price], [Remarks]) VALUES (20, 5, CAST(0x5D400B00 AS Date), CAST(0x7B400B00 AS Date), N'11', N'12.11', N'ok')
INSERT [dbo].[trnExpenseTravelling] ([ExpenseID], [InvoiceProductID], [FromDate], [ToDate], [Distance], [Price], [Remarks]) VALUES (23, 8, CAST(0x5D400B00 AS Date), CAST(0x7B400B00 AS Date), N'333', N'12.11', N'ok')
INSERT [dbo].[trnExpenseTravelling] ([ExpenseID], [InvoiceProductID], [FromDate], [ToDate], [Distance], [Price], [Remarks]) VALUES (31, 15, CAST(0x5D400B00 AS Date), CAST(0x7B400B00 AS Date), N'10lm', N'1000.00', N'ok')
INSERT [dbo].[trnExpenseTravelling] ([ExpenseID], [InvoiceProductID], [FromDate], [ToDate], [Distance], [Price], [Remarks]) VALUES (32, 15, CAST(0x5D400B00 AS Date), CAST(0x7B400B00 AS Date), N'50 km', N'200.00', N'ok1')
INSERT [dbo].[trnExpenseTravelling] ([ExpenseID], [InvoiceProductID], [FromDate], [ToDate], [Distance], [Price], [Remarks]) VALUES (33, 16, CAST(0x5D400B00 AS Date), CAST(0x7B400B00 AS Date), N'10lm', N'1000.00', N'ok')
INSERT [dbo].[trnExpenseTravelling] ([ExpenseID], [InvoiceProductID], [FromDate], [ToDate], [Distance], [Price], [Remarks]) VALUES (34, 16, CAST(0x5D400B00 AS Date), CAST(0x7B400B00 AS Date), N'50 km', N'200.00', N'ok1')
SET IDENTITY_INSERT [dbo].[trnExpenseTravelling] OFF
