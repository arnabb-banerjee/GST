USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[urMstOrganizationMasterAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[urMstOrganizationMasterAudit](
	[OrganizationID] [bigint] NULL,
	[OrganizationCode] [varchar](50) NULL,
	[DatauniqueID] [int] NULL,
	[OrganizationName] [varchar](50) NULL,
	[ActivityType] [varchar](1) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
