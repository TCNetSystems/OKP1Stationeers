using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OKP1_Stationeers_Editor
{
    public class ThingManager
    {
        public enum ThingType { NotInit, Player, Locker, LockerItem, Machine, Reagent, Tank }

        public virtual string Name => _name;
        public virtual string GivenName => _givenName;

        protected XElement _parentXML = null;

        public XElement ParentXML
        {
            get
            {
                return _parentXML;
            }
        }

        public string TypeData
        {
            get
            {
                if ( XML == null)
                {
                    return null;
                }
                if (XML.Attributes().FirstOrDefault(a => a.Name.LocalName == "type") != null)
                {
                    return XML.Attributes().FirstOrDefault(a => a.Name.LocalName == "type").Value;
                }
                return null;
            }
            set
            {
                if ( XML == null)
                {
                    return;
                }
                if (XML.Attributes().FirstOrDefault(a => a.Name.LocalName == "type") != null)
                {
                    XML.Attributes().FirstOrDefault(a => a.Name.LocalName == "type").SetValue(value);
                }
            }
        }
        protected string _name = null;
        protected string _givenName = null;

        public Int64 Id
        {
            get
            {
                return ReferenceId;
            }

        }
        public Int64 ReferenceId
        {
            get
            {
                if (XML == null)
                {
                    return -1;
                }
                if (XML.Element("ReferenceId") != null)
                {
                    return Int64.Parse(XML.Element("ReferenceId").Value);
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
                if (XML.Element("ReferenceId") != null)
                {
                    XML.Element("ReferenceId").SetValue(value);
                }
            }
        }
        protected XElement _xmlThing = null;
        public ThingType TypeOf
        {
            get
            {
                return _typeOf;
            }
        }
        public XElement XML => _xmlThing;
        protected ThingType _typeOf = ThingType.NotInit;
        public ThingManager()
        {

        }

        protected void _setBaseValues(XElement thing)
        {
            string name = null;
            string cn = thing.Element("CustomName").Value;
            // do this early so the get/setters work
            _xmlThing = thing;

            name = ReferenceId.ToString();
            if (cn.Length > 0)
            {
                name += " " + cn;
                _givenName = cn;
            }
            else
            {
                name += " <unnamed>";
                _givenName = "<unnamed>";
            }
            _name = name;
        }

         public ThingManager(XElement thing)
        {
            _setBaseValues(thing);
            
        }

        public ThingManager(XElement thing, XElement thingParent) : this(thing) => _parentXML = thingParent;

        public ThingManager(XElement thing, ThingType typeOf) : this(thing) => _typeOf = typeOf;

        public ThingManager(XElement thing, ThingType typeOf, XElement thingParent) : this(thing, typeOf) => _parentXML = thingParent;

        public override string ToString() => Name;

        public virtual void Save()
        {
            return;
        }

    }
}
