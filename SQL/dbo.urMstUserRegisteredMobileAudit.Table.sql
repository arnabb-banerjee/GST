USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[urMstUserRegisteredMobileAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[urMstUserRegisteredMobileAudit](
	[Mobile] [varchar](500) NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[UserID] [bigint] NULL,
	[IsMobileConfirmationSent] [char](1) NULL,
	[MobileConfirmationSentOn] [datetime] NULL,
	[IsMobileVerified] [char](1) NULL,
	[MobileVerifiedOn] [datetime] NULL,
	[IsActive] [varchar](1) NULL,
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
