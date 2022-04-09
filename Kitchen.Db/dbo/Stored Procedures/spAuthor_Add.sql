CREATE PROCEDURE [dbo].[spAuthor_Add]
 @Id varchar(255),
 @Name varchar(50),
 @Surname varchar(50),
 @Email varchar(50),
 @PhoneNumber varchar(9)
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO [dbo].[Author] (ID, [Name], Surname, Email, PhoneNumber)
	VALUES(@Id,@Name,@Surname,@Email,@PhoneNumber)
END
