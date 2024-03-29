USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[prMstAllowedTaxDefinationCountryWise]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prMstAllowedTaxDefinationCountryWise](
	[TaxDefinationID] [bigint] NULL,
	[CountryID] [bigint] NULL,
	[LocationRangeType] [varchar](50) NULL,
	[TaxName] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[prMstAllowedTaxDefinationCountryWise] ([TaxDefinationID], [CountryID], [LocationRangeType], [TaxName], [CreatedOn], [CreatedBy]) VALUES (1, 101, N'intra', N'SGST', NULL, NULL)
INSERT [dbo].[prMstAllowedTaxDefinationCountryWise] ([TaxDefinationID], [CountryID], [LocationRangeType], [TaxName], [CreatedOn], [CreatedBy]) VALUES (2, 101, N'intra', N'CGST', NULL, NULL)
INSERT [dbo].[prMstAllowedTaxDefinationCountryWise] ([TaxDefinationID], [CountryID], [LocationRangeType], [TaxName], [CreatedOn], [CreatedBy]) VALUES (3, 101, N'inter', N'IGST', NULL, NULL)
