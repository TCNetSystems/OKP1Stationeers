using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKP1_Stationeers_Editor.Stationeers
{
    public class GasMixture
    {
        public Dictionary<string, Mole> gases = new Dictionary<string, Mole>();

        public float Volume
        {
            get;
            set;
        }

        public float HeatCapacity
        {
            get
            {
                float _totalHeatCapacity = 0f;
                foreach(KeyValuePair<string, Mole> gas in gases)
                {
                    _totalHeatCapacity += gas.Value.HeatCapacity;
                }
                return _totalHeatCapacity;     
            }
        }

        // Needs to deal with NaN protection...?
        public float Energy
        {
            get
            {
                float _totalEnergy = 0f;
                foreach(KeyValuePair<string, Mole> gas in gases)
                {
                    if(!float.IsNaN(gas.Value.Energy))
                        _totalEnergy += gas.Value.Energy;
                    
                }
                return _totalEnergy;
            }
            set
            {
                foreach(KeyValuePair<string, Mole> gas in gases)
                {
                    gas.Value.Energy = value * (gas.Value.HeatCapacity / HeatCapacity);
                }
            }
        }

        public float TotalMoles
        {
            get
            {
                float _totalMoles = 0f;
                foreach (KeyValuePair<string, Mole> gas in gases)
                {
                    _totalMoles += gas.Value.Quantity;
                    
                }
                return _totalMoles;
            }
        }

        public float Temperature
        {
            get
            {
                return Energy / HeatCapacity;
            }
        }

        public float TemperatureC
        {
            get
            {
                return Temperature - 273.15f;
            }
        }


        public float Pressure
        {
            get
            {
                return TotalMoles * 8.3144f * Temperature / Volume;
            }
        }



        public Mole Oxygen
        {
            get
            {
                if (gases.ContainsKey("Oxygen"))
                    return gases["Oxygen"];
                return null;
            }
        }

        public Mole CarbonDioxide
        {
            get
            {
                if (gases.ContainsKey("CarbonDioxide"))
                    return gases["CarbonDioxide"];
                return null;
            }
        }

        public Mole Nitrogen
        {
            get
            {
                if (gases.ContainsKey("Nitrogen"))
                    return gases["Nitrogen"];
                return null;
            }
        }

        public Mole NitrousOxide
        {
            get
            {
                if (gases.ContainsKey("NitrousOxide"))
                    return gases["NitrousOxide"];
                return null;
            }
        }

        public Mole Chlorine
        {
            get
            {
                if (gases.ContainsKey("Chlorine"))
                    return gases["Chlorine"];
                return null;
            }
        }

        public Mole Pollutants
        {
            get
            {
                if (gases.ContainsKey("Chlorine"))
                    return gases["Chlorine"];
                return null;
            }
        }

        public Mole Volatiles
        {
            get
            {
                if (gases.ContainsKey("Volatiles"))
                    return gases["Volatiles"];
                return null;
            }
        }

        public Mole Water
        {
            get
            {
                if (gases.ContainsKey("Water"))
                    return gases["Water"];
                return null;
            }
        }

        public GasMixture()
        {
            InitGases();
        }

        public GasMixture(float volume)
        {
            Volume = volume;
            InitGases();
        }

        public GasMixture(float volume, float co2, float n2, float nitrous, float oxygen, float chlorine, float volatiles, float water )
        {
            Volume = volume;
            InitGases();
            this.CarbonDioxide.Quantity = co2;
            this.Nitrogen.Quantity = n2;
            this.NitrousOxide.Quantity = nitrous;
            this.Oxygen.Quantity = oxygen;
            this.Chlorine.Quantity = chlorine;
            this.Volatiles.Quantity = volatiles;
            this.Water.Quantity = water;

        }

        private void InitGases()
        {
            gases.Add("CarbonDioxide", new CarbonDioxide());
            gases.Add("Nitrogen", new Nitrogen());
            gases.Add("NitrousOxide", new NitrousOxide());
            gases.Add("Oxygen", new Oxygen());
            gases.Add("Chlorine", new Chlorine());
            gases.Add("Volatiles", new Volatiles());
            gases.Add("Water", new Water());

        }
    }
}
