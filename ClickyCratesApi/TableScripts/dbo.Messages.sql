CREATE TABLE [dbo].[Messages] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [IdPlayer]    NVARCHAR (128) NOT NULL,
    [Messages]    TEXT           NULL,
    [MessageHour] NVARCHAR (128) NULL,
    CONSTRAINT [FK_dbo.Messages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Messages_AspNetUsers] FOREIGN KEY ([IdPlayer]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

