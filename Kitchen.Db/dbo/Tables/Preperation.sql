CREATE TABLE [dbo].[Preperation]
(
	[AuthorId] VARCHAR(450) NOT NULL, 
    [RecipeId] INT NOT NULL,
	CONSTRAINT [FK_Author_ToTable] FOREIGN KEY (AuthorId) REFERENCES Author(Id),
	CONSTRAINT [FK_Recipe_ToTable] FOREIGN KEY ([RecipeId]) REFERENCES Recipes(Id), 
    CONSTRAINT [PK_Preperation] PRIMARY KEY ([RecipeId], [AuthorId])

)
