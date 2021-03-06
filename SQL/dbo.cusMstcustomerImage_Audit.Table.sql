USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[cusMstcustomerImage_Audit]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cusMstcustomerImage_Audit](
	[ImageId] [bigint] NOT NULL,
	[CustomerId] [bigint] NULL,
	[OrganizationID] [bigint] NULL,
	[DatauniqueID] [int] NULL,
	[FileData] [image] NULL,
	[FileName] [varchar](200) NULL,
	[FileType] [varchar](150) NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [varchar](50) NULL,
	[SEQ] [int] NULL,
	[isMain] [varchar](1) NULL,
	[IsActive] [varchar](1) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
