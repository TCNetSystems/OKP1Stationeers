# OKP1Stationeers
OverKillPlusOne Stationeers Editor

Refreshed/rebuilt to work with Stationeers v0.2.2299.10449 -- The XML type for Machines had changed, but structure was/is the same.

Now with TANKS!  Tank contents can be directly modified.  Why do I not just let you "set the pressure"?  
Because that is not how tanks work in the game or in the real world.  You've got some mols of substance, with a given energy.  
Quantity, Energy, and Container volume all work together to create temperature and pressure.

This change will need some fixup as right now the floats emit exponential format data....which I do not think is present in the
Assembly-CSharp generated data BUT the underlying data save stuff is .NET System.Xml ... The editor uses LINQ/XML which behaves almost the same
and the Serialization system does understand exponential format for floats, I'm just being lazy with a ToString() which defaults to a human friendly format which
is influenced by the culture (this is why this commit is marked WIP)...

Also very, very, very WIP in this branch isusing stationeering.com JSON data...I'm thinking about adding completion routines based on that data.  Right now it fetches it but doesn't do anything with it.

Locker inventory editing remains disabled and broken for the time being.

This is a bare bones inventory editor for Stationeers.  Current functionality finished is the ability to edit reagents content (steel, iron, solder, etc) contained in machines.  Not all machine types are discovered as of right now.

Take a look at [My YouTube Introduction](https://youtu.be/knInAS38phQ?t=24s)

Special thanks to @thej3rk for the app icon!
