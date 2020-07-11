USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[urMstUserRegisteredNotes]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[urMstUserRegisteredNotes](
	[UserID] [bigint] NOT NULL,
	[DatauniqueID] [int] NULL,
	[Notes] [varchar](5000) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[urMstUserRegisteredNotes] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30046, 14, N'as s asafsa safsaf', CAST(0x0000AB0E00FED241 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotes] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30047, 15, N'', CAST(0x0000AB1B00109A08 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotes] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20043, 2, N'', CAST(0x0000AB0E01149EE9 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotes] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20045, 4, N'', CAST(0x0000AB0F010CA6A9 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotes] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30053, 2, N'', CAST(0x0000AB1A01661C31 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotes] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30056, 2, N'', CAST(0x0000AB1D0008249F AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotes] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30057, 2, N'', CAST(0x0000AB1D00D4568E AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotes] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (10032, NULL, N'', CAST(0x0000AB1D00D77A41 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotes] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30060, 5, N'', CAST(0x0000AB1D0119C891 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotes] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (10030, 2, N'', CAST(0x0000AB1B0000AAE1 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotes] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30058, 2, N'', CAST(0x0000AB1D00D84152 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[urMstUserRegisteredNotes] ([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30059, 2, N'', CAST(0x0000AB1D00D8ED70 AS DateTime), N'10036', NULL, NULL)
