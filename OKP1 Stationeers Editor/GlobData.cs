using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OKP1_Stationeers_Editor
{
    static class GlobData
    {
        public static Dictionary<Int64, XElement> _atmosphereDataByThing = new Dictionary<Int64, XElement>();
        public static RecipeDataFile _recipesDataFile = null;
        // Hint the Dict to build to 490 items...That's the max estimated size right now (actually ends up ~310)
        public static Dictionary<string, List<ItemRecipe>> Recipes = new Dictionary<string, List<ItemRecipe>>(490);
        public static Dictionary<string, float> GasContainerDefaultVolumes = new Dictionary<string, float>() {
            { "DynamicGasCanister", 790f },
            { "StructureTankSmall", 6000f },
            { "StructureTankBig", 50000f },
            { "ItemGasCanister", 64f }
        };

        // This is set for the XML element names!
        public static Dictionary<string, float> GasSpecificHeat = new Dictionary<string, float>()
        {
            {"CarbonDioxide", 28.2f },
            {"Nitrogen", 20.6f },
            {"NitrousOxide", 23f },
            {"Oxygen", 21.1f },
            {"Chlorine", 24.8f }, // Pollutant
            {"Volatiles", 20.4f }, // "H2"
            {"Water", 72f }
        };

        public static float ComputeKpa(float moles, float temperatureK, float volume)
        {
            
            return ((moles * 8.3144f * temperatureK) / volume);
        }
    }

        
}
