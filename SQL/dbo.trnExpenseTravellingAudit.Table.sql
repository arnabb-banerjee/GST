USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[trnExpenseTravellingAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trnExpenseTravellingAudit](
	[ExpenseID] [bigint] NOT NULL,
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
