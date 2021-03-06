USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[mstBanks]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mstBanks](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[Name] [varchar](250) NULL,
	[CorpID] [varchar](50) NULL,
	[Address] [varchar](500) NULL,
	[IFSCCode] [varchar](50) NULL,
	[MCRCode] [varchar](50) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[OrganizationID] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[mstBanks] ON 

INSERT [dbo].[mstBanks] ([Id], [DatauniqueID], [ActivityType], [Name], [CorpID], [Address], [IFSCCode], [MCRCode], [IsActive], [LastModifiedOn], [LastModifiedBy], [OrganizationID], [CreatedOn], [CreatedBy]) VALUES (2, 2, N'M', N'gdgfd - MODIFIED', N'gdgfd - MODIFIED', N'hfhgf - MODIFIED', N'gfhghfh - MODIFIED', N'gfhgh - MODIFIED', N'T', CAST(0x0000A932015EFC43 AS DateTime), N'1', 0, NULL, NULL)
INSERT [dbo].[mstBanks] ([Id], [DatauniqueID], [ActivityType], [Name], [CorpID], [Address], [IFSCCode], [MCRCode], [IsActive], [LastModifiedOn], [LastModifiedBy], [OrganizationID], [CreatedOn], [CreatedBy]) VALUES (3, 1, N'A', N'lkasdfjldskfaj', N'kjaslfksj', N'lsdkddfjsalfksa', N'ldkfdjsladfkas', N'lkdjdfldskfsa', N'T', CAST(0x0000A95A0157F55E AS DateTime), N'0', 0, NULL, NULL)
INSERT [dbo].[mstBanks] ([Id], [DatauniqueID], [ActivityType], [Name], [CorpID], [Address], [IFSCCode], [MCRCode], [IsActive], [LastModifiedOn], [LastModifiedBy], [OrganizationID], [CreatedOn], [CreatedBy]) VALUES (4, 1, N'A', N'sadfsdfsd', N'wqerweqrw', N'rtyrtyry', N'ghgfh', N'ghjkjhkh', N'F', CAST(0x0000AADE00D34C3D AS DateTime), N'10', 2, NULL, NULL)
INSERT [dbo].[mstBanks] ([Id], [DatauniqueID], [ActivityType], [Name], [CorpID], [Address], [IFSCCode], [MCRCode], [IsActive], [LastModifiedOn], [LastModifiedBy], [OrganizationID], [CreatedOn], [CreatedBy]) VALUES (5, 1, N'A', N'safsadf', N'asdfsdaf', N'asfsad', N'asdfsda', N'asdfsadfa', N'T', CAST(0x0000AADE012E9DB1 AS DateTime), N'10', 2, NULL, NULL)
INSERT [dbo].[mstBanks] ([Id], [DatauniqueID], [ActivityType], [Name], [CorpID], [Address], [IFSCCode], [MCRCode], [IsActive], [LastModifiedOn], [LastModifiedBy], [OrganizationID], [CreatedOn], [CreatedBy]) VALUES (6, 1, N'A', N'1242421', N'1421421', N'124214', N'1234214', N'12342121', N'T', CAST(0x0000AADE0130BE43 AS DateTime), N'10', 10, NULL, NULL)
SET IDENTITY_INSERT [dbo].[mstBanks] OFF
