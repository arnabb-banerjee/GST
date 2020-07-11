USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[prMAPOrganizationproductQty]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[prMAPOrganizationproductQty](
	[OrganizationproductId] [bigint] NULL,
	[DatauniqueId] [int] NULL,
	[AvailableQty] [bigint] NULL,
	[LastModifiedOn] [datetime] NULL
) ON [PRIMARY]

GO
