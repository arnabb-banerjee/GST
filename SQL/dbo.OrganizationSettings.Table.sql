USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[OrganizationSettings]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrganizationSettings](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [bigint] NULL,
	[DataUniqueId] [bigint] NULL,
	[mc_isAllowedMutyCurrency] [varchar](1) NULL,
	[mc_CurrencyList] [varchar](max) NULL,
	[p_isAllowedOnlinePayment] [varchar](1) NULL,
	[p_BankAccountNumber] [varchar](250) NULL,
	[p_BankAccountHolder] [varchar](159) NULL,
	[p_BankAccountIFSCCode] [varchar](20) NULL,
	[p_BankAccountIMCRCode] [varchar](20) NULL,
	[p_BankAccountIBranchName] [varchar](50) NULL,
	[p_BankAccountIBankName] [varchar](50) NULL,
	[p_PaypalAccountID] [varchar](500) NULL,
	[c_isAllowedMultyLanguage] [varchar](1) NULL,
	[an_isAllowedAlert_GSTDate] [varchar](1) NULL,
	[an_isAllowedAlert_PaidMembership] [varchar](1) NULL,
	[an_AlertText_GSTDate] [varchar](max) NULL,
	[an_AlertText_PaidMembership] [varchar](max) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CompanyName] [varchar](250) NULL,
	[Email] [varchar](250) NULL,
	[Mobile] [varchar](50) NULL,
	[Address] [varchar](250) NULL,
	[City] [varchar](250) NULL,
	[State] [bigint] NULL,
	[Country] [bigint] NULL,
	[Website] [varchar](250) NULL,
	[CIN] [varchar](50) NULL,
	[PAN] [varchar](50) NULL,
	[DefaultEmail] [varchar](250) NULL,
	[SMTP] [varchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[OrganizationSettings] ON 

INSERT [dbo].[OrganizationSettings] ([id], [OrganizationId], [DataUniqueId], [mc_isAllowedMutyCurrency], [mc_CurrencyList], [p_isAllowedOnlinePayment], [p_BankAccountNumber], [p_BankAccountHolder], [p_BankAccountIFSCCode], [p_BankAccountIMCRCode], [p_BankAccountIBranchName], [p_BankAccountIBankName], [p_PaypalAccountID], [c_isAllowedMultyLanguage], [an_isAllowedAlert_GSTDate], [an_isAllowedAlert_PaidMembership], [an_AlertText_GSTDate], [an_AlertText_PaidMembership], [LastModifiedOn], [LastModifiedBy], [CompanyName], [Email], [Mobile], [Address], [City], [State], [Country], [Website], [CIN], [PAN], [DefaultEmail], [SMTP]) VALUES (1, 10020, 16, N'F', N'', N'T', N'', N'', N'', N'', N'asdada', N'asdas', N'', N'N', N'F', N'F', N'adsad', N'asda', CAST(0x0000AA9B01328722 AS DateTime), N'10', N'comp1', N'xyz3@gmail.com', N'', N'', N'', 91, 2, N'', N'', N'', N'abc3@gmail.com', N'')
INSERT [dbo].[OrganizationSettings] ([id], [OrganizationId], [DataUniqueId], [mc_isAllowedMutyCurrency], [mc_CurrencyList], [p_isAllowedOnlinePayment], [p_BankAccountNumber], [p_BankAccountHolder], [p_BankAccountIFSCCode], [p_BankAccountIMCRCode], [p_BankAccountIBranchName], [p_BankAccountIBankName], [p_PaypalAccountID], [c_isAllowedMultyLanguage], [an_isAllowedAlert_GSTDate], [an_isAllowedAlert_PaidMembership], [an_AlertText_GSTDate], [an_AlertText_PaidMembership], [LastModifiedOn], [LastModifiedBy], [CompanyName], [Email], [Mobile], [Address], [City], [State], [Country], [Website], [CIN], [PAN], [DefaultEmail], [SMTP]) VALUES (2, 3, 4, N'N', N'', N'N', N'', N'', N'', N'', N'', N'', N'', N'N', N'N', N'F', N'', N'', CAST(0x0000A9BB013B29FD AS DateTime), N'10', N'', N'', N'', N'', N'', 77, 2, N'', N'', N'', N'', N'')
INSERT [dbo].[OrganizationSettings] ([id], [OrganizationId], [DataUniqueId], [mc_isAllowedMutyCurrency], [mc_CurrencyList], [p_isAllowedOnlinePayment], [p_BankAccountNumber], [p_BankAccountHolder], [p_BankAccountIFSCCode], [p_BankAccountIMCRCode], [p_BankAccountIBranchName], [p_BankAccountIBankName], [p_PaypalAccountID], [c_isAllowedMultyLanguage], [an_isAllowedAlert_GSTDate], [an_isAllowedAlert_PaidMembership], [an_AlertText_GSTDate], [an_AlertText_PaidMembership], [LastModifiedOn], [LastModifiedBy], [CompanyName], [Email], [Mobile], [Address], [City], [State], [Country], [Website], [CIN], [PAN], [DefaultEmail], [SMTP]) VALUES (3, 10, 1, N'N', N'', N'N', N'', N'', N'', N'', N'', N'', N'', N'N', N'N', N'F', N'', N'', CAST(0x0000AA9B012508AB AS DateTime), N'10', N'Abc', N'xyz1@gmail.com', N'', N'', N'', 263, 13, N'', N'', N'', N'abc1@gmail.com', N'')
INSERT [dbo].[OrganizationSettings] ([id], [OrganizationId], [DataUniqueId], [mc_isAllowedMutyCurrency], [mc_CurrencyList], [p_isAllowedOnlinePayment], [p_BankAccountNumber], [p_BankAccountHolder], [p_BankAccountIFSCCode], [p_BankAccountIMCRCode], [p_BankAccountIBranchName], [p_BankAccountIBankName], [p_PaypalAccountID], [c_isAllowedMultyLanguage], [an_isAllowedAlert_GSTDate], [an_isAllowedAlert_PaidMembership], [an_AlertText_GSTDate], [an_AlertText_PaidMembership], [LastModifiedOn], [LastModifiedBy], [CompanyName], [Email], [Mobile], [Address], [City], [State], [Country], [Website], [CIN], [PAN], [DefaultEmail], [SMTP]) VALUES (4, 11, 1, N'N', N'', N'N', N'', N'', N'', N'', N'', N'', N'', N'N', N'N', N'F', N'', N'', CAST(0x0000AA9B01343F57 AS DateTime), N'10', N'aaa', N'aaa@gmail.com', N'', N'', N'', 250, 13, N'', N'', N'', N'bbb@gmail.com', N'')
INSERT [dbo].[OrganizationSettings] ([id], [OrganizationId], [DataUniqueId], [mc_isAllowedMutyCurrency], [mc_CurrencyList], [p_isAllowedOnlinePayment], [p_BankAccountNumber], [p_BankAccountHolder], [p_BankAccountIFSCCode], [p_BankAccountIMCRCode], [p_BankAccountIBranchName], [p_BankAccountIBankName], [p_PaypalAccountID], [c_isAllowedMultyLanguage], [an_isAllowedAlert_GSTDate], [an_isAllowedAlert_PaidMembership], [an_AlertText_GSTDate], [an_AlertText_PaidMembership], [LastModifiedOn], [LastModifiedBy], [CompanyName], [Email], [Mobile], [Address], [City], [State], [Country], [Website], [CIN], [PAN], [DefaultEmail], [SMTP]) VALUES (5, 5, 1, N'N', N'', N'N', N'', N'', N'', N'', N'', N'', N'', N'N', N'N', N'F', N'', N'', CAST(0x0000AA9B0134A157 AS DateTime), N'10', N'zzz', N'', N'', N'', N'', 517, 30, N'', N'', N'', N'', N'')
SET IDENTITY_INSERT [dbo].[OrganizationSettings] OFF
