USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[langMstlanguageMasterAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[langMstlanguageMasterAudit](
	[LanguageId] [int] NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[LanguageName] [varchar](50) NULL,
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
INSERT [dbo].[langMstlanguageMasterAudit] ([LanguageId], [DatauniqueID], [ActivityType], [LanguageName], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (3, 1, N'A', N'Bengali', N'T', CAST(0x0000A93200D9A63D AS DateTime), N'1', CAST(0x0000A93200EBD9F3 AS DateTime), N'1', NULL, NULL)
INSERT [dbo].[langMstlanguageMasterAudit] ([LanguageId], [DatauniqueID], [ActivityType], [LanguageName], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (3, 2, N'D', N'Bengali-Modified', N'T', CAST(0x0000A93200EBD9F3 AS DateTime), N'1', CAST(0x0000A93200ECF978 AS DateTime), N'1', NULL, NULL)
