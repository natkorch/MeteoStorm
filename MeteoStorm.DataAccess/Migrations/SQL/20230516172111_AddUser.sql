BEGIN TRANSACTION;
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Login] nvarchar(32) NOT NULL,
    [PasswordHash] nvarchar(256) NOT NULL,
    [Role] nvarchar(32) NOT NULL,
    [IsActive] bit NOT NULL,
    [CityId] int NULL,
    [FirstName] nvarchar(64) NULL,
    [Patronymic] nvarchar(64) NULL,
    [LastName] nvarchar(64) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id])
);
GO

CREATE INDEX [IX_Users_CityId] ON [Users] ([CityId]);
GO

INSERT INTO Users (Login, PasswordHash, IsActive, Role) VALUES ('admin', 'AQAAAAIAAYagAAAAEAlUYB8DEfjBJ+HD5HT0UjZdR2KK0f8MQyt5+Fvhd36gSPt4w88MhjdDbHhHpRkrzw==', 1, 'admin')
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230516172111_AddUser', N'7.0.5');
GO

COMMIT;
GO