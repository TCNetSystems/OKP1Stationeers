using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKP1_Stationeers_Editor.Stationeers
{
    public class Mole
    {
        public float SpecificHeat = 0f;
        public Chemistry.GasType Type = Chemistry.GasType.Undefined;

        private float _quantity = 0f;
        public float Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (float.IsNaN(value))
                    _quantity = 0f;
                _quantity = value;
            }
        }

        // Needs a NaN protection...
        private float _energy = 0f;
        public float Energy
        {
            get
            {
                if (float.IsNaN(_energy))
                    return 0f;
                return _energy;
            }
                
            set
            {
                if (float.IsNaN(value))
                    _energy = 0f;
                _energy = value;
            }
        }

        public float HeatCapacity
        {
            get
            {
                return this.SpecificHeat * this.Quantity;
            }
        }

        public float Temperature
        {
            get
            {

                return this.Energy / this.HeatCapacity;
            }
        }
        public Mole()
        {

        }

        public Mole(float quantity)
        {
            this.Quantity = quantity;
            this.Energy = Chemistry.Temperature.TwentyDegrees * this.HeatCapacity;
        }

        public Mole(Mole newMole)
        {
            this.Set(newMole);
        }

        public void Set (Mole newMole)
        {
            if (newMole == null)
                return;
            this.Set(newMole.Quantity, newMole.Energy);
        }

        public void Set(float quantity)
        {
            this.Set(quantity, 0.0f);
        }

        public void Set(float quantity, float energy)
        {
            this.Quantity = quantity;
            this.Energy = energy;
        }

        public void Add(float quantity, float energy)
        {
            this.Quantity += quantity;
            this.Energy += energy;
        }

        public void Add(float quantity)
        {
            float energy = quantity * this.SpecificHeat * Chemistry.Temperature.TwentyDegrees;
            this.Add(quantity, energy);
        }

    }
}
