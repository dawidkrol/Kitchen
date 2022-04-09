CREATE PROCEDURE [dbo].[spCategories_Add]
	@CategoryName NVARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Categories](CategoryName) VALUES(@CategoryName)
END
