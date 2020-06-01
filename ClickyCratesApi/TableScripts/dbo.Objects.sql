CREATE TABLE [dbo].[Objects]
(
	[Id]        NVARCHAR (128) NOT NULL,
    [Synti] NVARCHAR (128) NULL DEFAULT 0,
    [Box]  NVARCHAR (128) NULL DEFAULT 0,
    [Barrel]  NVARCHAR (128) NULL DEFAULT 0,
    [Skull]      NVARCHAR (128) NULL DEFAULT 0,
    CONSTRAINT [FK_dbo.Objects] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Objects_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
)
