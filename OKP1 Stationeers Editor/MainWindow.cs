using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace OKP1_Stationeers_Editor
{
    public partial class MainWindow : Form
    {
        private FileStream WorldStream = null;
        private XDocument World = null;
        private bool WorldIsModified = false;
        public UInt64 MaxThingRefId
        {
            get
            {
                return _maxThingRefId;
            }
            set
            {
                _maxThingRefId = value;
                // update tooltip...
                toolStripStatusLabelMaxRef.Text = $"Max Ref: {value}";
            }
        }
        private static UInt64 _maxThingRefId = 0;


        public MainWindow()
        {
            InitializeComponent();
        }
        private void ToolStripMenuFile_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuFileExit_Click(object sender, EventArgs e)
        {
            // XXX TODO XXX
            // check if open+unsaved and prompt for exit (unless disabled...?)
            if (WorldStream != null)
            {
                if (WorldIsModified)
                {
                    // bitch
                }
                WorldStream.Close();
            }

            Application.Exit();
        }

        private void ToolStripMenuFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "World File|world.xml";
            openFileDialog.Title = "Select Stationeers World File";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // we update the cursor just because we can...loading and parsing really shouldn't take much time...
                Cursor.Current = Cursors.WaitCursor;
                toolStripStatusOpenedFileLabel.Text = openFileDialog.FileName;
                WorldStream = File.Open(toolStripStatusOpenedFileLabel.Text, FileMode.Open, FileAccess.ReadWrite);
                World = XDocument.Load(WorldStream);
                toolStripMenuFileClose.Enabled = true;
                toolStripMenuFileOpen.Enabled = false;
                World.Changed += new EventHandler<XObjectChangeEventArgs>(
                    (csender, cea) =>
                    {
                        WorldIsModified = true;
                        toolStripMenuFileSave.Enabled = true;
                    }
                );
                CompleteLoading();
                treeViewNavLeft.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void CompleteLoading()
        {
            // find and populate lockers
            //and ln.Element("PrefabName") = "StructureStorageLocker"
            treeViewNavLeft.BeginUpdate();
            doTreeCleanup();
            // Run through and find the highest reference ID, lockers..etc...
            TreeNode treeLocker = treeViewNavLeft.Nodes["Lockers"];
            TreeNode treeMachines = treeViewNavLeft.Nodes["Machines"];
            var things = from ln in World.Descendants("ThingSaveData")
                         select ln;
            foreach (XElement ln in things)
            {
                if(ln.Element("ReferenceId") != null)
                {
                    UInt64 newVal = UInt64.Parse(ln.Element("ReferenceId").Value);
                    if( newVal > MaxThingRefId)
                    {
                        MaxThingRefId = newVal;
                    }
                    
                }

                // found a locker ?
                string thingType = (string)ln.Attributes().FirstOrDefault(a => a.Name.LocalName == "type");
                if (thingType == "StructureSaveData" && ln.Element("PrefabName").Value == "StructureStorageLocker")
                {
                    ThingLocker thing = new ThingLocker(ln);
                    TreeNode thisnode = treeLocker.Nodes.Add(thing.Id.ToString(), thing.Name);
                    thisnode.Tag = thing;
                    continue;
                }

                // found a player ?
                if (thingType == "HumanSaveData")
                {
                    ThingPlayer thing = new ThingPlayer(ln);
                    TreeNode thisnode = treeViewNavLeft.Nodes["Players"].Nodes.Add(thing.Id.ToString(), thing.Name);
                    thisnode.Tag = thing;
                    continue;
                }


                // found a machine.....maybe?  do not continue from this block as it is indeterminate...
                if (thingType == "StructureSaveData" || thingType == "FabricatorSaveData")
                {
                    XElement prefabName = ln.Element("PrefabName");
                    if (prefabName != null && Properties.Settings.Default.MachinePrefabs.Contains(prefabName.Value))
                    {
                        ThingMachine thing = new ThingMachine(ln);
                        TreeNode thisNode = treeMachines.Nodes.Add(thing.Id.ToString(), thing.Name);
                        thisNode.Tag = thing;
                    }
                }

            }
            treeViewNavLeft.EndUpdate();


        }

        private void doTreeCleanup()
        {
            treeViewNavLeft.BeginUpdate();
            foreach(TreeNode t in treeViewNavLeft.Nodes)
            {
                t.Nodes.Clear();
                t.Collapse();
            }
            treeViewNavLeft.EndUpdate();
        }

        private void doCloseCleanup()
        {
            // close open tabs....I'm really not clear if this Dispose()s of things...
            rightEditTab.TabPages.Clear();

            doTreeCleanup();

            World = null;
            WorldStream.Close();
            WorldStream = null;
            toolStripMenuFileClose.Enabled = false;
            toolStripMenuFileOpen.Enabled = true;
            toolStripMenuFileSave.Enabled = false;
            treeViewNavLeft.Enabled = false;
            MaxThingRefId = 0;

        }
        private void ToolStripMenuClose_Click(object sender, EventArgs e)
        {
            toolStripStatusOpenedFileLabel.Text = "No file...";
            doCloseCleanup();
        }


        private void treeViewNavLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView tree = (TreeView)sender;
            TreeNode sel = tree.SelectedNode;
            // determine which control to load based on the path...
            if (sel.Tag != null)
            {
                ThingManager thing = (ThingManager)sel.Tag;
                switch (thing.TypeOf)
                {
                    case ThingManager.ThingType.Locker:
                        // Console.WriteLine("selected locker {0} [{1}]", thing.ToString(), sel.FullPath);
                        // probably reimplement this similar to the Machines+Reagents...
                        ThingLocker thingLocker = (ThingLocker)thing;
                        if (!thingLocker.DataLoadedToNode)
                        {
                            Console.WriteLine($"Locker {thingLocker.Id} not loaded, loading now");
                            LockerTreeViewAddChildNodes(sel.Nodes, thingLocker);
                        }
                        break;

                    case ThingManager.ThingType.Player:
                        Console.WriteLine("Selected a player....");
                        break;

                    case ThingManager.ThingType.LockerItem:

                        /*
                        // if we've got it, pull the XML to the temp view....
                        if(thing.XML != null)
                        {
                            textBox1.Text = thing.XML.ToString();
                        }
                        else
                        {
                            textBox1.Text = "";
                        }
                        */
                        break;

                    case ThingManager.ThingType.Machine:
                        ThingMachine thingMachine = (ThingMachine)thing;
                        TabPage machineReagentsPage;
                        // see if it's already open...
                        string tabKey = $"{thingMachine.Id} Reagents";
                        if(!rightEditTab.TabPages.ContainsKey(tabKey))
                        {
                            machineReagentsPage = new TabPage(tabKey);
                            machineReagentsPage.Name = tabKey;
                            machineReagentsPage.Controls.Add(new ReagentEdit(thingMachine));
                            rightEditTab.TabPages.Add(machineReagentsPage);
                            rightEditTab.SelectTab(machineReagentsPage);
                        }
                        else
                        {
                            rightEditTab.SelectTab(tabKey);
                        }
                        break;

                    default:
                        Console.WriteLine("Shit! Unknown Thing Type in treeViewNavLeft_AfterSelect [{0}]", thing.TypeOf);
                        break;
                }
            }


        }
        private void LockerTreeViewAddChildNodes(TreeNodeCollection nodes, ThingLocker thing)
        {
            LockerTreeViewAddChildNodes(nodes, thing.XML);
            thing.DataLoadedToNode = true;
        }

        private void LockerTreeViewAddChildNodes(TreeNodeCollection nodes, XElement XML)
        {
            // http://csharphelper.com/blog/2014/09/load-a-treeview-control-from-an-xml-file-in-c/
            // seek up into the parent
            // find everything which mentions us as a parent
            string refId = XML.Element("ReferenceId").Value;
            var items = from i in XML.Parent.Elements("ThingSaveData")
                        where i.Element("ParentReferenceId") != null && i.Element("ParentReferenceId").Value == refId
                        orderby Int32.Parse(i.Element("ParentSlotId").Value)
                        select i;
            Dictionary<int, ThingLockerItem> lockerItems = new Dictionary<int, ThingLockerItem>();

            // lockers have 30 slots at present...
            // populate with existing items in the locker...
            foreach (XElement i in items)
            {
                ThingLockerItem thing = new ThingLockerItem(i);
                // this will throw an exception if there's a duplicate...
                // there really shouldn't be and if there is the game has a bug
                // and I'm not about to try to recover that, so we'll let the app crash.
                lockerItems.Add(thing.ParentSlotId, thing);
            }

            // now generate the tree node with the missing slots...

            int loop = 0;
            uint loopStop = Properties.Settings.Default.LockerSize;

            while (loop <= loopStop)
            {
                ThingLockerItem thing = null;
                TreeNode treeNode = null;
                try
                {
                    thing = lockerItems[loop];
                    treeNode = nodes.Add(thing.Id.ToString(), thing.Name);
                }
                catch (KeyNotFoundException)
                {
                    // later we catch the null XML to create a new node and potentially save it...
                    thing = new ThingLockerItem();
                    treeNode = nodes.Add(loop.ToString(), $"Slot: {loop} <EMPTY>");
                }
                treeNode.Tag = thing;
                treeNode.EnsureVisible();
                loop++;
            }
        }

        private void toolStripMenuFileSave_Click(object sender, EventArgs e)
        {

            // rewind/truncate stream...
            WorldStream.SetLength(0);

            World.Save(WorldStream);

            // lastly disable us and set unmodified...

            toolStripMenuFileSave.Enabled = false;
            WorldIsModified = false;
        }
    }
}
