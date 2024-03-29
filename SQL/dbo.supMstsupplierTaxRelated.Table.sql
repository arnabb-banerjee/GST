USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[supMstsupplierTaxRelated]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[supMstsupplierTaxRelated](
	[SupID] [bigint] NOT NULL,
	[DatauniqueID] [int] NULL,
	[GSTRegistrationType] [int] NULL,
	[GSTIN] [varchar](50) NULL,
	[TaxRegNo] [varchar](50) NULL,
	[CSTRegNo] [varchar](50) NULL,
	[PANNo] [varchar](50) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[Terms] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
