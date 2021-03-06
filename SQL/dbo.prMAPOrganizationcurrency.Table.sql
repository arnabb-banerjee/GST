USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[prMAPOrganizationcurrency]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prMAPOrganizationcurrency](
	[OrganizationCurrencyId] [bigint] IDENTITY(1,1) NOT NULL,
	[CurrencyId] [varchar](50) NULL,
	[OrganizationID] [bigint] NULL,
	[DatauniqueID] [int] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[prMAPOrganizationcurrency] ON 

INSERT [dbo].[prMAPOrganizationcurrency] ([OrganizationCurrencyId], [CurrencyId], [OrganizationID], [DatauniqueID], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (1, N'0', 3, NULL, CAST(0x0000AAFC0029C21C AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[prMAPOrganizationcurrency] ([OrganizationCurrencyId], [CurrencyId], [OrganizationID], [DatauniqueID], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (2, N'AFN', NULL, 0, CAST(0x0000AAFC00311067 AS DateTime), N'0', NULL, NULL)
INSERT [dbo].[prMAPOrganizationcurrency] ([OrganizationCurrencyId], [CurrencyId], [OrganizationID], [DatauniqueID], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (3, N'THB', 10, NULL, CAST(0x0000AAFC00311A1D AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[prMAPOrganizationcurrency] ([OrganizationCurrencyId], [CurrencyId], [OrganizationID], [DatauniqueID], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (4, N'AFN', 10, NULL, CAST(0x0000AAFC00312646 AS DateTime), N'10', NULL, NULL)
INSERT [dbo].[prMAPOrganizationcurrency] ([OrganizationCurrencyId], [CurrencyId], [OrganizationID], [DatauniqueID], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy]) VALUES (5, N'THB', 8, NULL, CAST(0x0000AAFC0031E2F1 AS DateTime), N'10', NULL, NULL)
SET IDENTITY_INSERT [dbo].[prMAPOrganizationcurrency] OFF
