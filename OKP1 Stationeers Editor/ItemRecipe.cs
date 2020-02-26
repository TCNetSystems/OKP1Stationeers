using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKP1_Stationeers_Editor
{
    public class ItemRecipe
    {
        // Quiet VS...Quiet....
#pragma warning disable IDE1006

        public ItemRecipe()
        {
            ingredients = new Dictionary<string, string>();
        }

        public string item
        {
            get;
            set;
        }

        public string manufactory
        {
            get;
            set;
        }

        public IDictionary<string,string> ingredients
        {
            get;
            set;
        }
    }
}
