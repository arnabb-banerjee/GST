USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[urMstUserRegisteredNotesAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[urMstUserRegisteredNotesAudit](
	[UserID] [bigint] NOT NULL,
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
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30047, 3, N'', CAST(0x0000AB0E0102E5DB AS DateTime), N'10', CAST(0x0000AB0E01030218 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30047, 4, N'', CAST(0x0000AB0E01030218 AS DateTime), N'10', CAST(0x0000AB0E010330F6 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30047, 5, N'', CAST(0x0000AB0E010330F6 AS DateTime), N'10', CAST(0x0000AB0E01097FD3 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30047, 6, N'', CAST(0x0000AB0E01097FD3 AS DateTime), N'10', CAST(0x0000AB0E010FF18C AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30047, 7, N'', CAST(0x0000AB0E010FF18C AS DateTime), N'10', CAST(0x0000AB0E011042A1 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30047, 8, N'', CAST(0x0000AB0E011042A1 AS DateTime), N'10', CAST(0x0000AB0E01137A08 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30047, 9, N'', CAST(0x0000AB0E01137A08 AS DateTime), N'10', CAST(0x0000AB0E01206069 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30047, 10, N'', CAST(0x0000AB0E01206069 AS DateTime), N'10', CAST(0x0000AB0E0120E118 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30047, 11, N'', CAST(0x0000AB0E0120E118 AS DateTime), N'10', CAST(0x0000AB0E0121ADC6 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30047, 12, N'', CAST(0x0000AB0E0121ADC6 AS DateTime), N'10', CAST(0x0000AB0E01228153 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30047, 13, N'', CAST(0x0000AB0E01228154 AS DateTime), N'10', CAST(0x0000AB1B00012CE4 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30060, 2, N'', CAST(0x0000AB1D0109D679 AS DateTime), N'10036', CAST(0x0000AB1D010DA838 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30060, 3, N'', CAST(0x0000AB1D010DA83F AS DateTime), N'10036', CAST(0x0000AB1D0119104E AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30060, 4, N'', CAST(0x0000AB1D0119104E AS DateTime), N'10036', CAST(0x0000AB1D0119C891 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotesAudit] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30047, 14, N'', CAST(0x0000AB1B00012CE7 AS DateTime), N'10', CAST(0x0000AB1B00109A07 AS DateTime), N'10', NULL, NULL)
