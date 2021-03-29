using OKP1_Stationeers_Editor.Stationeers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OKP1_Stationeers_Editor
{
    public class ThingAtmosphere : ThingManager
    {
        private XElement _atmosphere;

        public Stationeers.GasMixture gasMixture;


        public ThingAtmosphere() : base()
        {
            _typeOf = ThingType.Tank;
            gasMixture = new Stationeers.GasMixture();
        }

        public ThingAtmosphere(XElement thing) : base(thing)
        {
            _typeOf = ThingType.Tank;
            gasMixture = new Stationeers.GasMixture();
            string prefabName = thing.Element("PrefabName").Value;
            string customName = thing.Element("CustomName").Value.Length > 0 ? thing.Element("CustomName").Value : "<unnamed>";
            _name = $"{Id} {prefabName} {customName}";

            // See about grabbing the atmo data....
            if(GlobData._atmosphereDataByThing.ContainsKey(Id))
            {
                _atmosphere = GlobData._atmosphereDataByThing[Id];
            }

            // Get the volume or bitch?

            // A newly created tank, never connected to anything, will not have an Atmosphere ref...SOOOOO create one...

            if(_atmosphere == null)
            {
                throw new Exception($"Broken Save Data, no AtmosphereSaveData found with ThingReferenceId {Id}");
#if GENERATE_NEW_NODES
                XElement rootNode = _xmlThing;
                while(true)
                {
                    if(rootNode.Parent != null)
                    {
                        rootNode = rootNode.Parent;
                        continue;
                    }
                    break;
                }

                // rootNode is the WorldData XElement now...

                XElement newXML = XElement.Parse(Properties.Resources.AtmosphereSaveDataTemplate);

                // Initializing the Position data does not appear to be necessary
                // If it turns out in testing it is though, we will need to check for WorldPosition (structure tanks) or Position (dynamics)
                newXML.Element("ThingReferenceId").SetValue(Id);

                // Figure out the appropriate default Volume (I guess Volume should be on the edit table too)
                // Check each prefix...
                foreach(KeyValuePair<string,float> kvp in GlobData.GasContainerDefaultVolumes)
                {
                    if(prefabName.StartsWith(kvp.Key))
                    {
                        newXML.Element("Volume").SetValue(kvp.Value);
                        break;
                    }
                }

                // attach to correct place (which damn well better exist...)
                rootNode.Element("Atmospheres").Add(newXML);
                _atmosphere = newXML;
                GlobData._atmosphereDataByThing[Id] = _atmosphere;
#endif
            }
            
            
            

            float newVolume = 64f; // to avoid div by 0 just init as if it is a can
            if(_atmosphere.Element("Volume") != null)
            {
                if(float.TryParse(_atmosphere.Element("Volume").Value, out float tmpfloat))
                {
                    newVolume = tmpfloat;
                }
            }
            gasMixture.Volume = newVolume;

            foreach (KeyValuePair<string, float> gasData in GlobData.GasSpecificHeat)
            {
                if(_atmosphere.Element(gasData.Key) != null)
                {
                    if (float.TryParse(_atmosphere.Element(gasData.Key).Value, out float tmpfloat))
                    {
                        gasMixture.gases[gasData.Key].Set(tmpfloat);
                    }
                }
            }




            float totalEnergy = 0f;
            if(_atmosphere.Element("Energy") != null)
            {
                if(float.TryParse(_atmosphere.Element("Energy").Value, out float tmpfloat))
                {
                    totalEnergy = tmpfloat;
                }
            }
            gasMixture.Energy = totalEnergy;

        }

        public override void Save()
        {
            // Pull total energy, and gases, writing them to the XML...
            // _atmosphere.Element("Energy").Value = gasMixture.Energy.ToString();
            _atmosphere.Element("Energy").Value = string.Format("{0:R}", gasMixture.Energy);
            foreach(KeyValuePair<string, Mole> gas in gasMixture.gases)
            {
                // _atmosphere.Element(gas.Key).Value = gas.Value.Quantity.ToString();
                _atmosphere.Element(gas.Key).Value = string.Format("{0:R}", gas.Value.Quantity);
            }

            // should attach atmo here rather than during init...
        }
    }
}
