USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[prMstproductMasterAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prMstproductMasterAudit](
	[ProductId] [bigint] NULL,
	[CategoryId] [bigint] NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[ProductName] [varchar](5000) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CountryID] [bigint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[prMstproductMasterAudit] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (1, 0, 1, N'A', N'jgjhj', N'Y', CAST(0x0000A9320183A591 AS DateTime), N'1', CAST(0x0000A93300987260 AS DateTime), N'1', NULL, NULL, NULL)
INSERT [dbo].[prMstproductMasterAudit] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (1, 0, 2, N'M', N'jgjhj - modified', N'Y', CAST(0x0000A93300987268 AS DateTime), N'1', CAST(0x0000A93300988572 AS DateTime), N'1', NULL, NULL, NULL)
INSERT [dbo].[prMstproductMasterAudit] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (11, 1191, 1, N'D', N'jkfhekjwhk3', NULL, CAST(0x0000A96700326D87 AS DateTime), N'10', CAST(0x0000A9C500CB124B AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMasterAudit] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (12, 1191, 1, N'D', N'jkfhekjwhk4', NULL, CAST(0x0000A96700326D87 AS DateTime), N'10', CAST(0x0000A9C500CB1BA4 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMasterAudit] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (13, 1191, 1, N'A', N'jkfhekjwhk5', NULL, CAST(0x0000A96700326D87 AS DateTime), N'10', CAST(0x0000AAC201101DE6 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMasterAudit] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (13, 1191, 2, N'M', N'jkfhekjwhk5', N'Y', CAST(0x0000AAC201101DEF AS DateTime), N'10', CAST(0x0000AAC20110B1A9 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMasterAudit] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (15, 1191, 1, N'A', N'jkfhekjwhk7', NULL, CAST(0x0000A96700326D87 AS DateTime), N'10', CAST(0x0000AAC20117A06C AS DateTime), N'10', NULL, NULL, 101)
