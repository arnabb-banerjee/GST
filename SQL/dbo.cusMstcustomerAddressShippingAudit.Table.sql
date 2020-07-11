USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[cusMstcustomerAddressShippingAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cusMstcustomerAddressShippingAudit](
	[AddressID] [bigint] NULL,
	[DataUniqueID] [int] NULL,
	[CusID] [bigint] NULL,
	[Street1] [varchar](500) NULL,
	[Street2] [varchar](500) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](5) NULL,
	[Country] [varchar](3) NULL,
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
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 2, 10042, N'16/1 Ghola Road', N'16/1 Ghola Road', N'Kolkata', N'41', N'101', CAST(0x0000A9B000BCCA02 AS DateTime), N'10', CAST(0x0000A96300B9DEA4 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 2, 10042, N'16/1 Ghola Road', N'16/1 Ghola Road', N'Kolkata', N'41', N'101', CAST(0x0000A9B000BCCA02 AS DateTime), N'10', CAST(0x0000A96300B9F70F AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 2, 10042, N'16/1 Ghola Road', N'16/1 Ghola Road', N'Kolkata', N'41', N'101', CAST(0x0000A9B000BCCA02 AS DateTime), N'10', CAST(0x0000A98500898173 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 2, 10042, N'16/1 Ghola Road', N'16/1 Ghola Road', N'Kolkata', N'41', N'101', CAST(0x0000A9B000BCCA02 AS DateTime), N'10', CAST(0x0000A98701467A64 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 2, 10042, N'16/1 Ghola Road', N'16/1 Ghola Road', N'Kolkata', N'41', N'101', CAST(0x0000A9B000BCCA02 AS DateTime), N'10', CAST(0x0000A9870146AAF1 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 2, 10042, N'16/1 Ghola Road', N'16/1 Ghola Road', N'Kolkata', N'41', N'101', CAST(0x0000A9B000BCCA02 AS DateTime), N'10', CAST(0x0000A98D00EA41E5 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 2, 10042, N'16/1 Ghola Road', N'16/1 Ghola Road', N'Kolkata', N'41', N'101', CAST(0x0000A9B000BCCA02 AS DateTime), N'10', CAST(0x0000A98D00EB6C7A AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 2, 10042, N'16/1 Ghola Road', N'16/1 Ghola Road', N'Kolkata', N'41', N'101', CAST(0x0000A9B000BCCA02 AS DateTime), N'10', CAST(0x0000A98D00FAEACC AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 2, 10042, N'16/1 Ghola Road', N'16/1 Ghola Road', N'Kolkata', N'41', N'101', CAST(0x0000A9B000BCCA02 AS DateTime), N'10', CAST(0x0000A98D0136DA07 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 2, 10042, N'16/1 Ghola Road', N'16/1 Ghola Road', N'Kolkata', N'41', N'101', CAST(0x0000A9B000BCCA02 AS DateTime), N'10', CAST(0x0000A9B000A8ABF9 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 2, 10042, N'16/1 Ghola Road', N'16/1 Ghola Road', N'Kolkata', N'41', N'101', CAST(0x0000A9B000BCCA02 AS DateTime), N'10', CAST(0x0000A9B000BCCA02 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 1, 30049, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A9B301173EB8 AS DateTime), N'10033', CAST(0x0000A9B3012B005D AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 2, 30049, N'asdfasfafd asfsdf sfsadfsa', N'asdfasfafd asfsdf sfsadfsa', N'asdf sdfsd fsdafsdaf sda f', NULL, NULL, CAST(0x0000A9B3012B0063 AS DateTime), N'10033', CAST(0x0000A9B3012B2AE5 AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 1, 30052, N'jahska dkjdhak', N'kajdhak aksjdhaskd', N'Bihar', N'313', N'16', CAST(0x0000A9B3014B9C87 AS DateTime), N'10033', CAST(0x0000A9B3014E816C AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 1, 10042, N'5/4 Jatin Das Nagar', N'Belghoria', N'Kolkata', N'41', N'101', CAST(0x0000A96300A12A75 AS DateTime), N'0', CAST(0x0000A9B30157A699 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 1, 30050, N'Shippinagdsasj', N'ajddhgsakf', N'asdfjlsdfkj', N'298', N'15', CAST(0x0000A9B301440C87 AS DateTime), N'10034', CAST(0x0000AB0700AD0155 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 1, 30059, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AB0900D645B1 AS DateTime), N'10', CAST(0x0000AB0900D78835 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 2, 30059, N'erwr', N'erwr', N'New Delhi', N'41', N'101', CAST(0x0000AB0900D78838 AS DateTime), N'10', CAST(0x0000AB090130F95B AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 1, 10043, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A96300BA9E51 AS DateTime), N'0', CAST(0x0000AB1500D125D9 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerAddressShippingAudit] ([AddressID], [DataUniqueID], [CusID], [Street1], [Street2], [City], [State], [Country], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (NULL, 1, 30053, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AA9600F5C976 AS DateTime), N'10036', CAST(0x0000AB1500E06422 AS DateTime), N'10036', NULL, NULL)
