USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[urMstOrganizationBranch]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[urMstOrganizationBranch](
	[BranchID] [bigint] IDENTITY(1,1) NOT NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[OrganizationID] [bigint] NULL,
	[BranchName] [varchar](50) NULL,
	[IsMainBranch] [varchar](1) NULL,
	[Street1] [varchar](500) NULL,
	[Street2] [varchar](500) NULL,
	[City] [varchar](10) NULL,
	[State] [varchar](10) NULL,
	[Country] [varchar](10) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[BusinessLocationCountryID] [bigint] NULL,
	[BusinessLocationStateID] [bigint] NULL,
	[GSTIN] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
