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
        public enum ThingType { NotInit, Player, Locker, Solar, LockerItem, Machine, Reagent }
        public virtual string Name => _name;
        protected string _name = null;
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

         public ThingManager(XElement thing)
        {
            string name = null;
            string cn = thing.Element("CustomName").Value;
            // do this early so the get/setters work
            _xmlThing = thing;

            name = ReferenceId.ToString();
            if (cn.Length > 0)
            {
                name += " " + cn;
            }
            else
            {
                name += " <unnamed>";
            }
            _name = name;
            
        }

        public ThingManager(XElement thing, ThingType typeOf) : this(thing) => _typeOf = typeOf;

        public override string ToString() => Name;

    }
}
