USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[trnExpenseDeliveredAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trnExpenseDeliveredAudit](
	[DeliveredID] [bigint] NULL,
	[InvoiceID] [bigint] NULL,
	[DataUniqueID] [int] NULL,
	[DeliveredMethod] [varchar](50) NULL,
	[DeliveredOn] [date] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
