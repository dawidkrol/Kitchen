using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.Library.DataModels
{
    public class CategoryData
    {
        public int Id { get; set; }
        public string NazwaKategorii { get; set; }
        public List<SubCategoriesData> SubDirectories { get; set; }
    }
}
