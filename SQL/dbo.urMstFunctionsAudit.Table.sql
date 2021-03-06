USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[urMstFunctionsAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[urMstFunctionsAudit](
	[FunctionId] [bigint] NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[FunctionName] [varchar](250) NULL,
	[IsForMembership] [varchar](1) NULL,
	[IsForModerate] [varchar](1) NULL,
	[IsDesignation] [varchar](1) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](50) NULL,
	[ApplicableRoles] [varchar](500) NULL,
	[IsDefaultForRegisteredUser] [varchar](1) NULL,
	[IsDefaultForModerateUser] [varchar](1) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[urMstFunctionsAudit] ([FunctionId], [DatauniqueID], [ActivityType], [FunctionName], [IsForMembership], [IsForModerate], [IsDesignation], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [ApplicableRoles], [IsDefaultForRegisteredUser], [IsDefaultForModerateUser], [CreatedOn], [CreatedBy]) VALUES (2, 1, N'A', N'System Administrator', N'', N'Y', N'', N'T', CAST(0x0000A940012C6978 AS DateTime), N'-1', CAST(0x0000A951000D6F02 AS DateTime), N'0', N'5,6,7,8,9,10,11,12,13,14,15,16,17,18', NULL, NULL, NULL, NULL)
INSERT [dbo].[urMstFunctionsAudit] ([FunctionId], [DatauniqueID], [ActivityType], [FunctionName], [IsForMembership], [IsForModerate], [IsDesignation], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [ApplicableRoles], [IsDefaultForRegisteredUser], [IsDefaultForModerateUser], [CreatedOn], [CreatedBy]) VALUES (1, 1, N'A', N'basic User', N'Y', N'', N'', N'T', CAST(0x0000A940012A94AB AS DateTime), N'-1', CAST(0x0000A97200043847 AS DateTime), N'0', N'5,6,20', NULL, NULL, NULL, NULL)
INSERT [dbo].[urMstFunctionsAudit] ([FunctionId], [DatauniqueID], [ActivityType], [FunctionName], [IsForMembership], [IsForModerate], [IsDesignation], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [ApplicableRoles], [IsDefaultForRegisteredUser], [IsDefaultForModerateUser], [CreatedOn], [CreatedBy]) VALUES (1, 2, N'M', N'basic User', N'Y', N'', N'', N'T', CAST(0x0000A972000438B8 AS DateTime), N'0', CAST(0x0000A97200046A7C AS DateTime), N'0', N'5,6,8,10,14,19,20', N'N', N'N', NULL, NULL)
INSERT [dbo].[urMstFunctionsAudit] ([FunctionId], [DatauniqueID], [ActivityType], [FunctionName], [IsForMembership], [IsForModerate], [IsDesignation], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [ApplicableRoles], [IsDefaultForRegisteredUser], [IsDefaultForModerateUser], [CreatedOn], [CreatedBy]) VALUES (1, 3, N'M', N'basic User', N'Y', N'', N'', N'T', CAST(0x0000A97200046A8C AS DateTime), N'0', CAST(0x0000AB1600C1757F AS DateTime), N'10', N'5,6,8,9,10,14,19,20', N'Y', N'N', NULL, NULL)
