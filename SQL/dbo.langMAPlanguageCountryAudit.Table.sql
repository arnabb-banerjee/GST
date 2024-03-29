USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[langMAPlanguageCountryAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[langMAPlanguageCountryAudit](
	[LanguageId] [int] NULL,
	[DatauniqueID] [int] NULL,
	[Country] [int] NULL,
	[Visibility] [varchar](1) NULL,
	[Priority] [int] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [bigint] NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
