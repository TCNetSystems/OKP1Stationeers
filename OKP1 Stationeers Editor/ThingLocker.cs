using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OKP1_Stationeers_Editor
{
    class ThingLocker : ThingManager
    {
        public bool DataLoadedToNode
        {
            get;
            set;
        } = false;
        public ThingLocker() : base()
        {
            _typeOf = ThingType.Locker;
        }

        public ThingLocker(XElement thing) : base(thing)
        {
            _typeOf = ThingType.Locker;
        }
    }
}
