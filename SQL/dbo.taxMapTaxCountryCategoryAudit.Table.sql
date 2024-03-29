USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[taxMapTaxCountryCategoryAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[taxMapTaxCountryCategoryAudit](
	[TaxDefinationID] [int] NULL,
	[CategoryId] [int] NULL,
	[Percentage] [varchar](6) NULL,
	[DatauniqueID] [int] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CountryID] [int] NULL,
	[ParentDataUniqueID] [int] NULL,
	[ApplicableType] [varchar](10) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[taxMapTaxCountryCategoryAudit] ([TaxDefinationID], [CategoryId], [Percentage], [DatauniqueID], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [CountryID], [ParentDataUniqueID], [ApplicableType]) VALUES (1, 1301, N'12.11', 1, CAST(0x0000A9C800B12BA0 AS DateTime), N'10', NULL, NULL, NULL, NULL, 101, NULL, N'intra')
INSERT [dbo].[taxMapTaxCountryCategoryAudit] ([TaxDefinationID], [CategoryId], [Percentage], [DatauniqueID], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [CountryID], [ParentDataUniqueID], [ApplicableType]) VALUES (1, 1301, N'1', 1, CAST(0x0000AA8C0105E17E AS DateTime), N'10', NULL, NULL, NULL, NULL, 1, NULL, N'inter')
INSERT [dbo].[taxMapTaxCountryCategoryAudit] ([TaxDefinationID], [CategoryId], [Percentage], [DatauniqueID], [LastModifiedOn], [LastModifiedBy], [AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy], [CountryID], [ParentDataUniqueID], [ApplicableType]) VALUES (1, 1303, N'3', 1, CAST(0x0000AA8C0106106A AS DateTime), N'10', NULL, NULL, NULL, NULL, 1, NULL, N'inter')
