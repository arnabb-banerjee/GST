USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[cusMstcustomerpreferedMethods]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cusMstcustomerpreferedMethods](
	[CusID] [bigint] NOT NULL,
	[DatauniqueID] [int] NULL,
	[PrefferedPaymentMethod] [int] NULL,
	[PrefferedDeliveryMethod] [int] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (10042, 32, NULL, NULL, CAST(0x0000AB1500CB8AE5 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (10043, 3, NULL, NULL, CAST(0x0000AB1500D125D9 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20042, 5, NULL, NULL, CAST(0x0000AB0A0124DEEB AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20043, 1, 0, 0, CAST(0x0000A971017E3779 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20044, 1, 1, 6, CAST(0x0000A9830100B4FA AS DateTime), N'-1', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20045, 1, 1, 6, CAST(0x0000A9830103C03F AS DateTime), N'-1', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20046, 1, 1, 6, CAST(0x0000A9830115C05F AS DateTime), N'-1', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20047, 1, 0, 0, CAST(0x0000A9850070EBBA AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30047, 1, 3, 7, CAST(0x0000A98D0137EF8E AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30048, 1, 0, 0, CAST(0x0000A98D01381A82 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30049, 2, 3, 7, CAST(0x0000A9B3011F2A8E AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30050, 1, 2, 7, CAST(0x0000A9B301440C86 AS DateTime), N'10034', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30051, 1, 0, 0, CAST(0x0000A9B301481141 AS DateTime), N'10034', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30052, 1, 2, 6, CAST(0x0000A9B3014B9C87 AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30053, 7, NULL, NULL, CAST(0x0000AB1B011BCDA4 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30054, 1, 0, 0, CAST(0x0000AA9600F60C2B AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30055, 1, 0, 0, CAST(0x0000AA9601056096 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30056, 1, 0, 0, CAST(0x0000AAD3013F52BD AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30057, 1, 0, 0, CAST(0x0000AB0700B6174B AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30058, 1, 0, 0, CAST(0x0000AB0700C201E1 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30059, 2, 1, 7, CAST(0x0000AB0900D67DE9 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (40059, 1, 0, 0, CAST(0x0000AB19002367D0 AS DateTime), N'30052', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (40060, 1, 0, 0, CAST(0x0000AB19016413E8 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (40061, 4, NULL, NULL, CAST(0x0000AB1B00E3C519 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethods] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20048, 1, 1, 6, CAST(0x0000A98500EBBD04 AS DateTime), N'-1', NULL, NULL)
