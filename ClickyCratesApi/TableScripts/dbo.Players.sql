CREATE TABLE [dbo].[Players] (
    [Id]            NVARCHAR (128) NOT NULL,
    [FirstName]     NVARCHAR (128) NULL,
    [LastName]      NVARCHAR (128) NULL,
    [NickName]      NVARCHAR (128) NULL,
    [City]          NVARCHAR (128) NULL,
    [BirthDay]      NVARCHAR (128) NULL,
    [IsOnline]      BIT            DEFAULT ((0)) NOT NULL,
    [LastLogin]     NVARCHAR (128) NULL,
    [HourGameScene] NVARCHAR (128) NULL,
    [IsBanned]      BIT            DEFAULT ((0)) NOT NULL,
    [BannedHour]    NVARCHAR (128) NULL,
    CONSTRAINT [FK_dbo.Players] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Players_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

