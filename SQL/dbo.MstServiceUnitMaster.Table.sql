USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[MstServiceUnitMaster]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MstServiceUnitMaster](
	[ServiceUnitId] [int] IDENTITY(1,1) NOT NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[ServiceUnitName] [varchar](50) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[MstServiceUnitMaster] ON 

INSERT [dbo].[MstServiceUnitMaster] ([ServiceUnitId], [DatauniqueID], [ActivityType], [ServiceUnitName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (1, 1, N'A', N'KG', N'Y', CAST(0x0000A970015F7456 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[MstServiceUnitMaster] ([ServiceUnitId], [DatauniqueID], [ActivityType], [ServiceUnitName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (2, 3, N'M', N'Pcs', N'Y', CAST(0x0000AB0D00CF72FF AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[MstServiceUnitMaster] ([ServiceUnitId], [DatauniqueID], [ActivityType], [ServiceUnitName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (3, 1, N'A', N'Mtr', N'Y', CAST(0x0000AB0D00CF9309 AS DateTime), N'10', NULL, NULL)
SET IDENTITY_INSERT [dbo].[MstServiceUnitMaster] OFF
