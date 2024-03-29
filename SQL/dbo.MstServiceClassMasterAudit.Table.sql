USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[MstServiceClassMasterAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MstServiceClassMasterAudit](
	[ServiceClassId] [int] NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[ServiceClassName] [varchar](50) NULL,
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
INSERT [dbo].[MstServiceClassMasterAudit] ([ServiceClassId], [DatauniqueID], [ActivityType], [ServiceClassName], [IsActive], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (1, 1, N'A', N'Class A', N'Y', CAST(0x0000A970015FB4A1 AS DateTime), N'', CAST(0x0000A970015FC919 AS DateTime), N'', NULL, NULL)
