using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.Library.DataModels
{
    public class SubCategoriesData
    {
        public int ID_Podkategorii { get; set; }
        public string nazwa_podkategorii { get; set; }
        public int ID_Kategorii { get; set; }
    }
}
