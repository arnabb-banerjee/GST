USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[cusMstcustomerNotesAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cusMstcustomerNotesAudit](
	[CusID] [bigint] NOT NULL,
	[DatauniqueID] [int] NULL,
	[Notes] [varchar](5000) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](10) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[cusMstcustomerNotesAudit] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 1, N'Hi, I am a customer,  Hi, I am a customer,  Hi, I ', CAST(0x0000A96300A12A75 AS DateTime), N'0', CAST(0x0000A96300B9DE9B AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotesAudit] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30049, 1, NULL, CAST(0x0000A9B301173EB8 AS DateTime), N'10033', CAST(0x0000A9B3012B47F8 AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotesAudit] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30049, 2, N'akjfdsah faslkdfjshdflkasjfdh aslfksadjf hsadalkfs', CAST(0x0000A9B3012B4812 AS DateTime), N'10033', CAST(0x0000A9B3012BCF47 AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotesAudit] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30052, 1, N'fsdafjhsak askdjfhskf sakfsjdfhs', CAST(0x0000A9B3014B9C87 AS DateTime), N'10033', CAST(0x0000A9B3014E815C AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotesAudit] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30050, 1, N'asdfjhldska askdfjsh fskfjhdsflksdjfhslk', CAST(0x0000A9B301440C86 AS DateTime), N'10034', CAST(0x0000AB0700AD013C AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotesAudit] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30059, 1, NULL, CAST(0x0000AB0900D645B1 AS DateTime), N'10', CAST(0x0000AB0900D78816 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotesAudit] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30059, 2, N'aaa', CAST(0x0000AB0900D78835 AS DateTime), N'10', CAST(0x0000AB0900D7B82E AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotesAudit] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30059, 3, NULL, CAST(0x0000AB0900D7B82E AS DateTime), N'10', CAST(0x0000AB0901312083 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotesAudit] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30059, 4, N'xyz', CAST(0x0000AB090131208B AS DateTime), N'10', CAST(0x0000AB090143399C AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotesAudit] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30059, 5, N'Test', CAST(0x0000AB090143399C AS DateTime), N'10', CAST(0x0000AB0901597786 AS DateTime), N'10', NULL, NULL)
