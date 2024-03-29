USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[MstServiceTypeMaster]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MstServiceTypeMaster](
	[ServiceTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[ServiceTypeName] [varchar](50) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[MstServiceTypeMaster] ON 

INSERT [dbo].[MstServiceTypeMaster] ([ServiceTypeId], [DatauniqueID], [ActivityType], [ServiceTypeName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (1, 1, N'A', N'Fast food service', N'Y', CAST(0x0000A970015E7AA1 AS DateTime), N'', NULL, NULL)
SET IDENTITY_INSERT [dbo].[MstServiceTypeMaster] OFF
