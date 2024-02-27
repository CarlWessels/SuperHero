CREATE PROC [dbo].[InsertSuperHero]
(
	@SuperHero NVARCHAR(MAX),
	@SuperHeroId int OUTPUT
)
AS
BEGIN

	INSERT INTO [dbo].[Superhero]
           ([Name]
           ,[FullName]
           ,[Alignment]
           ,[ImageUri]
           ,[Gender]
           ,[Height]
           ,[Weight]
           ,[Intelligence]
           ,[Strength]
           ,[Speed]
           ,[Durability]
           ,[Power]
           ,[Combat]
           ,[EyeColor]
           ,[HairColor])
     SELECT 
            JSON_VALUE(@SuperHero, '$.Name')
           ,JSON_VALUE(@SuperHero, '$.FullName')
           ,JSON_VALUE(@SuperHero, '$.Alignment')
           ,JSON_VALUE(@SuperHero, '$.ImageUri')
           ,JSON_VALUE(@SuperHero, '$.Gender')
           ,JSON_VALUE(@SuperHero, '$.Height')
           ,JSON_VALUE(@SuperHero, '$.Weight')
           ,JSON_VALUE(@SuperHero, '$.Powerstats.Intelligence')
           ,JSON_VALUE(@SuperHero, '$.Powerstats.Strength')
           ,JSON_VALUE(@SuperHero, '$.Powerstats.Speed')
           ,JSON_VALUE(@SuperHero, '$.Powerstats.Durability')
           ,JSON_VALUE(@SuperHero, '$.Powerstats.Power')
           ,JSON_VALUE(@SuperHero, '$.Powerstats.Combat')
           ,JSON_VALUE(@SuperHero, '$.EyeColor')
           ,JSON_VALUE(@SuperHero, '$.HairColor')
	SELECT @SuperHeroId  = scope_identity()

	INSERT INTO [dbo].[AlterEgos]
           ([SuperHeroId]
           ,[AlterEgo])

    SELECT @SuperHeroId, [value]
	FROM OPENJSON(@SuperHero, '$.AlterEgos')

	INSERT INTO [dbo].[Aliases]
           ([SuperHeroId]
           ,[Alias])
    SELECT @SuperHeroId, [value]
	FROM OPENJSON(@SuperHero, '$.Aliases')

END
