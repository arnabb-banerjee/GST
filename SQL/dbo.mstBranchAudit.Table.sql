USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[mstBranchAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mstBranchAudit](
	[BranchId] [bigint] NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[OrganizationID] [bigint] NULL,
	[Name] [varchar](250) NULL,
	[Street1] [varchar](500) NULL,
	[Street2] [varchar](500) NULL,
	[City] [varchar](50) NULL,
	[State] [int] NULL,
	[Country] [int] NULL,
	[PIN] [varchar](10) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](50) NULL,
	[IsMainBranch] [varchar](1) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[mstBranchAudit] ([BranchId], [DatauniqueID], [ActivityType], [OrganizationID], [Name], [Street1], [Street2], [City], [State], [Country], [PIN], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [IsMainBranch], [CreatedOn], [CreatedBy]) VALUES (1, 1, N'D', 0, N'adsasd', N'l', N'kjl', N'k', 119, 119, NULL, N'T', CAST(0x0000A9320164B1BE AS DateTime), N'1', CAST(0x0000A932016A3736 AS DateTime), N'1', NULL, NULL, NULL)
