USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[UrMAPOrganizationBranchUserRole]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UrMAPOrganizationBranchUserRole](
	[UserID] [bigint] NULL,
	[DataUniqueID] [bigint] NULL,
	[RoleID] [bigint] NULL,
	[OrganizationID] [bigint] NULL,
	[BranchID] [bigint] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
