/****** Create database ******/
USE master
GO
IF db_id('[mars_net_dk_db]') IS NOT NULL
BEGIN
	Print 'Database exists..no need to re-create!'
END
ELSE
BEGIN
	Print 'Database does not exists..creating the database..'
	CREATE DATABASE [mars_net_dk_db]
END
GO

USE [mars_net_dk_db]
GO
/****** Object:  Table [dbo].[mars_CCombinedEvents]    Script Date: 14-09-2014 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mars_CCombinedEvents](
	[CombinedEventId] [nvarchar](16) NOT NULL,
	[CombinedSubEventId] [nvarchar](16) NOT NULL,
	[EventId] [nvarchar](16) NOT NULL,
	[SubEventId] [nvarchar](16) NOT NULL,
	[EventNo] [tinyint] NOT NULL,
	[DayNo] [tinyint] NOT NULL,
 CONSTRAINT [PK_CCombinedEvents] PRIMARY KEY CLUSTERED 
(
	[CombinedEventId] ASC,
	[CombinedSubEventId] ASC,
	[EventId] ASC,
	[SubEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[mars_CEvents]    Script Date: 14-09-2014 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mars_CEvents](
	[EventId] [nvarchar](16) NOT NULL,
	[Text] [nvarchar](128) NOT NULL,
	[EventType] [tinyint] NOT NULL,
	[ResultPrecision] [tinyint] NOT NULL,
	[ResultLayout] [tinyint] NOT NULL,
	[ManualTimeAdjustment] [int] NOT NULL,
	[WindMeasure] [char](1) NOT NULL,
	[WindMaxAllowed] [nvarchar](16) NULL,
	[SortKey] [int] NOT NULL,
 CONSTRAINT [PK_CEvents] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mars_CGroupEvents]    Script Date: 14-09-2014 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mars_CGroupEvents](
	[GroupId] [nvarchar](16) NOT NULL,
	[EventId] [nvarchar](16) NOT NULL,
	[SubEventId] [nvarchar](16) NOT NULL,
	[Text] [nvarchar](128) NULL,
	[Indoor] [char](1) NULL,
	[Outdoor] [char](1) NULL,
 CONSTRAINT [PK_CGroupEvents] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[EventId] ASC,
	[SubEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mars_CGroups]    Script Date: 14-09-2014 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mars_CGroups](
	[GroupId] [nvarchar](16) NOT NULL,
	[Age] [nvarchar](64) NOT NULL,
	[Text] [nvarchar](128) NOT NULL,
	[SortKey] [int] NOT NULL,
	[Gender] [char](1) NOT NULL,
 CONSTRAINT [PK_CGroups] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mars_CRecordMarks]    Script Date: 14-09-2014 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mars_CRecordMarks](
	[RecordMarkId] [uniqueidentifier] NOT NULL,
	[RecordId] [nvarchar](16) NOT NULL,
	[EventId] [nvarchar](16) NOT NULL,
	[SubEventId] [nvarchar](16) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[TeamName] [nvarchar](100) NOT NULL,
	[Place] [nvarchar](50) NULL,
	[RecordDate] [nvarchar](20) NOT NULL,
	[Mark] [nvarchar](50) NULL,
	[MarkAddOn] [nvarchar](20) NULL,
	[MarkRemark] [nvarchar](20) NULL,
 CONSTRAINT [PK_CRecordMarks] PRIMARY KEY CLUSTERED 
(
	[RecordMarkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[mars_CRecords]    Script Date: 14-09-2014 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mars_CRecords](
	[RecordId] [nvarchar](16) NOT NULL,
	[Text] [nvarchar](128) NOT NULL,
	[ShortText] [nvarchar](40) NOT NULL,
	[SortKey] [int] NOT NULL,
 CONSTRAINT [PK_CRecordIds] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[mars_CSubEvents]    Script Date: 14-09-2014 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mars_CSubEvents](
	[EventId] [nvarchar](16) NOT NULL,
	[SubEventId] [nvarchar](16) NOT NULL,
	[Text] [nvarchar](128) NOT NULL,
	[SortKey] [int] NOT NULL,
 CONSTRAINT [PK_CSubEvents] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC,
	[SubEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[mars_MEventEntries]    Script Date: 14-09-2014 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mars_MEventEntries](
	[MeetId] [uniqueidentifier] NOT NULL,
	[EventEntryId] [uniqueidentifier] NOT NULL,
	[GroupId] [nvarchar](16) NOT NULL,
	[EventId] [nvarchar](16) NOT NULL,
	[SubEventId] [nvarchar](16) NOT NULL,
	[Text] [nvarchar](128) NOT NULL,
	[SubText] [nvarchar](128) NOT NULL,
	[SortKey] [int] NOT NULL,
	[EventType] [tinyint] NOT NULL,
	[ResultPrecision] [tinyint] NOT NULL,
	[ResultLayout] [tinyint] NOT NULL,
	[ManualTimeAdjustment] [int] NOT NULL,
	[WindMeasure] [char](1) NOT NULL,
	[WindMaxAllowed] [nvarchar](16) NULL,
	[RoundNo] [tinyint] NOT NULL,
	[HeatGroupNo] [tinyint] NOT NULL,
	[DayNo] [tinyint] NULL,
	[Seconds] [int] NULL,
	[Indoor] [char](1) NULL,
	[Outdoor] [char](1) NULL,
	[UserDefined] [tinyint] NULL,
	[SortKeyEventId] [int] NOT NULL,
	[RoundShorthand] [char](1) NULL,
	[RoundText] [nvarchar](50) NULL,
	[NoOfRounds] [int] NULL,
	[Started] [datetime] NULL,
	[Stopped] [datetime] NULL,
	[LiveResults] [nvarchar](max) NULL,
	[State] [char](1) NULL,
 CONSTRAINT [PK_MEventEntries] PRIMARY KEY CLUSTERED 
(
	[MeetId] ASC,
	[EventEntryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mars_MGroups]    Script Date: 14-09-2014 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mars_MGroups](
	[MeetId] [uniqueidentifier] NOT NULL,
	[GroupId] [nvarchar](16) NOT NULL,
	[Age] [nvarchar](64) NOT NULL,
	[Text] [nvarchar](128) NOT NULL,
	[SortKey] [int] NOT NULL,
	[Gender] [char](1) NOT NULL,
 CONSTRAINT [PK_MGroups] PRIMARY KEY CLUSTERED 
(
	[MeetId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mars_MMarks]    Script Date: 14-09-2014 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mars_MMarks](
	[MeetId] [uniqueidentifier] NOT NULL,
	[MarkId] [uniqueidentifier] NOT NULL,
	[ParticipantId] [uniqueidentifier] NOT NULL,
	[EventEntryId] [uniqueidentifier] NOT NULL,
	[No] [int] NULL,
	[LaneNo] [int] NULL,
	[SubLaneNo] [int] NULL,
	[QualifyingMark] [int] NULL,
	[SB] [int] NULL,
	[PB] [int] NULL,
	[Mark] [int] NULL,
	[Points] [int] NULL,
	[QualifyingEntry] [nvarchar](50) NULL,
	[LateEntryCode] [int] NULL,
	[MarkCode] [char](1) NULL,
	[Pos] [int] NULL,
	[PosCode] [int] NULL,
	[MarkRemark] [nvarchar](20) NULL,
	[MarkText] [nvarchar](20) NULL,
 CONSTRAINT [PK_MMarks] PRIMARY KEY CLUSTERED 
(
	[MeetId] ASC,
	[MarkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mars_MMeet]    Script Date: 14-09-2014 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mars_MMeet](
	[MeetId] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](128) NOT NULL,
	[Days] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[Organizer] [nvarchar](128) NULL,
	[Location] [nvarchar](128) NULL,
	[Stadium] [nvarchar](128) NULL,
	[Manager] [nvarchar](128) NULL,
	[ManagerMail] [nvarchar](256) NULL,
	[ManagerPhone] [nvarchar](256) NULL,
	[HomesiteUrl] [nvarchar](256) NULL,
	[StartEntryDate] [datetime] NULL,
	[StopEntryDate] [datetime] NULL,
	[VisibleCode] [nchar](1) NULL,
 CONSTRAINT [PK_MMeet] PRIMARY KEY CLUSTERED 
(
	[MeetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[mars_MParticipants]    Script Date: 14-09-2014 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mars_MParticipants](
	[MeetId] [uniqueidentifier] NOT NULL,
	[ParticipantId] [uniqueidentifier] NOT NULL,
	[TeamId] [uniqueidentifier] NOT NULL,
	[MemberId] [uniqueidentifier] NULL,
	[FullName] [nvarchar](256) NULL,
	[UsedName] [nvarchar](256) NULL,
	[Gender] [char](1) NULL,
	[YearOfBirth] [int] NULL,
	[Nationality] [nvarchar](3) NULL,
	[Mail] [nvarchar](256) NULL,
	[Alias] [nvarchar](10) NULL,
	[DAFNo] [int] NULL,
	[UserDefined] [tinyint] NULL,
	[BibNo] [int] NULL,
 CONSTRAINT [PK_MParticipants] PRIMARY KEY CLUSTERED 
(
	[MeetId] ASC,
	[ParticipantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mars_MTeams]    Script Date: 14-09-2014 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mars_MTeams](
	[MeetId] [uniqueidentifier] NOT NULL,
	[TeamId] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](256) NOT NULL,
	[ShortName] [nvarchar](100) NOT NULL,
	[Name3] [nvarchar](3) NOT NULL,
	[Name6] [nvarchar](6) NOT NULL,
	[Name9] [nvarchar](9) NOT NULL,
	[Nation] [nvarchar](3) NOT NULL,
	[SortKey] [nvarchar](50) NOT NULL,
	[DAFName] [nvarchar](100) NULL,
	[UserDefined] [tinyint] NULL,
 CONSTRAINT [PK_MTeams] PRIMARY KEY CLUSTERED 
(
	[MeetId] ASC,
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[mars_MEventEntries] ADD  DEFAULT ((0)) FOR [SortKeyEventId]
GO
ALTER TABLE [dbo].[mars_CCombinedEvents]  WITH NOCHECK ADD FOREIGN KEY([CombinedEventId], [CombinedSubEventId])
REFERENCES [dbo].[mars_CSubEvents] ([EventId], [SubEventId])
GO
ALTER TABLE [dbo].[mars_CCombinedEvents]  WITH NOCHECK ADD FOREIGN KEY([EventId], [SubEventId])
REFERENCES [dbo].[mars_CSubEvents] ([EventId], [SubEventId])
GO
ALTER TABLE [dbo].[mars_CGroupEvents]  WITH NOCHECK ADD FOREIGN KEY([GroupId])
REFERENCES [dbo].[mars_CGroups] ([GroupId])
GO
ALTER TABLE [dbo].[mars_CGroupEvents]  WITH NOCHECK ADD FOREIGN KEY([EventId], [SubEventId])
REFERENCES [dbo].[mars_CSubEvents] ([EventId], [SubEventId])
GO
ALTER TABLE [dbo].[mars_CRecordMarks]  WITH NOCHECK ADD FOREIGN KEY([RecordId])
REFERENCES [dbo].[mars_CRecords] ([RecordId])
GO
ALTER TABLE [dbo].[mars_CRecordMarks]  WITH NOCHECK ADD FOREIGN KEY([EventId], [SubEventId])
REFERENCES [dbo].[mars_CSubEvents] ([EventId], [SubEventId])
GO
ALTER TABLE [dbo].[mars_CSubEvents]  WITH NOCHECK ADD FOREIGN KEY([EventId])
REFERENCES [dbo].[mars_CEvents] ([EventId])
GO
ALTER TABLE [dbo].[mars_MEventEntries]  WITH NOCHECK ADD FOREIGN KEY([MeetId], [GroupId])
REFERENCES [dbo].[mars_MGroups] ([MeetId], [GroupId])
GO
ALTER TABLE [dbo].[mars_MGroups]  WITH NOCHECK ADD FOREIGN KEY([MeetId])
REFERENCES [dbo].[mars_MMeet] ([MeetId])
GO
ALTER TABLE [dbo].[mars_MMarks]  WITH NOCHECK ADD FOREIGN KEY([MeetId], [ParticipantId])
REFERENCES [dbo].[mars_MParticipants] ([MeetId], [ParticipantId])
GO
ALTER TABLE [dbo].[mars_MMarks]  WITH NOCHECK ADD FOREIGN KEY([MeetId], [EventEntryId])
REFERENCES [dbo].[mars_MEventEntries] ([MeetId], [EventEntryId])
GO
ALTER TABLE [dbo].[mars_MParticipants]  WITH NOCHECK ADD FOREIGN KEY([MeetId], [TeamId])
REFERENCES [dbo].[mars_MTeams] ([MeetId], [TeamId])
GO
ALTER TABLE [dbo].[mars_MTeams]  WITH NOCHECK ADD FOREIGN KEY([MeetId])
REFERENCES [dbo].[mars_MMeet] ([MeetId])
GO

Print 'Database created!'
GO
