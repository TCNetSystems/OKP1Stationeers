using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OKP1_Stationeers_Editor
{
    public class ThingMachine : ThingManager
    {
        public ThingMachine() : base()
        {
            _typeOf = ThingType.Machine;
        }

        public ThingMachine(XElement thing) : base(thing)
        {
            _typeOf = ThingType.Machine;
            string prefabName = thing.Element("PrefabName").Value;
            string customName = thing.Element("CustomName").Value.Length > 0 ? thing.Element("CustomName").Value : "<unnamed>";
            _name = $"{Id} {prefabName} {customName}";
        }

        public List<ThingReagent> GetReagents()
        {
            List<ThingReagent> outList = new List<ThingReagent>();

            var reagents = from i in XML.Element("Reagents").Elements("Reagent")
                            select i;
            foreach ( XElement i in reagents)
            {
                ThingReagent thingReagent = new ThingReagent(i);
                outList.Add(thingReagent);
            }

            return outList;
        }
        public List<ThingReagent> Reagents
        {
            get
            {
                return GetReagents();
            }
        }
    }
}
