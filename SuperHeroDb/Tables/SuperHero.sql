CREATE TABLE SuperHero (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] VARCHAR(255),
    [FullName] VARCHAR(255),
    [Alignment] VARCHAR(50),
    [ImageUri] VARCHAR(255),
    [Gender] VARCHAR(10),
    [Height] INT,
    [Weight] INT,
    [Intelligence] INT,
    [Strength] INT,
    [Speed] INT,
    [Durability] INT,
    [Power] INT,
    [Combat] INT,
    [EyeColor] VARCHAR(50),
    [HairColor] VARCHAR(50)
);