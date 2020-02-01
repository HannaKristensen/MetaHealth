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




-- #######################################
-- #             Identity Tables         #
-- #######################################

-- ############# AspNetRoles #############
CREATE TABLE [dbo].[AspNetRoles]
(
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);

-- ############# AspNetUsers #############
CREATE TABLE [dbo].[AspNetUsers]
(
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]([UserName] ASC);

-- ############# AspNetUserClaims #############
CREATE TABLE [dbo].[AspNetUserClaims]
(
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]([UserId] ASC);

-- ############# AspNetUserLogins #############
CREATE TABLE [dbo].[AspNetUserLogins]
(
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]([UserId] ASC);

-- ############# AspNetUserRoles #############
CREATE TABLE [dbo].[AspNetUserRoles]
(
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]([UserId] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]([RoleId] ASC);