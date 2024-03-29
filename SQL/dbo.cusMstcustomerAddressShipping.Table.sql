USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[cusMstcustomerAddressShipping]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cusMstcustomerAddressShipping](
	[AddressID] [bigint] IDENTITY(1,1) NOT NULL,
	[DataUniqueID] [int] NULL,
	[CusID] [bigint] NULL,
	[Street1] [varchar](500) NULL,
	[Street2] [varchar](500) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](5) NULL,
	[Country] [varchar](3) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[cusMstcustomerAddressShipping] ON 

INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (10039, 2, 10042, N'16/1 Ghola Road', N'16/1 Ghola Road', N'Kolkata', N'41', N'101', CAST(0x0000A9B30157A699 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (10040, 2, 10043, N'Shipping Address', NULL, NULL, NULL, NULL, CAST(0x0000AB1500D125D9 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20039, 1, 20042, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A96301698265 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20040, 1, 20043, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A971017E3779 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20041, 1, 20044, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A9830100B4FC AS DateTime), N'-1', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20042, 1, 20045, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A9830103C03F AS DateTime), N'-1', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20043, 1, 20046, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A9830115C05F AS DateTime), N'-1', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20044, 1, 20047, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A9850070EBC9 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30044, 1, 30047, NULL, NULL, NULL, N'56', N'1', CAST(0x0000A98D0137EF94 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30045, 1, 30048, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A98D01381A82 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30046, 3, 30049, N'asdfasfafd asfsdf sfsadfsa', N'asdfasfafd asfsdf sfsadfsa', N'asdf sdfsd fsdafsdaf sda f', N'257', N'13', CAST(0x0000A9B3012B2AE5 AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30047, 2, 30050, N'yyyy', N'yyyy', N'aksjsdhak', N'21', N'101', CAST(0x0000AB0700AD0156 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30048, 1, 30051, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A9B301481141 AS DateTime), N'10034', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30049, 2, 30052, N'tyttyt gghgg', N'tyttyt gghgg', N'Mumbai', N'335', N'17', CAST(0x0000A9B3014E816F AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30050, 2, 30053, N'ship1', N'ship2', N'ship city', NULL, NULL, CAST(0x0000AB1500E06423 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30051, 1, 30054, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AA9600F60C2B AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30052, 1, 30055, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AA9601056096 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30053, 1, 30056, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AAD3013F52BE AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30054, 1, 30057, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AB0700B6174B AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30055, 1, 30058, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AB0700C201E1 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30056, 3, 30059, N'erwr1', N'erwr1', N'New Delhi', N'41', N'101', CAST(0x0000AB090130F95F AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (40056, 1, 40059, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AB19002367D0 AS DateTime), N'30052', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (40057, 1, 40060, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AB19016413E8 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (40058, 1, 40061, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AB1B00E3887A AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShipping] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20045, 1, 20048, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A98500EBBD0E AS DateTime), N'-1', NULL, NULL)
SET IDENTITY_INSERT [dbo].[cusMstcustomerAddressShipping] OFF
