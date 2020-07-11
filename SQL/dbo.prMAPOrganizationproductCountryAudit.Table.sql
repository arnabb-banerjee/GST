USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[prMAPOrganizationproductCountryAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[prMAPOrganizationproductCountryAudit](
	[OrganizationproductId] [bigint] NULL,
	[DatauniqueId] [int] NULL,
	[CountryId] [bigint] NULL,
	[LastModifiedOn] [datetime] NULL
) ON [PRIMARY]

GO
