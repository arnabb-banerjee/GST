USE [GST_DEV_V2]
GO
/****** Object:  UserDefinedTableType [dbo].[CATEGORYDATA]    Script Date: 08-12-2019 17:51:43 ******/
CREATE TYPE [dbo].[CATEGORYDATA] AS TABLE(
	[CountrtyName] [varchar](500) NULL,
	[ParentCategory] [varchar](500) NULL,
	[CategoryName] [varchar](500) NULL,
	[ServiceGoods] [varchar](20) NULL,
	[HSNSAC] [varchar](20) NULL,
	[CGST] [varchar](20) NULL,
	[SGST] [varchar](20) NULL,
	[IGST] [varchar](20) NULL,
	[VAT] [varchar](20) NULL
)
GO
