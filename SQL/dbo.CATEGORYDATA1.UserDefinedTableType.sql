USE [GST_DEV_V2]
GO
/****** Object:  UserDefinedTableType [dbo].[CATEGORYDATA1]    Script Date: 08-12-2019 17:51:43 ******/
CREATE TYPE [dbo].[CATEGORYDATA1] AS TABLE(
	[ParentCategory] [varchar](500) NULL,
	[CategoryName] [varchar](500) NULL,
	[TaxName] [varchar](10) NULL,
	[CountrtyName] [varchar](500) NULL,
	[CGST] [varchar](20) NULL,
	[IGST] [varchar](20) NULL,
	[SGST] [varchar](20) NULL,
	[VAT] [varchar](20) NULL
)
GO
