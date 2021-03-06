USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[prMstproductMaster]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prMstproductMaster](
	[ProductId] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryId] [bigint] NULL,
	[DatauniqueID] [int] NULL,
	[ActivityType] [varchar](1) NULL,
	[ProductName] [varchar](5000) NULL,
	[IsActive] [varchar](1) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CountryID] [bigint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[prMstproductMaster] ON 

INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (10008, 1304, 1, N'A', N'Basmati', N'Y', CAST(0x0000AAD001127C15 AS DateTime), N'10', NULL, NULL, 3)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (10009, 0, 1, N'A', N'sdfgdgdsf', N'N', CAST(0x0000AB160143A201 AS DateTime), N'10', NULL, NULL, 3)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (13, 1191, 3, N'M', N'jkfhekjwhk5', N'Y', CAST(0x0000AAC20110B1A9 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (14, 1191, 1, N'A', N'jkfhekjwhk6', NULL, CAST(0x0000A96700326D87 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (15, 1191, 2, N'M', N'jkfhekjwhk7', N'Y', CAST(0x0000AAC20117A06C AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (16, 1191, 1, N'A', N'jkfhekjwhk8', NULL, CAST(0x0000A96700326D87 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (17, 1191, 1, N'A', N'jkfhekjwhk9', NULL, CAST(0x0000A96700326D87 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (18, 1191, 1, N'A', N'jkfhekjwhk10', NULL, CAST(0x0000A96700326D87 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (19, 1191, 1, N'A', N'jkfhekjwhk11', NULL, CAST(0x0000A96700326D87 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (20, 1191, 1, N'A', N'jkfhekjwhk12', NULL, CAST(0x0000A96700326D87 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (21, 1191, 1, N'A', N'jkfhekjwhk13', NULL, CAST(0x0000A96700326D87 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (22, 1191, 1, N'A', N'jkfhekjwhk14', NULL, CAST(0x0000A96700326D88 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (23, 1191, 1, N'A', N'jkfhekjwhk15', NULL, CAST(0x0000A96700326D88 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (24, 1191, 1, N'A', N'jkfhekjwhk16', NULL, CAST(0x0000A96700326D88 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (25, 1191, 1, N'A', N'jkfhekjwhk17', NULL, CAST(0x0000A96700326D88 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (26, 1191, 1, N'A', N'jkfhekjwhk18', NULL, CAST(0x0000A96700326D88 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (27, 1191, 1, N'A', N'jkfhekjwhk19', NULL, CAST(0x0000A96700326D89 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (28, 1191, 1, N'A', N'jkfhekjwhk20', NULL, CAST(0x0000A96700326D89 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (29, 1191, 1, N'A', N'jkfhekjwhk21', NULL, CAST(0x0000A96700326D89 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (30, 1191, 1, N'A', N'jkfhekjwhk22', NULL, CAST(0x0000A96700326D89 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (31, 1191, 1, N'A', N'jkfhekjwhk23', NULL, CAST(0x0000A96700326D89 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (32, 1191, 1, N'A', N'jkfhekjwhk24', NULL, CAST(0x0000A96700326D89 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (33, 1191, 1, N'A', N'jkfhekjwhk25', NULL, CAST(0x0000A96700326D89 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (34, 1191, 1, N'A', N'jkfhekjwhk26', NULL, CAST(0x0000A96700326D89 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (35, 1191, 1, N'A', N'jkfhekjwhk27', NULL, CAST(0x0000A96700326D8A AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (36, 1191, 1, N'A', N'jkfhekjwhk28', NULL, CAST(0x0000A96700326D8A AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (37, 1191, 1, N'A', N'jkfhekjwhk29', NULL, CAST(0x0000A96700326D8A AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (38, 1301, 1, N'A', N'Computer', N'Y', CAST(0x0000A967012693D5 AS DateTime), N'0', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (10004, 1301, 1, N'A', N'Mobile1', N'Y', CAST(0x0000A9C500B94718 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (10005, 1301, 1, N'A', N'Laptop1', N'Y', CAST(0x0000A9C500B94E40 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (10006, 1301, 1, N'A', N'Tab1', N'Y', CAST(0x0000A9C500B95013 AS DateTime), N'10', NULL, NULL, 101)
INSERT [dbo].[prMstproductMaster] ([ProductId], [CategoryId], [DatauniqueID], [ActivityType], [ProductName], [IsActive], [LastModifiedOn], [LastModifiedBy], [CreatedOn], [CreatedBy], [CountryID]) VALUES (10007, 1301, 1, N'A', N'Switch1', N'Y', CAST(0x0000A9C500B951D4 AS DateTime), N'10', NULL, NULL, 101)
SET IDENTITY_INSERT [dbo].[prMstproductMaster] OFF
