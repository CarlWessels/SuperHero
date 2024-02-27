CREATE TABLE [dbo].[Favorites]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[SuperHeroId] INT NOT NULL,
	[UserId] INT NOT NULL,

	FOREIGN KEY (SuperHeroId) REFERENCES SuperHero(Id),
	FOREIGN KEY (UserId) REFERENCES [User](Id)
)
