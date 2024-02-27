CREATE TABLE AlterEgos (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [SuperHeroId] INT,
    [AlterEgo] VARCHAR(255),
    FOREIGN KEY (SuperHeroId) REFERENCES SuperHero(Id)
);