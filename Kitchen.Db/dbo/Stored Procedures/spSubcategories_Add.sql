CREATE PROCEDURE [dbo].[spSubcategories_Add]
	@CategoryId int,
	@Name nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Subcategories](CategoryId, [Name])
	VALUES(@CategoryId, @Name)
END
