USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[OrganizationCustSuppMappingAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrganizationCustSuppMappingAudit](
	[OrganizationID] [bigint] NULL,
	[UserID] [bigint] NULL,
	[UserType] [varchar](1) NULL,
	[DatauniqueID] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
