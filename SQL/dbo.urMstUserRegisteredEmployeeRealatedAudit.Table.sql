USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[urMstUserRegisteredEmployeeRealatedAudit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[urMstUserRegisteredEmployeeRealatedAudit](
	[UserID] [bigint] NULL,
	[DatauniqueID] [int] NULL,
	[EmployeeID] [varchar](50) NULL,
	[ManagerID] [varchar](50) NULL,
	[BillingRatePerHour] [varchar](50) NULL,
	[isBillableBydefault] [varchar](1) NULL,
	[HireDate] [date] NULL,
	[ReleaseDate] [date] NULL,
	[Notes] [varchar](max) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[AuditOperationOn] [datetime] NULL,
	[AuditOperationBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[urMstUserRegisteredEmployeeRealatedAudit] ADD [CreatedBy] [varchar](50) NULL

GO
SET ANSI_PADDING OFF
GO
