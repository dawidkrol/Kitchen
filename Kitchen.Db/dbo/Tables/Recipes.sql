CREATE TABLE [dbo].[Recipes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Id_Subcategory] INT NOT NULL, 
    [Title] NVARCHAR(150) NOT NULL, 
    [Recipe] NVARCHAR(MAX) NOT NULL, 
    [NumberOfServings] INT NULL, 
    [EstimatedValue] MONEY NULL, 
    [CaloriesPerServingsInGrams] INT NULL, 
    [ProteinsPerServingsInGrams] INT NULL, 
    [FatsPerServingsInGrams] INT NULL, 
    [CarbohydratesPerServingsInGrams] INT NULL, 
    [OriginId] INT NULL, 
    [CreatedDate] DATETIME2 NOT NULL, 
    [IsActive] BIT NOT NULL, 
    CONSTRAINT [FK_Origin_ToTable] FOREIGN KEY (OriginId) REFERENCES Origin(Id),
    CONSTRAINT [FK_Subcategory_ToTable] FOREIGN KEY (Id_Subcategory) REFERENCES Subcategories(Id)
)
