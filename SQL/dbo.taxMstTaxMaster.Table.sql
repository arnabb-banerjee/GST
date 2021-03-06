USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[taxMstTaxMaster]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[taxMstTaxMaster](
	[TaxDefinationID] [bigint] NULL,
	[TaxName] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DatauniqueID] [bigint] NULL,
	[LastModifiedOn] [datetime] NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[taxMstTaxMaster] ADD [LastModifiedBy] [varchar](50) NULL

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[taxMstTaxMaster] ([TaxDefinationID], [TaxName], [CreatedOn], [CreatedBy], [DatauniqueID], [LastModifiedOn], [LastModifiedBy]) VALUES (1, N'SGST 1', NULL, NULL, 1, CAST(0x0000A9CA00140227 AS DateTime), N'10')
INSERT [dbo].[taxMstTaxMaster] ([TaxDefinationID], [TaxName], [CreatedOn], [CreatedBy], [DatauniqueID], [LastModifiedOn], [LastModifiedBy]) VALUES (2, N'CGST', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[taxMstTaxMaster] ([TaxDefinationID], [TaxName], [CreatedOn], [CreatedBy], [DatauniqueID], [LastModifiedOn], [LastModifiedBy]) VALUES (3, N'IGST', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[taxMstTaxMaster] ([TaxDefinationID], [TaxName], [CreatedOn], [CreatedBy], [DatauniqueID], [LastModifiedOn], [LastModifiedBy]) VALUES (4, N'Vat', NULL, NULL, 1, CAST(0x0000AA4801219E6F AS DateTime), N'10')
