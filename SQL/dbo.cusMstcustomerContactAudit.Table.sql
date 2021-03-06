USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[cusMstcustomerContactAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cusMstcustomerContactAudit](
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
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](10) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 1, N'btecharnab@gmail.com', N'8765434345', N'12 AC Road', N'Indraprastha', N'Berhampore', N'41', N'101', N'http://www.example.com', CAST(0x0000A96300A12A75 AS DateTime), N'0', CAST(0x0000A96300B9F6FC AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 2, N'btecharnab@gmail.com', N'8765434345', N'12 AC Road', N'Indraprastha', N'Berhampore', NULL, NULL, N'http://www.example.com', CAST(0x0000A96300B9F70F AS DateTime), N'0', CAST(0x0000A98500898166 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 3, N'btecharnab@gmail.com', N'8765434345', N'12 AC Road', N'Indraprastha', N'Berhampore', N'416', N'20', N'http://www.example.com', CAST(0x0000A9850089816A AS DateTime), N'0', CAST(0x0000A98701467A5B AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 4, N'btecharnab@gmail.com', N'8765434345', N'12 AC Road', N'Indraprastha', N'Berhampore', NULL, NULL, N'http://www.example.com', CAST(0x0000A98701467A5C AS DateTime), N'0', CAST(0x0000A98D00FAEAC5 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 5, N'btecharnab@gmail.com', N'8765434345', N'12 AC Road', N'Indraprastha', N'Berhampore', NULL, N'10', N'http://www.example.com', CAST(0x0000A98D00FAEAC9 AS DateTime), N'0', CAST(0x0000A98D0136DA04 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30049, 1, N'customer1@gmail.com', N'0920492487', NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A9B301173EB5 AS DateTime), N'10033', CAST(0x0000A9B3011F2A82 AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30049, 2, N'customer1@gmail.com', N'0920492487', N'Plain Street1', N'Plain Street2', N'Plain Street', N'43', N'1', N'http://app.bigbook.io', CAST(0x0000A9B3011F2A82 AS DateTime), N'10033', CAST(0x0000A9B3012FD595 AS DateTime), N'10033', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 6, N'btecharnab@gmail.com', N'8765434345', N'12 AC Road', N'Indraprastha', N'Berhampore', N'221', N'10', N'http://www.example.com', CAST(0x0000A98D0136DA06 AS DateTime), N'0', CAST(0x0000AAF7010CDA15 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10042, 7, N'btecharnab1@gmail.com', N'8765434345', N'12 AC Road', N'Indraprastha', N'Berhampore', N'221', N'10', N'http://www.example.com', CAST(0x0000AAF7010CDA15 AS DateTime), N'10', CAST(0x0000AAF7010D0CA9 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (20042, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A96301698261 AS DateTime), N'0', CAST(0x0000AAF701131717 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30059, 1, N'indra@gmail.com', N'3453453445', NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AB0900D645B1 AS DateTime), N'10', CAST(0x0000AB0900D70757 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10043, 2, N'btecharnab1@gmail.com', N'8764567866', NULL, NULL, NULL, NULL, N'20', N'http://welcome.com', CAST(0x0000A9850091CC38 AS DateTime), N'0', CAST(0x0000AB1500D125D0 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30053, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000AA9600F5C976 AS DateTime), N'10036', CAST(0x0000AB1500E06422 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (30053, 2, NULL, NULL, N'cust1', N'cust2', N'city 1', NULL, NULL, NULL, CAST(0x0000AB1500E06422 AS DateTime), N'10036', CAST(0x0000AB1500E0BC94 AS DateTime), N'10036', NULL, NULL)
INSERT [dbo].[cusMstcustomerContactAudit] ([CusID], [DatauniqueID], [EmailID], [Mobile], [Street1], [Street2], [City], [State], [Country], [Website], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy]) VALUES (10043, 1, N'btecharnab1@gmail.com', N'8764567866', NULL, NULL, NULL, NULL, NULL, N'http://welcome.com', CAST(0x0000A96300BA9E4A AS DateTime), N'0', CAST(0x0000A9850091CC37 AS DateTime), N'0', NULL, NULL)
