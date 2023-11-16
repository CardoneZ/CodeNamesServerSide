
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/05/2023 20:26:19
-- Generated from EDMX file: A:\CodeNamesHouse\Server_CodeNames\DataModels\CodeNamesBD.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CodeNamesBD];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PlayerRequest_Sender]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FriendshipRequestSet] DROP CONSTRAINT [FK_PlayerRequest_Sender];
GO
IF OBJECT_ID(N'[dbo].[FK_PlayerRequest_Receiver]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FriendshipRequestSet] DROP CONSTRAINT [FK_PlayerRequest_Receiver];
GO
IF OBJECT_ID(N'[dbo].[FK_PlayerFriendship1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FriendshipSet] DROP CONSTRAINT [FK_PlayerFriendship1];
GO
IF OBJECT_ID(N'[dbo].[FK_PlayerFriendship2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FriendshipSet] DROP CONSTRAINT [FK_PlayerFriendship2];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PlayerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlayerSet];
GO
IF OBJECT_ID(N'[dbo].[FriendshipRequestSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FriendshipRequestSet];
GO
IF OBJECT_ID(N'[dbo].[FriendshipSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FriendshipSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PlayerSet'
CREATE TABLE [dbo].[PlayerSet] (
    [IdPlayer] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(30)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Password] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'FriendshipRequestSet'
CREATE TABLE [dbo].[FriendshipRequestSet] (
    [IdFriendshipRequest] int IDENTITY(1,1) NOT NULL,
    [IdSenderPlayer] int  NOT NULL,
    [IdReceiverPlayer] int  NOT NULL
);
GO

-- Creating table 'FriendshipSet'
CREATE TABLE [dbo].[FriendshipSet] (
    [IdFriendship] int IDENTITY(1,1) NOT NULL,
    [IdPlayer1] int  NOT NULL,
    [IdPlayer2] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdPlayer] in table 'PlayerSet'
ALTER TABLE [dbo].[PlayerSet]
ADD CONSTRAINT [PK_PlayerSet]
    PRIMARY KEY CLUSTERED ([IdPlayer] ASC);
GO

-- Creating primary key on [IdFriendshipRequest] in table 'FriendshipRequestSet'
ALTER TABLE [dbo].[FriendshipRequestSet]
ADD CONSTRAINT [PK_FriendshipRequestSet]
    PRIMARY KEY CLUSTERED ([IdFriendshipRequest] ASC);
GO

-- Creating primary key on [IdFriendship] in table 'FriendshipSet'
ALTER TABLE [dbo].[FriendshipSet]
ADD CONSTRAINT [PK_FriendshipSet]
    PRIMARY KEY CLUSTERED ([IdFriendship] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdSenderPlayer] in table 'FriendshipRequestSet'
ALTER TABLE [dbo].[FriendshipRequestSet]
ADD CONSTRAINT [FK_PlayerRequest_Sender]
    FOREIGN KEY ([IdSenderPlayer])
    REFERENCES [dbo].[PlayerSet]
        ([IdPlayer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerRequest_Sender'
CREATE INDEX [IX_FK_PlayerRequest_Sender]
ON [dbo].[FriendshipRequestSet]
    ([IdSenderPlayer]);
GO

-- Creating foreign key on [IdReceiverPlayer] in table 'FriendshipRequestSet'
ALTER TABLE [dbo].[FriendshipRequestSet]
ADD CONSTRAINT [FK_PlayerRequest_Receiver]
    FOREIGN KEY ([IdReceiverPlayer])
    REFERENCES [dbo].[PlayerSet]
        ([IdPlayer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerRequest_Receiver'
CREATE INDEX [IX_FK_PlayerRequest_Receiver]
ON [dbo].[FriendshipRequestSet]
    ([IdReceiverPlayer]);
GO

-- Creating foreign key on [IdPlayer1] in table 'FriendshipSet'
ALTER TABLE [dbo].[FriendshipSet]
ADD CONSTRAINT [FK_PlayerFriendship1]
    FOREIGN KEY ([IdPlayer1])
    REFERENCES [dbo].[PlayerSet]
        ([IdPlayer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerFriendship1'
CREATE INDEX [IX_FK_PlayerFriendship1]
ON [dbo].[FriendshipSet]
    ([IdPlayer1]);
GO

-- Creating foreign key on [IdPlayer2] in table 'FriendshipSet'
ALTER TABLE [dbo].[FriendshipSet]
ADD CONSTRAINT [FK_PlayerFriendship2]
    FOREIGN KEY ([IdPlayer2])
    REFERENCES [dbo].[PlayerSet]
        ([IdPlayer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerFriendship2'
CREATE INDEX [IX_FK_PlayerFriendship2]
ON [dbo].[FriendshipSet]
    ([IdPlayer2]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------