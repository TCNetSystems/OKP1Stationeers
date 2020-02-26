using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OKP1_Stationeers_Editor
{
    public class ThingLockerItem : ThingManager
    {

        private bool _storedToParentTree = false;
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
                    XML.Element("ParentSlotId").SetValue(value);
                }
            }
        }
        public int Quantity
        {
            get
            {
                if (XML == null)
                {
                    return -1;
                }
                if (XML.Element("Quantity") != null)
                {
                    return Int32.Parse(XML.Element("Quantity").Value);
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
                if (XML.Element("Quantity") != null)
                {
                    XML.Element("Quantity").SetValue(value);
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
        public string PrefabName
        {
            get
            {
                if (XML == null)
                {
                    return null;
                }
                if (XML.Element("PrefabName") != null)
                {
                    return XML.Element("PrefabName").Value;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (XML == null)
                {
                    return;
                }
                // try to update the element...
                if (XML.Element("PrefabName") != null)
                {
                    XML.Element("PrefabName").SetValue(value);
                }
            }
        }

        public bool StoredToParentTree { get => _storedToParentTree; set => _storedToParentTree = value; }

        public ThingLockerItem() : base()
        {
            _typeOf = ThingType.LockerItem;

        }

        public ThingLockerItem(XElement thing) : base(thing)
        {
            _typeOf = ThingType.LockerItem;
            
            // reset name sensibly...
            
            _name = _genName();
        }
        private string _genName()
        {
            
            return $"Slot: {ParentSlotId} {Id.ToString()} {PrefabName} {TypeData}";
        }
        public bool GenerateNewLockerItem(ThingLocker thingLocker, int parentSlotId, Int64 newItemId)
        {
            XElement parentLockerXML = thingLocker.XML;
            XElement newXML = XElement.Parse(Properties.Resources.LockerItemTemplate);
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            _xmlThing = newXML;
            // add empty type attribute...
            newXML.Add(new XAttribute(xsi + "type",""));
            // set our ref
            ReferenceId = newItemId;
            // set parent ref
            ParentReferenceId = thingLocker.Id;
            // set parent slot
            ParentSlotId = parentSlotId;

            PrefabName = "StructureTypeUninit";

            // Do not add to parent until saved....
            // parentLockerXML.Add(newXML);
            _name = _genName();
            

            return true;

        }

        public override void Save()
        {
            base.Save();
            if(!_storedToParentTree && _parentXML != null)
            {
                _parentXML.Add(_xmlThing);
                _storedToParentTree = true;
            }
        }
    }

}

