using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.App.Models
{
    public class CulinaryRecipeViewModel
    {
        public string Name { get; set; }
        public string Recipe { get; set; }
        public int Servings { get; set; }
        public string Origin { get; set; }
    }
}
