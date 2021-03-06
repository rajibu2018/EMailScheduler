USE [MailServer]
GO
ALTER TABLE [dbo].[MailSentHistory] DROP CONSTRAINT [FK_MailSentHistory_Users]
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF__tmp_ms_xx__SentP__1DE57479]
GO
ALTER TABLE [dbo].[MailSentHistory] DROP CONSTRAINT [DF__tmp_ms_xx__LinkO__1B0907CE]
GO
ALTER TABLE [dbo].[MailSentHistory] DROP CONSTRAINT [DF__tmp_ms_xx__SentD__1A14E395]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/28/2020 9:12:26 PM ******/
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[MailSentHistory]    Script Date: 5/28/2020 9:12:26 PM ******/
DROP TABLE [dbo].[MailSentHistory]
GO
/****** Object:  Table [dbo].[MailSentHistory]    Script Date: 5/28/2020 9:12:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailSentHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SentDate] [datetime] NOT NULL,
	[LinkUID] [uniqueidentifier] NOT NULL,
	[LinkOpned] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/28/2020 9:12:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[MailId] [varchar](500) NOT NULL,
	[SentPrimaryMail] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[MailSentHistory] ON 

GO
INSERT [dbo].[MailSentHistory] ([Id], [UserId], [SentDate], [LinkUID], [LinkOpned]) VALUES (1, 1, CAST(0x0000ABC9014B345E AS DateTime), N'10d83b70-f150-44b1-9250-d4c4610dfe71', 1)
GO
INSERT [dbo].[MailSentHistory] ([Id], [UserId], [SentDate], [LinkUID], [LinkOpned]) VALUES (2, 2, CAST(0x0000ABC9014B3473 AS DateTime), N'9211b26f-26b3-4c22-a321-79f7514835ad', 0)
GO
INSERT [dbo].[MailSentHistory] ([Id], [UserId], [SentDate], [LinkUID], [LinkOpned]) VALUES (3, 3, CAST(0x0000ABC9014B347A AS DateTime), N'a1478871-dd99-4304-8631-3d83da748530', 0)
GO
SET IDENTITY_INSERT [dbo].[MailSentHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

GO
INSERT [dbo].[Users] ([Id], [Name], [MailId], [SentPrimaryMail]) VALUES (1, N'Rajib Dutta', N'rajibu2010@yahoo.com', 1)
GO
INSERT [dbo].[Users] ([Id], [Name], [MailId], [SentPrimaryMail]) VALUES (2, N'Mr. Manjunath', N'manju@xyz.com', 1)
GO
INSERT [dbo].[Users] ([Id], [Name], [MailId], [SentPrimaryMail]) VALUES (3, N'Mr. Alok', N'alok@abc.com', 1)
GO
INSERT [dbo].[Users] ([Id], [Name], [MailId], [SentPrimaryMail]) VALUES (4, N'Mr. Muller', N'muller@pqr.com', 0)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[MailSentHistory] ADD  DEFAULT (getdate()) FOR [SentDate]
GO
ALTER TABLE [dbo].[MailSentHistory] ADD  DEFAULT ((0)) FOR [LinkOpned]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [SentPrimaryMail]
GO
ALTER TABLE [dbo].[MailSentHistory]  WITH CHECK ADD  CONSTRAINT [FK_MailSentHistory_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MailSentHistory] CHECK CONSTRAINT [FK_MailSentHistory_Users]
GO
