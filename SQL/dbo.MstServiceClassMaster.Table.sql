USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[MstServiceClassMaster]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MstServiceClassMaster](
	[ServiceClassId] [int] IDENTITY(1,1) NOT NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[ServiceClassName] [varchar](50) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[MstServiceClassMaster] ON 

INSERT [dbo].[MstServiceClassMaster] ([ServiceClassId], [DatauniqueID], [ActivityType], [ServiceClassName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (1, 2, N'M', N'Class A- Modified', N'Y', CAST(0x0000A970015FC919 AS DateTime), N'', NULL, NULL)
SET IDENTITY_INSERT [dbo].[MstServiceClassMaster] OFF
