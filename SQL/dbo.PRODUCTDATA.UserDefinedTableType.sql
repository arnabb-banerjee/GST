USE [GST_DEV_V2]
GO
/****** Object:  UserDefinedTableType [dbo].[PRODUCTDATA]    Script Date: 08-12-2019 17:51:43 ******/
CREATE TYPE [dbo].[PRODUCTDATA] AS TABLE(
	[CategoryName] [varchar](500) NULL,
	[CountrtyName] [varchar](500) NULL,
	[ProductName] [varchar](500) NULL
)
GO
