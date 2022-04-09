CREATE PROCEDURE [dbo].[spRecipes_GetBySubcategoryId]
	@SubcategoryId int
AS
BEGIN
	SET NOCOUNT ON;

--TODO: For tests

	SELECT r.Id, r.Title, r.Recipe, r.NumberOfServings,
			r.ProteinsPerServingsInGrams, r.FatsPerServingsInGrams,
			r.CarbohydratesPerServingsInGrams, r.CaloriesPerServingsInGrams,
			r.EstimatedValue, a.[Name], a.Surname, a.ID as "UserId", o.Country, o.Region
			FROM [dbo].[Recipes] as r
		LEFT JOIN [dbo].[Origin] as o on o.Id = r.OriginId
		LEFT JOIN [dbo].[Preperation] as p on p.RecipeId = r.Id
		LEFT JOIN [dbo].[Author] as a on a.ID = p.AuthorId
		WHERE @SubcategoryId = r.Id_Subcategory AND r.IsActive = 1
END
