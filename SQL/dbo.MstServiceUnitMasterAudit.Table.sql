USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[MstServiceUnitMasterAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MstServiceUnitMasterAudit](
	[ServiceUnitId] [int] NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[ServiceUnitName] [varchar](50) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[MstServiceUnitMasterAudit] ([ServiceUnitId], [DatauniqueID], [ActivityType], [ServiceUnitName], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (2, 1, N'A', N'', N'Y', CAST(0x0000A9750004B395 AS DateTime), N'', CAST(0x0000AB0D00CF727E AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[MstServiceUnitMasterAudit] ([ServiceUnitId], [DatauniqueID], [ActivityType], [ServiceUnitName], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (2, 2, N'M', N'Pcs', N'Y', CAST(0x0000AB0D00CF7290 AS DateTime), N'10', CAST(0x0000AB0D00CF72FF AS DateTime), N'10', NULL, NULL)
