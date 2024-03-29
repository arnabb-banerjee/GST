USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[taxMapTaxCountryAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[taxMapTaxCountryAudit](
	[TaxDefinationID] [bigint] NULL,
	[CountryID] [bigint] NULL,
	[DatauniqueID] [int] NULL,
	[ApplicableType] [varchar](50) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](10) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[taxMapTaxCountryAudit] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy]) VALUES (0, 1, 1, N'inter', CAST(0x0000A9C800BCFA1E AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[taxMapTaxCountryAudit] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy]) VALUES (2, 101, 1, N'intra', NULL, NULL, NULL, NULL)
INSERT [dbo].[taxMapTaxCountryAudit] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy]) VALUES (1, 101, 1, N'intra', NULL, NULL, NULL, NULL)
