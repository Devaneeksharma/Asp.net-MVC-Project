
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/08/2019 18:43:18
-- Generated from EDMX file: C:\Users\Devaneek\source\repos\WildernessOdyssey\WildernessOdyssey\Models\WildernessModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-WildernessOdyssey-20190908051350];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Trips]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trips];
GO
IF OBJECT_ID(N'[dbo].[Activities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Activities];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [EmailId] nvarchar(max)  NOT NULL,
    [Phone] int  NOT NULL
);
GO

-- Creating table 'Trips'
CREATE TABLE [dbo].[Trips] (
    [TripId] int IDENTITY(1,1) NOT NULL,
    [TripType] nvarchar(max)  NOT NULL,
    [TripName] nvarchar(max)  NOT NULL,
    [TripLocation] nvarchar(max)  NOT NULL,
    [Duration] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL
);
GO

-- Creating table 'Activities'
CREATE TABLE [dbo].[Activities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ActivityName] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Capacity] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [TripId] in table 'Trips'
ALTER TABLE [dbo].[Trips]
ADD CONSTRAINT [PK_Trips]
    PRIMARY KEY CLUSTERED ([TripId] ASC);
GO

-- Creating primary key on [Id] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [PK_Activities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------