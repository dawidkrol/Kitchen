CREATE TABLE [dbo].[Subcategories]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(255) NOT NULL, 
    [CategoryId] INT NOT NULL,
    CONSTRAINT [FK_Category_ToTable] FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
)
