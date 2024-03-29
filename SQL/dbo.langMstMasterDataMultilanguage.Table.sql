USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[langMstMasterDataMultilanguage]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[langMstMasterDataMultilanguage](
	[MasterIDField] [varchar](50) NULL,
	[MasterTablePrefix] [varchar](50) NULL,
	[LanguageId] [varchar](50) NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[Value] [nvarchar](max) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[langMstMasterDataMultilanguage] ([MasterIDField], [MasterTablePrefix], [LanguageId], [DatauniqueID], [ActivityType], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (N'11', N'P', N'2', 1, N'A', N'আমি তোমায় ভালবাসি', N'Y', NULL, NULL, CAST(0x0000A98901676F6B AS DateTime), N'0')
