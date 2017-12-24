using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OKP1_Stationeers_Editor
{
    class ThingLockerItem : ThingManager
    {

        public int ParentSlotId
        {
            get
            {
                if(XML == null)
                {
                    return -1;
                }
                if(XML.Element("ParentSlotId") != null)
                {
                    return Int32.Parse(XML.Element("ParentSlotId").Value);
                } else
                {
                    return -1;
                }
            }
            set
            {
                if(XML == null)
                {
                    return;
                }
                // try to update the element...
                if (XML.Element("ParentSlotId") != null)
                {
                    XML.Element("ParentSlotID").SetValue(value);
                }
            }
        }
        public Int64 ParentReferenceId
        {
            get
            {
                if (XML == null)
                {
                    return -1;
                }
                if (XML.Element("ParentReferenceId") != null)
                {
                    return Int64.Parse(XML.Element("ParentReferenceId").Value);
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                if (XML == null)
                {
                    return;
                }
                // try to update the element...
                if (XML.Element("ParentReferenceId") != null)
                {
                    XML.Element("ParentReferenceId").SetValue(value);
                }
            }
        }

        public ThingLockerItem() : base()
        {
            _typeOf = ThingType.LockerItem;

        }

        public ThingLockerItem(XElement thing) : base(thing)
        {
            _typeOf = ThingType.LockerItem;
            
            // reset name sensibly...

            string parentSlotId = thing.Element("ParentSlotId").Value;
            string prefabName = thing.Element("PrefabName").Value;
            _name = $"Slot: {parentSlotId} {Id.ToString()} {prefabName}";
        }
    }

}

