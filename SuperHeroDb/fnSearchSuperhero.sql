CREATE FUNCTION fnSearchSuperhero (@searchString VARCHAR(255))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM SuperHero
    WHERE @searchString = '' OR
          ([Name] LIKE '%' + @searchString + '%'
           OR [FullName] LIKE '%' + @searchString + '%'
           OR [Alignment] LIKE '%' + @searchString + '%'
           OR [Gender] LIKE '%' + @searchString + '%'
           OR [EyeColor] LIKE '%' + @searchString + '%'
           OR [HairColor] LIKE '%' + @searchString + '%')
);
