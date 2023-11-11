CREATE DATABASE [SpecPattern]
GO
USE [SpecPattern]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Director](
	[DirectorID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Director] PRIMARY KEY CLUSTERED 
(
	[DirectorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Movie](
	[MovieID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ReleaseDate] [datetime2] NOT NULL,
	[MpaaRating] [int] NOT NULL,
	[Genre] [varchar](50) NOT NULL,
	[Rating] [float] NOT NULL,
	[DirectorID] [bigint] NOT NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Director] ON 

GO
INSERT [dbo].[Director] ([DirectorID], [Name]) VALUES (1, N'Marc Webb')
GO
INSERT [dbo].[Director] ([DirectorID], [Name]) VALUES (2, N'Bill Condon')
GO
INSERT [dbo].[Director] ([DirectorID], [Name]) VALUES (3, N'Chris Renaud')
GO
INSERT [dbo].[Director] ([DirectorID], [Name]) VALUES (4, N'Jon Favreau')
GO
INSERT [dbo].[Director] ([DirectorID], [Name]) VALUES (5, N'M. Night Shyamalan')
GO
INSERT [dbo].[Director] ([DirectorID], [Name]) VALUES (6, N'Alex Kurtzman')
GO
SET IDENTITY_INSERT [dbo].[Director] OFF
GO
SET IDENTITY_INSERT [dbo].[Movie] ON 
GO
DECLARE @now DateTime2;
SET @now = CAST(SYSDATETIME() AS DATE);
INSERT [dbo].[Movie] ([MovieID], [Name], [ReleaseDate], [MpaaRating], [Genre], [Rating], [DirectorID]) VALUES (1, N'The Amazing Spider-Man', DATEADD(DAY, -29, DATEADD(MONTH, -11, DATEADD(YEAR, -4, @now))), 3, N'Adventure', 7, 1)
INSERT [dbo].[Movie] ([MovieID], [Name], [ReleaseDate], [MpaaRating], [Genre], [Rating], [DirectorID]) VALUES (2, N'Beauty and the Beast', DATEADD(DAY, -15, DATEADD(MONTH, -3, @now)), 3, N'Family', 7.8, 2)
INSERT [dbo].[Movie] ([MovieID], [Name], [ReleaseDate], [MpaaRating], [Genre], [Rating], [DirectorID]) VALUES (3, N'The Secret Life of Pets', DATEADD(DAY, -20, DATEADD(MONTH, -11, @now)), 1, N'Adventure', 6.6, 3)
INSERT [dbo].[Movie] ([MovieID], [Name], [ReleaseDate], [MpaaRating], [Genre], [Rating], [DirectorID]) VALUES (4, N'The Jungle Book', DATEADD(DAY, -15, DATEADD(MONTH, -2, DATEADD(YEAR, -1, @now))), 2, N'Fantasy', 7.5, 4)
INSERT [dbo].[Movie] ([MovieID], [Name], [ReleaseDate], [MpaaRating], [Genre], [Rating], [DirectorID]) VALUES (5, N'Split', DATEADD(DAY, -10, DATEADD(MONTH, -5, @now)), 3, N'Horror', 7.4, 5)
INSERT [dbo].[Movie] ([MovieID], [Name], [ReleaseDate], [MpaaRating], [Genre], [Rating], [DirectorID]) VALUES (6, N'The Mummy', DATEADD(DAY, 8, @now), 4, N'Action', 6.7, 6)
GO
SET IDENTITY_INSERT [dbo].[Movie] OFF
GO
ALTER TABLE [dbo].[Movie]  WITH CHECK ADD  CONSTRAINT [FK_Movie_Director] FOREIGN KEY([DirectorID])
REFERENCES [dbo].[Director] ([DirectorID])
GO
ALTER TABLE [dbo].[Movie] CHECK CONSTRAINT [FK_Movie_Director]
GO
