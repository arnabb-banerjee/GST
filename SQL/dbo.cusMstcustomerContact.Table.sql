USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[cusMstcustomerContact]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cusMstcustomerContact](
	[CusID] [bigint] NOT NULL,
	[DatauniqueID] [int] NULL,
	[EmailID] [varchar](500) NULL,
	[Mobile] [varchar](150) NULL,
	[Street1] [varchar](500) NULL,
	[Street2] [varchar](500) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](5) NULL,
	[Country] [varchar](3) NULL,
	[Website] [varchar](200) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (10042, 8, N'btecharnab@gmail.com', N'8765434345', N'12 AC Road', N'Indraprastha', N'Berhampore', N'221', N'10', N'http://www.example.com', CAST(0x0000AAF7010D0CAA AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (10043, 3, N'btecharnab1@gmail.com', N'8764567866', N'Customer Address', NULL, NULL, NULL, N'20', N'http://welcome.com', CAST(0x0000AB1500D125D0 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20042, 2, N'abc@gmail.com', N'5776787666', NULL, NULL, NULL, NULL, NULL, N'http://www.example1.com', CAST(0x0000AAF701131717 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20043, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A971017E3779 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20044, 1, N'arna@pwc.in', N'9.86756e+011', N'AMAR CHAKRABORTY ROAD', NULL, N'BERHEAMPORE', N'41', N'101', NULL, CAST(0x0000A9830100B4F8 AS DateTime), N'-1', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20045, 1, N'arna@pwc.in', N'9.86756e+011', N'AMAR CHAKRABORTY ROAD', NULL, N'BERHEAMPORE', N'41', N'101', NULL, CAST(0x0000A9830103C03F AS DateTime), N'-1', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20046, 1, N'arna@pwc.in', N'9.86756e+011', N'AMAR CHAKRABORTY ROAD', NULL, N'BERHEAMPORE', N'41', N'101', NULL, CAST(0x0000A9830115C05F AS DateTime), N'-1', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20047, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A9850070EBB6 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30047, 1, NULL, NULL, NULL, NULL, NULL, N'324', N'16', NULL, CAST(0x0000A98D0137EF8E AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30048, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A98D01381A82 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30049, 3, N'customer1@gmail.com', N'09201928476294823746', N'Plain Street1', N'Plain Street2', N'Plain Street', N'43', N'1', N'http://app.bigbook.io', CAST(0x0000A9B3012FD595 AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30050, 1, N'bijay.ray@gmail.com', N'12846213687687687687', N'khqlwejw', N'k82746kqwejh', N'KOlkall', N'113', N'3', N'https://iamaboy.com', CAST(0x0000A9B301440C86 AS DateTime), N'10034', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30051, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A9B301481141 AS DateTime), N'10034', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30052, 1, N'arati.halar@gamil.com', N'13617367126348', N'qurorwe  qoweiru', N'iqweuryiw qiweur', N'Kolkata', N'75', N'2', N'http://app.bigbook.io', CAST(0x0000A9B3014B9C87 AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30053, 3, N'abc123@gmail.com', N'435435445435', N'cust1', N'cust2', N'city 1', NULL, NULL, NULL, CAST(0x0000AB1500E0BC94 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30054, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AA9600F60C2B AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30055, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AA9601056096 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30056, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AAD3013F52BD AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30057, 1, N'btecharnab@gmail.com', N'8765434345', NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AB0700B6174B AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30058, 1, N'btecharnab@gmail.com', N'8765434345', NULL, NULL, NULL, NULL, NULL, N'http://www.example.com', CAST(0x0000AB0700C201E1 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (30059, 2, N'indra@gmail.com', N'3453453445', N'11 AS Road', NULL, N'Kolkata', N'41', N'101', NULL, CAST(0x0000AB0900D7077B AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (40059, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AB19002367D0 AS DateTime), N'30052', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (40060, 1, N'ewrewr@gmail.com', N'5345345345', NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AB19016413E8 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (40061, 1, N'arp@gmail.comk', N'2421412444', NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AB1B00E3887A AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerContact] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (20048, 1, N'arna@pwc.in', N'9.86756e+011', N'AMAR CHAKRABORTY ROAD', NULL, N'BERHEAMPORE', N'41', N'101', NULL, CAST(0x0000A98500EBBD04 AS DateTime), N'-1', NULL, NULL)
