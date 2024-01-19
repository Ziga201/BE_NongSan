IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE TABLE [Decentralization] (
        [DecentralizationID] int NOT NULL IDENTITY,
        [AuthorityName] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdateAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Decentralization] PRIMARY KEY ([DecentralizationID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE TABLE [OrderStatus] (
        [OrderStatusID] int NOT NULL IDENTITY,
        [OrderName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_OrderStatus] PRIMARY KEY ([OrderStatusID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE TABLE [Payment] (
        [PaymentID] int NOT NULL IDENTITY,
        [PaymentMethod] nvarchar(max) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdateAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Payment] PRIMARY KEY ([PaymentID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE TABLE [ProductType] (
        [ProductTypeID] int NOT NULL IDENTITY,
        [NameProductType] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdateAt] datetime2 NOT NULL,
        CONSTRAINT [PK_ProductType] PRIMARY KEY ([ProductTypeID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE TABLE [Account] (
        [AccountID] int NOT NULL IDENTITY,
        [UserName] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [DecentralizationID] int NOT NULL,
        [ResetPasswordToken] nvarchar(max) NULL,
        [ResetPasswordTokenExpiry] datetime2 NULL,
        [Avatar] nvarchar(max) NULL,
        [FullName] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdateAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Account] PRIMARY KEY ([AccountID]),
        CONSTRAINT [FK_Account_Decentralization_DecentralizationID] FOREIGN KEY ([DecentralizationID]) REFERENCES [Decentralization] ([DecentralizationID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE TABLE [Product] (
        [ProductID] int NOT NULL IDENTITY,
        [ProductTypeID] int NOT NULL,
        [NameProduct] nvarchar(max) NOT NULL,
        [Price] real NOT NULL,
        [AvatarImageProduct] nvarchar(max) NULL,
        [Title] nvarchar(max) NULL,
        [Discount] int NULL,
        [Status] nvarchar(max) NULL,
        [NumberOfViews] int NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdateAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Product] PRIMARY KEY ([ProductID]),
        CONSTRAINT [FK_Product_ProductType_ProductTypeID] FOREIGN KEY ([ProductTypeID]) REFERENCES [ProductType] ([ProductTypeID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE TABLE [Cart] (
        [CartID] int NOT NULL IDENTITY,
        [AccountID] int NOT NULL,
        CONSTRAINT [PK_Cart] PRIMARY KEY ([CartID]),
        CONSTRAINT [FK_Cart_Account_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [Account] ([AccountID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE TABLE [ConfirmEmail] (
        [ConfirmEmailID] int NOT NULL IDENTITY,
        [AccountID] int NOT NULL,
        [CodeActive] int NOT NULL,
        [ExpriedTime] datetime2 NOT NULL,
        [IsConfirmed] bit NOT NULL,
        CONSTRAINT [PK_ConfirmEmail] PRIMARY KEY ([ConfirmEmailID]),
        CONSTRAINT [FK_ConfirmEmail_Account_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [Account] ([AccountID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE TABLE [Order] (
        [OrderID] int NOT NULL IDENTITY,
        [PaymentID] int NOT NULL,
        [AccountID] int NOT NULL,
        [OriginalPrice] float NULL,
        [ActualPrice] float NULL,
        [FullName] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Phone] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [OrderStatusID] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdateAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Order] PRIMARY KEY ([OrderID]),
        CONSTRAINT [FK_Order_Account_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [Account] ([AccountID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Order_OrderStatus_OrderStatusID] FOREIGN KEY ([OrderStatusID]) REFERENCES [OrderStatus] ([OrderStatusID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Order_Payment_PaymentID] FOREIGN KEY ([PaymentID]) REFERENCES [Payment] ([PaymentID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE TABLE [ProductImage] (
        [ProductImageID] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [ImageProduct] nvarchar(max) NOT NULL,
        [ProductID] int NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdateAt] datetime2 NOT NULL,
        CONSTRAINT [PK_ProductImage] PRIMARY KEY ([ProductImageID]),
        CONSTRAINT [FK_ProductImage_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE TABLE [ProductReview] (
        [ProductReviewID] int NOT NULL IDENTITY,
        [ProductID] int NOT NULL,
        [AccountID] int NOT NULL,
        [ContentRated] nvarchar(max) NOT NULL,
        [PointEvaluation] int NOT NULL,
        [ContentSeen] nvarchar(max) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdateAt] datetime2 NOT NULL,
        CONSTRAINT [PK_ProductReview] PRIMARY KEY ([ProductReviewID]),
        CONSTRAINT [FK_ProductReview_Account_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [Account] ([AccountID]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductReview_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE TABLE [CartItem] (
        [CartItemID] int NOT NULL IDENTITY,
        [CartID] int NOT NULL,
        [ProductID] int NOT NULL,
        [Quantity] int NOT NULL,
        CONSTRAINT [PK_CartItem] PRIMARY KEY ([CartItemID]),
        CONSTRAINT [FK_CartItem_Cart_CartID] FOREIGN KEY ([CartID]) REFERENCES [Cart] ([CartID]) ON DELETE CASCADE,
        CONSTRAINT [FK_CartItem_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE TABLE [OrderDetail] (
        [OrderDetailID] int NOT NULL IDENTITY,
        [OrderID] int NOT NULL,
        [ProductID] int NOT NULL,
        [PriceTotal] float NOT NULL,
        [Quantity] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdateAt] datetime2 NOT NULL,
        CONSTRAINT [PK_OrderDetail] PRIMARY KEY ([OrderDetailID]),
        CONSTRAINT [FK_OrderDetail_Order_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [Order] ([OrderID]) ON DELETE CASCADE,
        CONSTRAINT [FK_OrderDetail_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_Account_DecentralizationID] ON [Account] ([DecentralizationID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_Cart_AccountID] ON [Cart] ([AccountID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_CartItem_CartID] ON [CartItem] ([CartID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_CartItem_ProductID] ON [CartItem] ([ProductID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_ConfirmEmail_AccountID] ON [ConfirmEmail] ([AccountID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_Order_AccountID] ON [Order] ([AccountID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_Order_OrderStatusID] ON [Order] ([OrderStatusID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_Order_PaymentID] ON [Order] ([PaymentID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_OrderDetail_OrderID] ON [OrderDetail] ([OrderID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_OrderDetail_ProductID] ON [OrderDetail] ([ProductID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_Product_ProductTypeID] ON [Product] ([ProductTypeID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_ProductImage_ProductID] ON [ProductImage] ([ProductID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_ProductReview_AccountID] ON [ProductReview] ([AccountID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    CREATE INDEX [IX_ProductReview_ProductID] ON [ProductReview] ([ProductID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240102162618_init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240102162618_init', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106210047_blog')
BEGIN
    CREATE TABLE [BlogType] (
        [BlogTypeID] int NOT NULL IDENTITY,
        [BlogTypeName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_BlogType] PRIMARY KEY ([BlogTypeID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106210047_blog')
BEGIN
    CREATE TABLE [Blog] (
        [BlogID] int NOT NULL IDENTITY,
        [BlogTypeID] int NOT NULL,
        [AccountID] int NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [Image] nvarchar(max) NOT NULL,
        [CreateAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Blog] PRIMARY KEY ([BlogID]),
        CONSTRAINT [FK_Blog_Account_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [Account] ([AccountID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Blog_BlogType_BlogTypeID] FOREIGN KEY ([BlogTypeID]) REFERENCES [BlogType] ([BlogTypeID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106210047_blog')
BEGIN
    CREATE INDEX [IX_Blog_AccountID] ON [Blog] ([AccountID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106210047_blog')
BEGIN
    CREATE INDEX [IX_Blog_BlogTypeID] ON [Blog] ([BlogTypeID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106210047_blog')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240106210047_blog', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240108235946_message')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Blog]') AND [c].[name] = N'Image');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Blog] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Blog] ALTER COLUMN [Image] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240108235946_message')
BEGIN
    CREATE TABLE [Message] (
        [MessageID] int NOT NULL IDENTITY,
        [AccountID] int NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Topic] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Message] PRIMARY KEY ([MessageID]),
        CONSTRAINT [FK_Message_Account_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [Account] ([AccountID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240108235946_message')
BEGIN
    CREATE INDEX [IX_Message_AccountID] ON [Message] ([AccountID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240108235946_message')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240108235946_message', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240112033418_product')
BEGIN
    EXEC sp_rename N'[Product].[Title]', N'Describe', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240112033418_product')
BEGIN
    EXEC sp_rename N'[Product].[NumberOfViews]', N'Quantity', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240112033418_product')
BEGIN
    ALTER TABLE [Product] ADD [Purchases] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240112033418_product')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240112033418_product', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240112095308_product-review')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductReview]') AND [c].[name] = N'ContentRated');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ProductReview] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [ProductReview] DROP COLUMN [ContentRated];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240112095308_product-review')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductReview]') AND [c].[name] = N'UpdateAt');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ProductReview] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [ProductReview] DROP COLUMN [UpdateAt];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240112095308_product-review')
BEGIN
    EXEC sp_rename N'[ProductReview].[Status]', N'Image', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240112095308_product-review')
BEGIN
    EXEC sp_rename N'[ProductReview].[ContentSeen]', N'Content', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240112095308_product-review')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240112095308_product-review', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240112095542_product-review2')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductReview]') AND [c].[name] = N'Image');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ProductReview] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [ProductReview] ALTER COLUMN [Image] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240112095542_product-review2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240112095542_product-review2', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240116093455_update-blog')
BEGIN
    ALTER TABLE [Blog] ADD [View] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240116093455_update-blog')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240116093455_update-blog', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240116100317_update-order')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Order]') AND [c].[name] = N'ActualPrice');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Order] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Order] DROP COLUMN [ActualPrice];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240116100317_update-order')
BEGIN
    EXEC sp_rename N'[Order].[OriginalPrice]', N'TotalPrice', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240116100317_update-order')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240116100317_update-order', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240116115513_update-product')
BEGIN
    ALTER TABLE [Product] ADD [DiscountedPrice] real NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240116115513_update-product')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240116115513_update-product', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240116124709_update-product2')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'Price');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Product] ALTER COLUMN [Price] int NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240116124709_update-product2')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'DiscountedPrice');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Product] ALTER COLUMN [DiscountedPrice] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240116124709_update-product2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240116124709_update-product2', N'7.0.15');
END;
GO

COMMIT;
GO

