USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[supMstsupplierMaster]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[supMstsupplierMaster](
	[SupID] [bigint] IDENTITY(1,1) NOT NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[Title] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
	[MiddleName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Safix] [varchar](50) NULL,
	[DOB] [datetime] NULL,
	[Sex] [varchar](20) NULL,
	[IsSubSupplier] [varchar](1) NULL,
	[ParentSupID] [varchar](50) NULL,
	[BillingWith] [varchar](1) NULL,
	[OrganizationID] [bigint] NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CompanyName] [varchar](250) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
