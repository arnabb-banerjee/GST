USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[langMstlanguageMaster]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[langMstlanguageMaster](
	[LanguageId] [int] IDENTITY(1,1) NOT NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[LanguageName] [varchar](50) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[langMstlanguageMaster] ON 

INSERT [dbo].[langMstlanguageMaster] ([LanguageId], [DatauniqueID], [ActivityType], [LanguageName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (1, 1, N'A', N'Spanish', N'Y', CAST(0x0000A96A00A554A2 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[langMstlanguageMaster] ([LanguageId], [DatauniqueID], [ActivityType], [LanguageName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (2, 1, N'A', N'Hindi', N'Y', CAST(0x0000A96A00A57EF0 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[langMstlanguageMaster] ([LanguageId], [DatauniqueID], [ActivityType], [LanguageName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (3, 1, N'A', N'gfhgfh', N'Y', CAST(0x0000A98801895A78 AS DateTime), N'', NULL, NULL)
SET IDENTITY_INSERT [dbo].[langMstlanguageMaster] OFF
