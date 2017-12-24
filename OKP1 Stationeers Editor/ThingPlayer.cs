using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OKP1_Stationeers_Editor
{
    class ThingPlayer : ThingManager
    {
        public ThingPlayer() : base()
        {
            _typeOf = ThingType.Player;
        }

        public ThingPlayer(XElement thing) : base(thing)
        {
            _typeOf = ThingType.Player;
        }
    }
}
