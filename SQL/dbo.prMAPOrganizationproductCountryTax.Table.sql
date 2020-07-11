USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[prMAPOrganizationproductCountryTax]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[prMAPOrganizationproductCountryTax](
	[OrganizationproductId] [bigint] NULL,
	[DatauniqueId] [int] NULL,
	[TaxDefinationID] [bigint] NULL,
	[Tax] [numeric](18, 2) NULL,
	[LastModifiedOn] [datetime] NULL
) ON [PRIMARY]

GO
