USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[urMstFunctions]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[urMstFunctions](
	[FunctionId] [bigint] IDENTITY(1,1) NOT NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[FunctionName] [varchar](250) NULL,
	[IsForMembership] [varchar](1) NULL,
	[IsForModerate] [varchar](1) NULL,
	[IsDesignation] [varchar](1) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[ApplicableRoles] [varchar](500) NULL,
	[IsDefaultForRegisteredUser] [varchar](1) NULL,
	[IsDefaultForModerateUser] [varchar](1) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[urMstFunctions] ON 

INSERT [dbo].[urMstFunctions] ([FunctionId], [DatauniqueID], [ActivityType], [FunctionName], [IsForMembership], [IsForModerate], [IsDesignation], [IsActive], [LastModifiedOn], [LastModifiedBy], [ApplicableRoles], [IsDefaultForRegisteredUser], [IsDefaultForModerateUser], [CreatedOn], [CreatedBy]) VALUES (1, 4, N'M', N'Customers', N'Y', N'', N'', N'T', CAST(0x0000AB1600C1757F AS DateTime), N'10', N'5,6,8,9,10,14,19,20', N'Y', N'N', NULL, NULL)
INSERT [dbo].[urMstFunctions] ([FunctionId], [DatauniqueID], [ActivityType], [FunctionName], [IsForMembership], [IsForModerate], [IsDesignation], [IsActive], [LastModifiedOn], [LastModifiedBy], [ApplicableRoles], [IsDefaultForRegisteredUser], [IsDefaultForModerateUser], [CreatedOn], [CreatedBy]) VALUES (2, 2, N'M', N'System Administrator', N'', N'Y', N'', N'T', CAST(0x0000A951000D6FDD AS DateTime), N'0', N'5,6,7,8,9,10,11,12,13,14,15,16,17,19,18', NULL, NULL, NULL, NULL)
INSERT [dbo].[urMstFunctions] ([FunctionId], [DatauniqueID], [ActivityType], [FunctionName], [IsForMembership], [IsForModerate], [IsDesignation], [IsActive], [LastModifiedOn], [LastModifiedBy], [ApplicableRoles], [IsDefaultForRegisteredUser], [IsDefaultForModerateUser], [CreatedOn], [CreatedBy]) VALUES (3, 1, N'A', N'assdfsakjf', N'', N'Y', N'Y', N'F', CAST(0x0000A948018086BE AS DateTime), N'-1', N'5,8,9,10,11,12', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[urMstFunctions] OFF
