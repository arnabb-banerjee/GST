USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[supMstsupplierpreferedMethodsAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[supMstsupplierpreferedMethodsAudit](
	[SupID] [bigint] NOT NULL,
	[DatauniqueID] [int] NULL,
	[PrefferedPaymentMethod] [int] NULL,
	[PrefferedDeliveryMethod] [int] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](10) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
