using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKP1_Stationeers_Editor
{
    public class RecipeDataFile
    {
        // Hush VS, hush...
#pragma warning disable IDE1006

        public RecipeDataFile()
        {
            recipes = new List<ItemRecipe>();
        }
        public string branch
        {
            get;
            set;
        }
        public UInt64 updated_time
        {
            get;
            set;
        }

        public IList<ItemRecipe> recipes
        {
            get;
            set;
        }
    }
}
