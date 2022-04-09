CREATE PROCEDURE [dbo].[spOrigin_Add]
	@Region varchar(50) null,
	@Country varchar(50),
	@NewIdOutputParam int
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Origin(Region, Country)
	VALUES(@Region,@Country)

	SELECT @NewIdOutputParam = SCOPE_IDENTITY()
END
