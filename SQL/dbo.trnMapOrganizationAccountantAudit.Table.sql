USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[trnMapOrganizationAccountantAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trnMapOrganizationAccountantAudit](
	[ID] [bigint] NULL,
	[DataUniqueID] [int] NULL,
	[OrganizationID] [bigint] NULL,
	[AccountantID] [varchar](50) NULL,
	[ActivityType] [varchar](1) NULL,
	[UserID] [bigint] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [bigint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[trnMapOrganizationAccountantAudit] ([ID], [DataUniqueID], [OrganizationID], [AccountantID], [ActivityType], [UserID], [LastModifiedOn], [LastModifiedBy]) VALUES (3, NULL, NULL, N'10031', NULL, 10, CAST(0x0000A99900775B2E AS DateTime), 10)
INSERT [dbo].[trnMapOrganizationAccountantAudit] ([ID], [DataUniqueID], [OrganizationID], [AccountantID], [ActivityType], [UserID], [LastModifiedOn], [LastModifiedBy]) VALUES (4, NULL, NULL, N'10031', NULL, 10, CAST(0x0000A999007F705F AS DateTime), 10)
INSERT [dbo].[trnMapOrganizationAccountantAudit] ([ID], [DataUniqueID], [OrganizationID], [AccountantID], [ActivityType], [UserID], [LastModifiedOn], [LastModifiedBy]) VALUES (5, NULL, 0, N'10031', NULL, 10, CAST(0x0000A99900833B0F AS DateTime), 10)
INSERT [dbo].[trnMapOrganizationAccountantAudit] ([ID], [DataUniqueID], [OrganizationID], [AccountantID], [ActivityType], [UserID], [LastModifiedOn], [LastModifiedBy]) VALUES (6, NULL, 0, N'10031', NULL, 10, CAST(0x0000A99900834DDC AS DateTime), 10)
