CREATE TABLE [dbo].[Coaches]
(
    [CoachID]        INT IDENTITY (1,1)    NOT NULL,
    [Name] NVARCHAR(50)        NOT NULL,

    CONSTRAINT [PK_dbo.Coaches] PRIMARY KEY CLUSTERED ([CoachID] ASC)
);

CREATE TABLE [dbo].[Teams]
(
    [TeamID]        INT IDENTITY (1,1)    NOT NULL,
    [Name] NVARCHAR(50)        NOT NULL,
	[CoachID] INT NOT NULL,

    CONSTRAINT [PK_dbo.Teams] PRIMARY KEY CLUSTERED ([TeamID] ASC),
	CONSTRAINT[FK_dbo.Teams] FOREIGN KEY (CoachID) REFERENCES Coaches(CoachID)

);

CREATE TABLE [dbo].[Athletes]
(
    [AthleteID]        INT IDENTITY (1,1)    NOT NULL,
    [Name] NVARCHAR(50)        NOT NULL,
	[Gender] NVARCHAR(20)        NOT NULL,
	[TeamID] INT NOT NULL,

    CONSTRAINT [PK_dbo.Athletes] PRIMARY KEY CLUSTERED ([AthleteID] ASC),
	CONSTRAINT[FK_dbo.Athletes] FOREIGN KEY (TeamID) REFERENCES Teams(TeamID)

);

CREATE TABLE [dbo].[Locations]
(
    [LocationID]        INT IDENTITY (1,1)    NOT NULL,
    [Location] NVARCHAR(50)        NOT NULL,
	[MeetDate] DATETIME        NOT NULL,

    CONSTRAINT [PK_dbo.Locations] PRIMARY KEY CLUSTERED ([LocationID] ASC)
);

CREATE TABLE [dbo].[Results]
(
    [ResultID]        INT IDENTITY (1,1)    NOT NULL,
    [Event] NVARCHAR(20)        NOT NULL,

    CONSTRAINT [PK_dbo.Results] PRIMARY KEY CLUSTERED ([ResultID] ASC)
);

CREATE TABLE [dbo].[AthleteResults]
(
    [AthleteResultsID]        INT IDENTITY (1,1)    NOT NULL,
    [RaceTime] REAL        NOT NULL,
	[LocationID] INT NOT NULL,
	[AthleteID] INT NOT NULL,
	[ResultID] INT NOT NULL,

    CONSTRAINT [PK_dbo.AthleteResults] PRIMARY KEY CLUSTERED ([AthleteResultsID] ASC),
	CONSTRAINT[FK1_dbo.AthleteResults] FOREIGN KEY (ResultID) REFERENCES Results(ResultID),
	CONSTRAINT[FK2_dbo.AthleteResults] FOREIGN KEY (AthleteID) REFERENCES Athletes(AthleteID),
	CONSTRAINT[FK3_dbo.AthleteResults] FOREIGN KEY (LocationID) REFERENCES Locations(LocationID)


);