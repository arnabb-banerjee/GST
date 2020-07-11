USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[cusMstcustomerpreferedMethodsAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cusMstcustomerpreferedMethodsAudit](
	[CusID] [bigint] NOT NULL,
	[DatauniqueID] [int] NULL,
	[PrefferedPaymentMethod] [int] NULL,
	[PrefferedDeliveryMethod] [int] NULL,
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
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 1, 0, 0, CAST(0x0000A96300A12A75 AS DateTime), N'0', CAST(0x0000A96300B9DE97 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 2, NULL, NULL, CAST(0x0000A96300B9DE98 AS DateTime), N'0', CAST(0x0000A96300B9F70F AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 3, NULL, NULL, CAST(0x0000A96300B9F70F AS DateTime), N'0', CAST(0x0000A9850089816D AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 4, NULL, NULL, CAST(0x0000A98500898173 AS DateTime), N'0', CAST(0x0000A98701467A63 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 5, NULL, NULL, CAST(0x0000A98701467A64 AS DateTime), N'0', CAST(0x0000A9870146AAF0 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 6, NULL, NULL, CAST(0x0000A9870146AAF1 AS DateTime), N'0', CAST(0x0000A98D00EA41DD AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 7, NULL, NULL, CAST(0x0000A98D00EA41E4 AS DateTime), N'0', CAST(0x0000A98D00EB6C7A AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 8, NULL, NULL, CAST(0x0000A98D00EB6C7A AS DateTime), N'0', CAST(0x0000A98D00FAEACC AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 9, NULL, NULL, CAST(0x0000A98D00FAEACC AS DateTime), N'0', CAST(0x0000A98D0136DA06 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 10, NULL, NULL, CAST(0x0000A98D0136DA06 AS DateTime), N'0', CAST(0x0000A9B000A8ABE6 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 11, NULL, NULL, CAST(0x0000A9B000A8ABF3 AS DateTime), N'10', CAST(0x0000A9B000BCCA02 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30049, 1, 0, 0, CAST(0x0000A9B301173EB8 AS DateTime), N'10033', CAST(0x0000A9B3011F2A8E AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 12, NULL, NULL, CAST(0x0000A9B000BCCA02 AS DateTime), N'10', CAST(0x0000A9B30157A694 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 13, NULL, NULL, CAST(0x0000A9B30157A699 AS DateTime), N'10', CAST(0x0000A9C30003EA73 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 14, NULL, NULL, CAST(0x0000A9C30003EA74 AS DateTime), N'10', CAST(0x0000A9C500BF77DD AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 15, NULL, NULL, CAST(0x0000A9C500BF77E0 AS DateTime), N'10', CAST(0x0000A9C500BF85D5 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 16, NULL, NULL, CAST(0x0000A9C500BF85D5 AS DateTime), N'10', CAST(0x0000AAF7010B137D AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 17, NULL, NULL, CAST(0x0000AAF7010B137E AS DateTime), N'10', CAST(0x0000AAF7010B364E AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 18, NULL, NULL, CAST(0x0000AAF7010B364E AS DateTime), N'10', CAST(0x0000AAF7010C21B4 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 19, NULL, NULL, CAST(0x0000AAF7010C21B4 AS DateTime), N'10', CAST(0x0000AAF7010C37EE AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 20, NULL, NULL, CAST(0x0000AAF7010C37EE AS DateTime), N'10', CAST(0x0000AAF7010C7D11 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 21, NULL, NULL, CAST(0x0000AAF7010C7D11 AS DateTime), N'10', CAST(0x0000AAF7010CDA15 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 22, NULL, NULL, CAST(0x0000AAF7010CDA15 AS DateTime), N'10', CAST(0x0000AAF7010D0CAA AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (20042, 1, 0, 0, CAST(0x0000A96301698265 AS DateTime), N'0', CAST(0x0000AAF70111BFB9 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (20042, 2, NULL, NULL, CAST(0x0000AAF70111BFB9 AS DateTime), N'10', CAST(0x0000AAF701120E30 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 23, NULL, NULL, CAST(0x0000AAF7010D0CAA AS DateTime), N'10', CAST(0x0000AB0700C65537 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 24, NULL, NULL, CAST(0x0000AB0700C6553C AS DateTime), N'10', CAST(0x0000AB0700D2DE1E AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 26, NULL, NULL, CAST(0x0000AB0700DB29A3 AS DateTime), N'10', CAST(0x0000AB0900D56927 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 27, NULL, NULL, CAST(0x0000AB0900D56927 AS DateTime), N'10', CAST(0x0000AB0900D5D6EC AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30059, 1, 0, 0, CAST(0x0000AB0900D645B1 AS DateTime), N'10', CAST(0x0000AB0900D67DE9 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 28, NULL, NULL, CAST(0x0000AB0900D5D6EC AS DateTime), N'10', CAST(0x0000AB090134E23F AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 29, NULL, NULL, CAST(0x0000AB090134E243 AS DateTime), N'10', CAST(0x0000AB090134EA09 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 31, NULL, NULL, CAST(0x0000AB0A0124C527 AS DateTime), N'10', CAST(0x0000AB1500CB8AE5 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10043, 2, NULL, NULL, CAST(0x0000A9850091CC38 AS DateTime), N'0', CAST(0x0000AB1500D125D8 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30053, 1, 0, 0, CAST(0x0000AA9600F5C976 AS DateTime), N'10036', CAST(0x0000AB1500E06422 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30053, 2, NULL, NULL, CAST(0x0000AB1500E06422 AS DateTime), N'10036', CAST(0x0000AB1500E0BC94 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30053, 3, NULL, NULL, CAST(0x0000AB1500E0BC94 AS DateTime), N'10036', CAST(0x0000AB1900070F2F AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30053, 4, NULL, NULL, CAST(0x0000AB1900070F32 AS DateTime), N'10036', CAST(0x0000AB1B011B56DA AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30053, 5, NULL, NULL, CAST(0x0000AB1B011B56DB AS DateTime), N'10036', CAST(0x0000AB1B011B5776 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30053, 6, NULL, NULL, CAST(0x0000AB1B011B5776 AS DateTime), N'10036', CAST(0x0000AB1B011BCDA4 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10043, 1, 0, 0, CAST(0x0000A96300BA9E4B AS DateTime), N'0', CAST(0x0000A9850091CC38 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (20042, 3, NULL, NULL, CAST(0x0000AAF701120E30 AS DateTime), N'10', CAST(0x0000AAF701131717 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 25, NULL, NULL, CAST(0x0000AB0700D2DE1E AS DateTime), N'10', CAST(0x0000AB0700DB2984 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 30, NULL, NULL, CAST(0x0000AB090134EA09 AS DateTime), N'10', CAST(0x0000AB0A0124C525 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (20042, 4, NULL, NULL, CAST(0x0000AAF701131717 AS DateTime), N'10', CAST(0x0000AB0A0124DEEB AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (40061, 1, 0, 0, CAST(0x0000AB1B00E3887A AS DateTime), N'10036', CAST(0x0000AB1B00E39BF5 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (40061, 2, NULL, NULL, CAST(0x0000AB1B00E39BF9 AS DateTime), N'10036', CAST(0x0000AB1B00E39C92 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerpreferedMethodsAudit] ([CusID], [DatauniqueID], [PrefferedPaymentMethod], [PrefferedDeliveryMethod], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (40061, 3, NULL, NULL, CAST(0x0000AB1B00E39C92 AS DateTime), N'10036', CAST(0x0000AB1B00E3C519 AS DateTime), N'10036', NULL, NULL)
