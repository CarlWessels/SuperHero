CREATE TABLE Aliases (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [SuperHeroId] INT,
    [Alias] VARCHAR(255),
    FOREIGN KEY (SuperHeroId) REFERENCES SuperHero(Id)
);