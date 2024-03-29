USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[taxMstTaxMasterAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[taxMstTaxMasterAudit](
	[TaxDefinationID] [bigint] NULL,
	[TaxName] [varchar](50) NULL,
	[DatauniqueID] [int] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[taxMstTaxMasterAudit] ([TaxDefinationID], [TaxName], [DatauniqueID], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy]) VALUES (1, N'SGST', NULL, NULL, NULL, CAST(0x0000A9CA001401F4 AS DateTime), N'10')
