USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[cusMstcustomerImage]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cusMstcustomerImage](
	[ImageId] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NULL,
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
SET IDENTITY_INSERT [dbo].[cusMstcustomerImage] ON 

INSERT [dbo].[cusMstcustomerImage] ([ImageId], [CustomerId], [OrganizationID], [DatauniqueID], [FileData], [FileName], [FileType], [LastModifiedOn], [LastModifiedBy], [SEQ], [isMain], [IsActive]) VALUES (1, 10042, NULL, 1, 0x, N'98c54509-d966-4ba6-8167-5a87d369c3a8.png', N'image/png', CAST(0x0000A9CC00EAB63C AS DateTime), N'10', 1, N'N', N'N')
SET IDENTITY_INSERT [dbo].[cusMstcustomerImage] OFF
