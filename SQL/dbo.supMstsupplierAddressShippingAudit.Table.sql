USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[supMstsupplierAddressShippingAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[supMstsupplierAddressShippingAudit](
	[AddressID] [bigint] NULL,
	[DataUniqueID] [int] NULL,
	[SupID] [bigint] NULL,
	[Street1] [varchar](500) NULL,
	[Street2] [varchar](500) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](5) NULL,
	[Country] [varchar](3) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
