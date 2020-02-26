using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKP1_Stationeers_Editor.Stationeers
{
    class CarbonDioxide : Mole
    {
        public CarbonDioxide()
        {
            this.SpecificHeat = 28.2f;
            this.Type = Chemistry.GasType.CarbonDioxide;
        }

        public CarbonDioxide(float quantity) : base(quantity)
        {
            this.SpecificHeat = 28.2f;
            this.Type = Chemistry.GasType.CarbonDioxide;
        }

        public CarbonDioxide(Mole mole) : base(mole)
        {
            this.SpecificHeat = 28.2f;
            this.Type = Chemistry.GasType.CarbonDioxide;
        }
    }

    class Nitrogen : Mole
    {

        public Nitrogen()
        {
            this.SpecificHeat = 20.6f;
            this.Type = Chemistry.GasType.Nitrogen;
        }

        public Nitrogen(float quantity)
          : base(quantity)
        {
            this.SpecificHeat = 20.6f;
            this.Type = Chemistry.GasType.Nitrogen;
        }

        public Nitrogen(Mole mole)
          : base(mole)
        {
            this.SpecificHeat = 20.6f;
            this.Type = Chemistry.GasType.Nitrogen;
        }
    }

    class NitrousOxide : Mole
    {
        
        public NitrousOxide()
        {
            this.SpecificHeat = 23f;
            this.Type = Chemistry.GasType.NitrousOxide;
        }

        public NitrousOxide(float quantity)
          : base(quantity)
        {
            this.SpecificHeat = 23f;
            this.Type = Chemistry.GasType.NitrousOxide;
        }

        public NitrousOxide(Mole mole)
          : base(mole)
        {
            this.SpecificHeat = 23f;
            this.Type = Chemistry.GasType.NitrousOxide;
        }
    }

    class Oxygen : Mole
    {
        
        public Oxygen()
        {
            this.SpecificHeat = 21.1f;
            this.Type = Chemistry.GasType.Oxygen;
        }

        public Oxygen(float quantity)
          : base(quantity)
        {
            this.SpecificHeat = 21.1f;
            this.Type = Chemistry.GasType.Oxygen;
        }

        public Oxygen(Mole mole)
          : base(mole)
        {
            this.SpecificHeat = 21.1f;
            this.Type = Chemistry.GasType.Oxygen;
        }
    }

    class Chlorine : Mole
    {

        public Chlorine()
        {
            this.SpecificHeat = 24.8f;
            this.Type = Chemistry.GasType.Pollutant;
        }

        public Chlorine(float quantity)
          : base(quantity)
        {
            this.SpecificHeat = 24.8f;
            this.Type = Chemistry.GasType.Pollutant;
        }

        public Chlorine(Mole mole)
          : base(mole)
        {
            this.SpecificHeat = 24.8f;
            this.Type = Chemistry.GasType.Pollutant;
        }
    }

    class Volatiles : Mole
    {
        
        public Volatiles()
        {
            this.SpecificHeat = 20.4f;
            this.Type = Chemistry.GasType.Volatiles;
        }

        public Volatiles(float quantity)
          : base(quantity)
        {
            this.SpecificHeat = 20.4f;
            this.Type = Chemistry.GasType.Volatiles;
        }

        public Volatiles(Mole mole)
          : base(mole)
        {
            this.SpecificHeat = 20.4f;
            this.Type = Chemistry.GasType.Volatiles;
        }
    }

    class Water : Mole
    {

        public Water()
        {
            this.SpecificHeat = 72f;
            this.Type = Chemistry.GasType.Water;
        }

        public Water(float quantity)
          : base(quantity)
        {
            this.SpecificHeat = 72f;
            this.Type = Chemistry.GasType.Water;
        }

        public Water(Mole mole)
          : base(mole)
        {
            this.SpecificHeat = 72f;
            this.Type = Chemistry.GasType.Water;
        }
    }
}
