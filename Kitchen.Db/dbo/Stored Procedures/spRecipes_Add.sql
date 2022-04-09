CREATE PROCEDURE [dbo].[spRecipes_Add]
	@Title nvarchar(150),
	@Recipe nvarchar(MAX),
	@Id_Subcategory int,
	@OriginId int,
	@NumberOfServings int null,
	@EstimatedValue money null,
	@FatsPerServingsInGrams int null,
	@ProteinsPerServingsInGrams int null,
	@CaloriesPerServingsInGrams int null,
	@CarbohydratesPerServingsInGrams int null,
	@UserId varchar(450)
AS
BEGIN
	SET NOCOUNT ON;

    Insert into Recipes(Title,Recipe,Id_Subcategory, NumberOfServings, EstimatedValue,
					FatsPerServingsInGrams, ProteinsPerServingsInGrams, 
					CaloriesPerServingsInGrams,CarbohydratesPerServingsInGrams,
					OriginId, IsActive, CreatedDate)
					VALUES(@Title, @Recipe, @Id_Subcategory, @NumberOfServings,
					@EstimatedValue, @FatsPerServingsInGrams, @ProteinsPerServingsInGrams,
					@CaloriesPerServingsInGrams, @CarbohydratesPerServingsInGrams,
					@OriginId, 1, GETUTCDATE());

	Insert into Preperation(AuthorId, RecipeId) values(@UserId, @@IDENTITY)
END

