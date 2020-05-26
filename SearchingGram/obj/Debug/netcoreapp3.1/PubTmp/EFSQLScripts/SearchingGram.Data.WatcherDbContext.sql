IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409200650_SecondInit')
BEGIN
    CREATE TABLE [Watchers] (
        [Id] int NOT NULL IDENTITY,
        CONSTRAINT [PK_Watchers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409200650_SecondInit')
BEGIN
    CREATE TABLE [Monitors] (
        [Id] int NOT NULL IDENTITY,
        [WatchOwnerId] int NOT NULL,
        CONSTRAINT [PK_Monitors] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Monitors_Watchers_WatchOwnerId] FOREIGN KEY ([WatchOwnerId]) REFERENCES [Watchers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409200650_SecondInit')
BEGIN
    CREATE TABLE [InstaAccounts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [MonitorOwnerId] int NOT NULL,
        CONSTRAINT [PK_InstaAccounts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_InstaAccounts_Monitors_MonitorOwnerId] FOREIGN KEY ([MonitorOwnerId]) REFERENCES [Monitors] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409200650_SecondInit')
BEGIN
    CREATE TABLE [TikTokAccounts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [MonitorOwnerId] int NOT NULL,
        CONSTRAINT [PK_TikTokAccounts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TikTokAccounts_Monitors_MonitorOwnerId] FOREIGN KEY ([MonitorOwnerId]) REFERENCES [Monitors] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409200650_SecondInit')
BEGIN
    CREATE TABLE [TwitterAccounts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [MonitorOwnerId] int NOT NULL,
        CONSTRAINT [PK_TwitterAccounts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TwitterAccounts_Monitors_MonitorOwnerId] FOREIGN KEY ([MonitorOwnerId]) REFERENCES [Monitors] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409200650_SecondInit')
BEGIN
    CREATE INDEX [IX_InstaAccounts_MonitorOwnerId] ON [InstaAccounts] ([MonitorOwnerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409200650_SecondInit')
BEGIN
    CREATE INDEX [IX_Monitors_WatchOwnerId] ON [Monitors] ([WatchOwnerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409200650_SecondInit')
BEGIN
    CREATE INDEX [IX_TikTokAccounts_MonitorOwnerId] ON [TikTokAccounts] ([MonitorOwnerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409200650_SecondInit')
BEGIN
    CREATE INDEX [IX_TwitterAccounts_MonitorOwnerId] ON [TwitterAccounts] ([MonitorOwnerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409200650_SecondInit')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200409200650_SecondInit', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409202231_AddedName')
BEGIN
    ALTER TABLE [Watchers] ADD [Name] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409202231_AddedName')
BEGIN
    ALTER TABLE [Monitors] ADD [Name] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409202231_AddedName')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200409202231_AddedName', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409204719_NewThird')
BEGIN
    ALTER TABLE [Monitors] DROP CONSTRAINT [FK_Monitors_Watchers_WatchOwnerId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409204719_NewThird')
BEGIN
    DROP INDEX [IX_Monitors_WatchOwnerId] ON [Monitors];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409204719_NewThird')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Monitors]') AND [c].[name] = N'WatchOwnerId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Monitors] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Monitors] DROP COLUMN [WatchOwnerId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409204719_NewThird')
BEGIN
    ALTER TABLE [Monitors] ADD [WatcherId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409204719_NewThird')
BEGIN
    CREATE INDEX [IX_Monitors_WatcherId] ON [Monitors] ([WatcherId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409204719_NewThird')
BEGIN
    ALTER TABLE [Monitors] ADD CONSTRAINT [FK_Monitors_Watchers_WatcherId] FOREIGN KEY ([WatcherId]) REFERENCES [Watchers] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200409204719_NewThird')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200409204719_NewThird', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200413124225_AddedStatistic')
BEGIN
    ALTER TABLE [TikTokAccounts] ADD [_growsFollowers] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200413124225_AddedStatistic')
BEGIN
    ALTER TABLE [TikTokAccounts] ADD [_growsLikes] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200413124225_AddedStatistic')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [_growsComments] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200413124225_AddedStatistic')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [_growsFollowers] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200413124225_AddedStatistic')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [_growsLikes] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200413124225_AddedStatistic')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200413124225_AddedStatistic', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200419095451_Added_Twitter')
BEGIN
    ALTER TABLE [TwitterAccounts] ADD [_growsFollowers] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200419095451_Added_Twitter')
BEGIN
    ALTER TABLE [TwitterAccounts] ADD [_growsRetweets] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200419095451_Added_Twitter')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200419095451_Added_Twitter', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200515125624_InstaPic')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Pic] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200515125624_InstaPic')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200515125624_InstaPic', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200515133500_Pics_Stage_2')
BEGIN
    ALTER TABLE [TwitterAccounts] ADD [Pic] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200515133500_Pics_Stage_2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200515133500_Pics_Stage_2', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [TwitterAccounts] ADD [FollowerCount] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [TwitterAccounts] ADD [MaxRetweets] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [TwitterAccounts] ADD [MaxRetweets_Text] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [TwitterAccounts] ADD [MinRetweets] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [TwitterAccounts] ADD [MinRetweets_Text] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [TwitterAccounts] ADD [RetweetsCount] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [TwitterAccounts] ADD [ScreenName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [TwitterAccounts] ADD [_retweetsList] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Biography] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Business_category_name] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Comments] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Follow] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Followers] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Full_name] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Is_business_account] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Is_error] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Likes] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Max_comments] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Max_comments_pic] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Max_likes] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Max_likes_pic] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Min_comments] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Min_comments_pic] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Min_likes] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [Min_likes_pic] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [_commentsList] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    ALTER TABLE [InstaAccounts] ADD [_likesList] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    CREATE TABLE [YouTubeAccounts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [MonitorOwnerId] int NOT NULL,
        [ChanelId] nvarchar(max) NULL,
        [Subscribers] nvarchar(max) NULL,
        [Views] nvarchar(max) NULL,
        [_viewsList] nvarchar(max) NULL,
        [_likes] nvarchar(max) NULL,
        [_dislikes] nvarchar(max) NULL,
        [_mostLiked] bigint NOT NULL,
        [_mostDisliked] bigint NOT NULL,
        [_commentsCounts] nvarchar(max) NULL,
        [VideosCount] nvarchar(max) NULL,
        [_videoNames] nvarchar(max) NULL,
        [_viewsGrows] nvarchar(max) NULL,
        CONSTRAINT [PK_YouTubeAccounts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_YouTubeAccounts_Monitors_MonitorOwnerId] FOREIGN KEY ([MonitorOwnerId]) REFERENCES [Monitors] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    CREATE INDEX [IX_YouTubeAccounts_MonitorOwnerId] ON [YouTubeAccounts] ([MonitorOwnerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200521212118_YouTube_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200521212118_YouTube_Added', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200522131052_TryFix')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InstaAccounts]') AND [c].[name] = N'Is_error');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [InstaAccounts] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [InstaAccounts] DROP COLUMN [Is_error];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200522131052_TryFix')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200522131052_TryFix', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200522135419_TryFix2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200522135419_TryFix2', N'3.1.1');
END;

GO

