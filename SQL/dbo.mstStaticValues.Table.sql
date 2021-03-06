USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[mstStaticValues]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mstStaticValues](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[Key] [varchar](50) NULL,
	[Value] [varchar](250) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[mstStaticValues] ON 

INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (1, 1, N'A', N'PaymentMethod', N'Net Banking', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (2, 1, N'A', N'PaymentMethod', N'Debit Card', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (3, 1, N'A', N'PaymentMethod', N'Credit Card', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (4, 1, N'A', N'PaymentMethod', N'Cheque', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (5, 1, N'A', N'PaymentMethod', N'Cash', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (6, 1, N'A', N'DeliveryMethod', N'Print', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (7, 1, N'A', N'DeliveryMethod', N'Email', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (8, 1, N'A', N'UserNameTitle', N'Mr.', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (9, 1, N'A', N'UserNameTitle', N'Miss.', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (10, 1, N'A', N'UserNameTitle', N'Mrs.', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (11, 1, N'A', N'UserNameTitle', N'Ms.', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (12, 1, N'A', N'UserNameSafix', N'Jr.', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (13, 1, N'A', N'UserNameTitle', N'Engg.', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (14, 1, N'A', N'UserNameTitle', N'Dr.', N'Y', CAST(0x0000A92100000000 AS DateTime), N'1')
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (25, 1, N'A', N'GSTRegistrationType', N'SZN', N'Y', CAST(0x0000A92100000000 AS DateTime), NULL)
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (21, 1, N'A', N'GSTRegistrationType', N'GST Regitered - Regular
', N'Y', CAST(0x0000A92100000000 AS DateTime), NULL)
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (22, 1, N'A', N'GSTRegistrationType', N'Overseas
', N'Y', CAST(0x0000A92100000000 AS DateTime), NULL)
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (23, 1, N'A', N'GSTRegistrationType', N'GST Ungistered
', N'Y', CAST(0x0000A92100000000 AS DateTime), NULL)
INSERT [dbo].[mstStaticValues] ([Id], [DatauniqueID], [ActivityType], [Key], [Value], [IsActive], [LastModifiedOn], [LastModifiedBy]) VALUES (24, 1, N'A', N'GSTRegistrationType', N'GST Registered - Composition
', N'Y', CAST(0x0000A92100000000 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[mstStaticValues] OFF
