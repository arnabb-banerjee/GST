USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[langMAPlanguageCountry]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[langMAPlanguageCountry](
	[LanguageId] [int] NULL,
	[DatauniqueID] [int] NULL,
	[Country] [int] NULL,
	[Visibility] [varchar](1) NULL,
	[Priority] [int] NULL,
	[LastModifiedBy] [bigint] NULL,
	[LastModifiedOn] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[langMAPlanguageCountry] ON 

INSERT [dbo].[langMAPlanguageCountry] ([LanguageId], [DatauniqueID], [Country], [Visibility], [Priority], [LastModifiedBy], [LastModifiedOn], [CreatedOn], [CreatedBy], [id]) VALUES (2, 1, 8, N'Y', 1, 0, CAST(0x0000A98B0184F5FC AS DateTime), CAST(0x0000A98B0184F5FC AS DateTime), N'0', 1)
SET IDENTITY_INSERT [dbo].[langMAPlanguageCountry] OFF
