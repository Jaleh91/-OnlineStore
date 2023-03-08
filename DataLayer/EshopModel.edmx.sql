
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/07/2023 19:47:33
-- Generated from EDMX file: D:\Projects\MyEshop\DataLayer\EshopModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyEshop_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Users_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Group_Product_Group]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Group] DROP CONSTRAINT [FK_Product_Group_Product_Group];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Product_Group]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product_Group];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleID] int  NOT NULL,
    [RoleTitle] nvarchar(250)  NOT NULL,
    [RoleName] varchar(150)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [RoleID] int  NOT NULL,
    [UserName] nvarchar(250)  NOT NULL,
    [Email] nvarchar(520)  NOT NULL,
    [Password] varchar(200)  NOT NULL,
    [ActiveCode] varchar(50)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [RegisterDate] datetime  NOT NULL
);
GO

-- Creating table 'Product_Group'
CREATE TABLE [dbo].[Product_Group] (
    [GroupID] int IDENTITY(1,1) NOT NULL,
    [GroupTitle] nvarchar(400)  NOT NULL,
    [ParentID] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [RoleID] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [GroupID] in table 'Product_Group'
ALTER TABLE [dbo].[Product_Group]
ADD CONSTRAINT [PK_Product_Group]
    PRIMARY KEY CLUSTERED ([GroupID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RoleID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_Roles]
    FOREIGN KEY ([RoleID])
    REFERENCES [dbo].[Roles]
        ([RoleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_Roles'
CREATE INDEX [IX_FK_Users_Roles]
ON [dbo].[Users]
    ([RoleID]);
GO

-- Creating foreign key on [ParentID] in table 'Product_Group'
ALTER TABLE [dbo].[Product_Group]
ADD CONSTRAINT [FK_Product_Group_Product_Group]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[Product_Group]
        ([GroupID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Group_Product_Group'
CREATE INDEX [IX_FK_Product_Group_Product_Group]
ON [dbo].[Product_Group]
    ([ParentID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------