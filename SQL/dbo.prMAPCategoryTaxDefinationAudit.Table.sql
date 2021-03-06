USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[prMAPCategoryTaxDefinationAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prMAPCategoryTaxDefinationAudit](
	[TaxDefinationID] [bigint] NULL,
	[CategoryId] [bigint] NULL,
	[Percentage] [varchar](6) NULL,
	[DatauniqueID] [bigint] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CountryID] [bigint] NULL,
	[ParentDataUniqueID] [bigint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
