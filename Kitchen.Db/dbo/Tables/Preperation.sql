CREATE TABLE [dbo].[Preperation]
(
	[AuthorId] VARCHAR(450) NOT NULL PRIMARY KEY, 
    [RecipeId] INT NOT NULL PRIMARY KEY,
	CONSTRAINT [FK_Author_ToTable] FOREIGN KEY (AuthorId) REFERENCES Author(Id),
	CONSTRAINT [FK_Recipe_ToTable] FOREIGN KEY ([RecipeId]) REFERENCES Recipes(Id)

)
