namespace Kitchen.Library.DataModels
{
    public class RecipeData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Id_Subcategory { get; set; }
        public string Recipe { get; set; }
        public int? NumberOfServings { get; set; }
        public int? ProteinsPerServingsInGrams { get; set; }
        public int? CarbohydratesPerServingsInGrams { get; set; }
        public int? CaloriesPerServingsInGrams { get; set; }
        public int? EstimatedValue { get; set; }
        public int? FatsPerServingsInGrams { get; set; }
        public int OriginId { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
