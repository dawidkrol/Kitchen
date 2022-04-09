using System.Collections.Generic;

namespace Kitchen.Library.DataModels
{
    public class CategoryData
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<SubcategoriesData> SubDirectories { get; set; }
    }
}
