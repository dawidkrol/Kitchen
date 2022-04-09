CREATE PROCEDURE [dbo].[spRecipes_Del]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Recipes
	SET IsActive = 0
	WHERE Id = @Id;
END
