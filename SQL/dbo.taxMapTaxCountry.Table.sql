USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[taxMapTaxCountry]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[taxMapTaxCountry](
	[TaxDefinationID] [bigint] NULL,
	[CountryID] [bigint] NULL,
	[DatauniqueID] [int] NULL,
	[ApplicableType] [varchar](50) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[taxMapTaxCountry] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy]) VALUES (4, 182, 1, N'yes', CAST(0x0000AA480121E193 AS DateTime), N'10')
INSERT [dbo].[taxMapTaxCountry] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy]) VALUES (1, 2, 1, N'inter', CAST(0x0000AAFA0123727E AS DateTime), N'10')
INSERT [dbo].[taxMapTaxCountry] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy]) VALUES (3, 101, 1, N'inter', NULL, NULL)
INSERT [dbo].[taxMapTaxCountry] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy]) VALUES (0, 1, 1, N'inter', CAST(0x0000A9C800BE2961 AS DateTime), N'10')
INSERT [dbo].[taxMapTaxCountry] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy]) VALUES (0, 2, 1, N'inter', CAST(0x0000A9C800BE9C7C AS DateTime), N'10')
INSERT [dbo].[taxMapTaxCountry] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy]) VALUES (0, 3, 1, N'inter', CAST(0x0000A9C800BEA963 AS DateTime), N'10')
INSERT [dbo].[taxMapTaxCountry] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy]) VALUES (0, 4, 1, N'inter', CAST(0x0000A9C800BEF0FB AS DateTime), N'10')
INSERT [dbo].[taxMapTaxCountry] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy]) VALUES (1, 1, 1, N'inter', CAST(0x0000A9C800C06DE4 AS DateTime), N'10')
INSERT [dbo].[taxMapTaxCountry] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy]) VALUES (2, 101, 1, N'intra', CAST(0x0000A9CA001C8DB8 AS DateTime), N'10')
INSERT [dbo].[taxMapTaxCountry] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy]) VALUES (1, 101, 1, N'intra', CAST(0x0000A9CA001CA8C2 AS DateTime), N'10')
INSERT [dbo].[taxMapTaxCountry] ([TaxDefinationID], [CountryID], [DatauniqueID], [ApplicableType], [LastModifiedOn], [LastModifiedBy]) VALUES (2, 3, 1, N'weqe', CAST(0x0000A9CA001CD3FD AS DateTime), N'10')
