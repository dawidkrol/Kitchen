CREATE PROCEDURE [dbo].[spSubcategories_GetByCategoryId]
	@CategoryId int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT p.Id, p.[Name] FROM [dbo].[Subcategories] as p 
	WHERE @CategoryId = p.CategoryId
	ORDER BY p.[Name]
END
