USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[supMstsupplierOpeningBalanceAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[supMstsupplierOpeningBalanceAudit](
	[SupID] [bigint] NULL,
	[OpeningBalance] [numeric](18, 2) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AsOfDate] [date] NULL,
	[AuditOperationBy] [varchar](10) NULL,
	[AuditOperationOn] [date] NULL,
	[DatauniqueID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
