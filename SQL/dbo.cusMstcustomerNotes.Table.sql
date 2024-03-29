USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[cusMstcustomerNotes]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cusMstcustomerNotes](
	[CusID] [bigint] NOT NULL,
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
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (10042, 2, NULL, CAST(0x0000A96300B9DE9C AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (10043, 1, NULL, CAST(0x0000A96300BA9E4B AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20042, 1, NULL, CAST(0x0000A96301698265 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20043, 1, NULL, CAST(0x0000A971017E3779 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20047, 1, NULL, CAST(0x0000A9850070EBC7 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30047, 1, NULL, CAST(0x0000A98D0137EF93 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30048, 1, NULL, CAST(0x0000A98D01381A82 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30049, 3, NULL, CAST(0x0000A9B3012BCF47 AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30050, 2, NULL, CAST(0x0000AB0700AD0155 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30051, 1, NULL, CAST(0x0000A9B301481141 AS DateTime), N'10034', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30053, 1, NULL, CAST(0x0000AA9600F5C976 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30054, 1, NULL, CAST(0x0000AA9600F60C2B AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30055, 1, NULL, CAST(0x0000AA9601056096 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30056, 1, NULL, CAST(0x0000AAD3013F52BD AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30057, 1, NULL, CAST(0x0000AB0700B6174B AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30058, 1, NULL, CAST(0x0000AB0700C201E1 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30059, 6, N'Abc', CAST(0x0000AB0901597786 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (40059, 1, NULL, CAST(0x0000AB19002367D0 AS DateTime), N'30052', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (40060, 1, NULL, CAST(0x0000AB19016413E8 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (40061, 1, NULL, CAST(0x0000AB1B00E3887A AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerNotes] ([CusID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30052, 2, NULL, CAST(0x0000A9B3014E8167 AS DateTime), N'10033', NULL, NULL)
