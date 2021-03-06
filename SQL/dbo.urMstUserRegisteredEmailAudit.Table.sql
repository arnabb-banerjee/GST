USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[urMstUserRegisteredEmailAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[urMstUserRegisteredEmailAudit](
	[EmailID] [varchar](500) NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[UserID] [bigint] NULL,
	[IsEmailConfirmationSend] [char](1) NULL,
	[EmailConfirmationSentOn] [datetime] NULL,
	[IsEmailVerified] [char](1) NULL,
	[EmailVerifiedOn] [datetime] NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[VerificationKey] [varchar](550) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
