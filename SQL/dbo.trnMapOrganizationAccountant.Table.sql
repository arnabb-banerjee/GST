USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[trnMapOrganizationAccountant]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trnMapOrganizationAccountant](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DataUniqueID] [int] NULL,
	[OrganizationID] [bigint] NULL,
	[AccountantID] [varchar](50) NULL,
	[ActivityType] [varchar](1) NULL,
	[UserID] [bigint] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [bigint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[trnMapOrganizationAccountant] ON 

INSERT [dbo].[trnMapOrganizationAccountant] ([ID], [DataUniqueID], [OrganizationID], [AccountantID], [ActivityType], [UserID], [LastModifiedOn], [LastModifiedBy]) VALUES (1, NULL, NULL, NULL, NULL, 10, CAST(0x0000A9980184A25F AS DateTime), 10)
INSERT [dbo].[trnMapOrganizationAccountant] ([ID], [DataUniqueID], [OrganizationID], [AccountantID], [ActivityType], [UserID], [LastModifiedOn], [LastModifiedBy]) VALUES (2, NULL, NULL, NULL, NULL, 10, CAST(0x0000A9980184A569 AS DateTime), 10)
SET IDENTITY_INSERT [dbo].[trnMapOrganizationAccountant] OFF
