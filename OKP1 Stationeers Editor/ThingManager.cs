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
            get;
        }  = 0;
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
            
            Int64 refid = 0;
            if(Int64.TryParse(thing.Element("ReferenceId").Value, out refid))
            {
                Id = refid;
            }
            name = refid.ToString();
            if (cn.Length > 0)
            {
                name += " " + cn;
            }
            else
            {
                name += " <unnamed>";
            }
            _name = name;
            _xmlThing = thing;
        }

        public ThingManager(XElement thing, ThingType typeOf) : this(thing) => _typeOf = typeOf;

        public override string ToString() => Name;

    }
}
