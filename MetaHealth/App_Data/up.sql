CREATE TABLE [dbo].[SepMoods]
(
    [PK]   INT IDENTITY(1,1) NOT NULL,
    [UserID]  NVARCHAR (128) NOT NULL,
    [MoodNum] INT NOT NULL,
    [Date] DATETIME NOT NULL,
    [Reason] NVARCHAR (1000),
    CONSTRAINT [PK_dbo.SepMoods] PRIMARY KEY CLUSTERED ([PK] ASC),
);


CREATE TABLE [dbo].[MoodsInBetween]
(
    [PK]   INT IDENTITY(1,1) NOT NULL,
    [FK_UserTable] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.MoodsInBetween] PRIMARY KEY CLUSTERED ([PK] ASC),
    CONSTRAINT [FK_dbo.MoodsInBetween_dbo.AspNetUsers_UserId] FOREIGN KEY ([FK_UserTable]) REFERENCES [dbo].[AspNetUsers] ([Id])

);

CREATE TABLE [dbo].[Moods]
(
    [PK]   INT IDENTITY(1,1) NOT NULL,
    [FK_MoodsInBetween] INT NOT NULL,
    [MoodNum] INT NOT NULL,
    [Date] DATETIME NOT NULL,
    CONSTRAINT [PK_dbo.Moods] PRIMARY KEY CLUSTERED ([PK] ASC),
    CONSTRAINT [FK_dbo.Moods_dbo.MoodsInBetween] FOREIGN KEY ([FK_MoodsInBetween]) REFERENCES [dbo].[MoodsInBetween] ([PK])

);

CREATE TABLE [dbo].[List]
(
    [PK]   INT IDENTITY(1,1) NOT NULL,
    [FK_UserTable] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.List] PRIMARY KEY CLUSTERED ([PK] ASC),
    CONSTRAINT [FK_dbo.List_dbo.AspNetUsers_UserId] FOREIGN KEY ([FK_UserTable]) REFERENCES [dbo].[AspNetUsers] ([Id])

);

CREATE TABLE [dbo].[CustomLevel]
(
    [PK]   INT IDENTITY(1,1) NOT NULL,
    [FK_List] INT NOT NULL,
    [Task] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.CustomLevel] PRIMARY KEY CLUSTERED ([PK] ASC),
    CONSTRAINT [FK_dbo.CustomLevel_dbo.List] FOREIGN KEY ([FK_List]) REFERENCES [dbo].[List] ([PK])

);

CREATE TABLE [dbo].[ToDoList]
(
    [PK]   INT IDENTITY(1,1) NOT NULL,
    [FK_List] INT NOT NULL,
    [Task] NVARCHAR (256) NOT NULL,
    [Checked] BIT NOT NULL,
    CONSTRAINT [PK_dbo.ToDoList] PRIMARY KEY CLUSTERED ([PK] ASC),
    CONSTRAINT [FK_dbo.ToDoList_dbo.List] FOREIGN KEY ([FK_List]) REFERENCES [dbo].[List] ([PK])

);

CREATE TABLE [dbo].[APIToDoList]
(
    [PK]   INT IDENTITY(1,1) NOT NULL,
    [FK_List] INT NOT NULL,
    [CalandarID] NVARCHAR (256) NOT NULL,
    [EventID] NVARCHAR (256) NOT NULL,
    [Checked] BIT NOT NULL,
    CONSTRAINT [PK_dbo.APIToDoList] PRIMARY KEY CLUSTERED ([PK] ASC),
    CONSTRAINT [FK_dbo.APIToDoList_dbo.List] FOREIGN KEY ([FK_List]) REFERENCES [dbo].[List] ([PK])

);

CREATE TABLE [dbo].[PreLevelList]
(
     [PK]   INT IDENTITY(1,1) NOT NULL,
     [Level] INT NOT NULL,
     [Task] NVARCHAR (256) NOT NULL,
     CONSTRAINT [PK_dbo.PreLevelList] PRIMARY KEY CLUSTERED ([PK] ASC)
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

