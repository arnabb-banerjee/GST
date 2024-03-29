USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[mstTerms]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mstTerms](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[Name] [varchar](50) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[OrganizationID] [bigint] NULL,
	[DueInFixedNumberDays] [numeric](3, 0) NULL,
	[DueInCertainDayOfMonth] [numeric](3, 0) NULL,
	[DueInNextMonth] [numeric](3, 0) NULL,
	[Discount] [numeric](3, 3) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
