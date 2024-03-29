USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[mstBanksAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[mstBanksAudit](
	[Id] [bigint] NOT NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[Name] [varchar](50) NULL,
	[CorpID] [varchar](50) NULL,
	[Address] [varchar](500) NULL,
	[IFSCCode] [varchar](50) NULL,
	[MCRCode] [varchar](50) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](50) NULL,
	[OrganizationID] [bigint] NULL,
	[CreatedOn] [datetime] NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[mstBanksAudit] ADD [CreatedBy] [varchar](50) NULL

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[mstBanksAudit] ([Id], [DatauniqueID], [ActivityType], [Name], [CorpID], [Address], [IFSCCode], [MCRCode], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [OrganizationID], [CreatedOn], [CreatedBy]) VALUES (1, 1, N'D', N'Ss', N'sSsA', N'ADADASDA', N'ASDADAD', N'ADSADADA', N'Y', CAST(0x0000A932015CD08F AS DateTime), N'1', CAST(0x0000A932015ECE1A AS DateTime), N'1', 0, NULL, NULL)
INSERT [dbo].[mstBanksAudit] ([Id], [DatauniqueID], [ActivityType], [Name], [CorpID], [Address], [IFSCCode], [MCRCode], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [OrganizationID], [CreatedOn], [CreatedBy]) VALUES (2, 1, N'A', N'gdgfd', N'gdgfd', N'hfhgf', N'gfhghfh', N'gfhgh', N'T', CAST(0x0000A932015CE07E AS DateTime), N'1', CAST(0x0000A932015EFC43 AS DateTime), N'1', 0, NULL, NULL)
