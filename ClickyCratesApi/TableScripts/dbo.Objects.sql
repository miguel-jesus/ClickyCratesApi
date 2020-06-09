CREATE TABLE [dbo].[Objects] (
    [Id]     NVARCHAR (128) NOT NULL,
    [Synti]  NVARCHAR (128) DEFAULT ((0)) NULL,
    [Box]    NVARCHAR (128) DEFAULT ((0)) NULL,
    [Barrel] NVARCHAR (128) DEFAULT ((0)) NULL,
    [Skull]  NVARCHAR (128) DEFAULT ((0)) NULL,
    CONSTRAINT [FK_dbo.Objects] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Objects_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

