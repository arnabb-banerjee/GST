USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[prMAPOrganizationproductImage_Audit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prMAPOrganizationproductImage_Audit](
	[ImageId] [bigint] NOT NULL,
	[OrganizationproductId] [bigint] NOT NULL,
	[ProductId] [bigint] NULL,
	[OrganizationID] [bigint] NULL,
	[DatauniqueID] [int] NULL,
	[FileData] [image] NULL,
	[FileName] [varchar](200) NULL,
	[FileType] [varchar](150) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[SEQ] [int] NULL,
	[isMain] [varchar](1) NULL,
	[IsActive] [varchar](1) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[prMAPOrganizationproductImage_Audit] ([ImageId], [OrganizationproductId], [ProductId], [OrganizationID], [DatauniqueID], [FileData], [FileName], [FileType], [LastModifiedOn], [LastModifiedBy], [SEQ], [isMain], [IsActive]) VALUES (3, 6, 0, NULL, 2, 0x, N'caa0b98b-26b1-446b-a408-801ddf2caf52.jpg', N'image/jpeg', CAST(0x0000A9B601809169 AS DateTime), N'10', 2, N'N', N'N')
INSERT [dbo].[prMAPOrganizationproductImage_Audit] ([ImageId], [OrganizationproductId], [ProductId], [OrganizationID], [DatauniqueID], [FileData], [FileName], [FileType], [LastModifiedOn], [LastModifiedBy], [SEQ], [isMain], [IsActive]) VALUES (2, 6, 0, NULL, 1, 0x, N'9ba5e364-7050-47b8-acf3-e123509d1fda.jpg', N'image/jpeg', CAST(0x0000A9B6017EDE5B AS DateTime), N'10', 1, N'N', N'N')
INSERT [dbo].[prMAPOrganizationproductImage_Audit] ([ImageId], [OrganizationproductId], [ProductId], [OrganizationID], [DatauniqueID], [FileData], [FileName], [FileType], [LastModifiedOn], [LastModifiedBy], [SEQ], [isMain], [IsActive]) VALUES (5, 6, 0, NULL, 2, 0x, N'9126cc35-3eec-4c65-b2ee-78be2c513427.jpg', N'image/jpeg', CAST(0x0000A9B70096D7DD AS DateTime), N'10', 2, N'N', N'N')
