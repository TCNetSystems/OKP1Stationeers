using System;
using System.Collections.Generic;
// using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
// using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace OKP1_Stationeers_Editor
{
     public partial class MainWindow : Form
    {
        private FileStream WorldStream = null;
        private XDocument World = null;
        private bool WorldIsModified = false;
        private string _recipeDataFile = null;
        
        public static readonly HttpClient httpClient = new HttpClient();


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
                if(value != 0)
                {
                    toolStripStatusLabelMaxRef.Text = $"Max Ref: {value}";
                } else
                {
                    toolStripStatusLabelMaxRef.Text = "";
                }
                
            }
        }

        protected string RecipeDataETag {
            get => Properties.Settings.Default.RecipeDataETag;

            set {

                Properties.Settings.Default.RecipeDataETag = value;
            }

        }

        protected DateTime RecipeDataRefreshedAt {
            get => Properties.Settings.Default.RecipeDataRefreshedAt;

            set
            {
                Properties.Settings.Default.RecipeDataRefreshedAt = value;
            }

        }

        private static UInt64 _maxThingRefId = 0;


        public MainWindow()
        {
            InitializeComponent();
            string[] pathData = { Application.UserAppDataPath, "recipes-public.json" };

            _recipeDataFile = Path.Combine(pathData);
        }

        private void ToolStripMenuFileExit_Click(object sender, EventArgs e)
        {
            // XXX TODO XXX
            // check if open+unsaved and prompt for exit (unless disabled...?)
            if (WorldStream != null)
            {
                if (WorldIsModified)
                {
                    if(MessageBox.Show("Quit without saving?", "Quit without saving world?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        WorldStream.Close();
                        Application.Exit();
                    } else
                    {
                        return;
                    }
                }

                WorldStream.Close();
            }

            Application.Exit();
        }

        private async void ToolStripMenuFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "World File|world.xml|All XML (*.xml)|*.xml";
            openFileDialog.Title = "Select Stationeers World File";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // we update the cursor just because we can...loading and parsing really shouldn't take much time...
                Cursor.Current = Cursors.WaitCursor;
                toolStripStatusOpenedFileLabel.Text = openFileDialog.FileName;
                WorldStream = File.Open(toolStripStatusOpenedFileLabel.Text, FileMode.Open, FileAccess.ReadWrite);
                World = XDocument.Load(WorldStream, LoadOptions.PreserveWhitespace);
                toolStripMenuFileClose.Enabled = true;
                toolStripMenuFileOpen.Enabled = false;
                World.Changed += new EventHandler<XObjectChangeEventArgs>(
                    (csender, cea) =>
                    {
                        WorldIsModified = true;
                        toolStripMenuFileSave.Enabled = true;
                    }
                );

                // See if we've got recipe data file and fetch it if not...
                if(!File.Exists(_recipeDataFile))
                {
                    HttpResponseMessage response;
                    response = await FetchRecipes();
                    DoRecipeDataCompletion(response);
                }

                if(GlobData._recipesDataFile == null)
                {
                    DoAutoCompleterLoad();
                }

                CompleteLoading();
                treeViewNavLeft.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void CompleteLoading()
        {

            // we defer updating the status bar until the end
            // I'm not *certain* it's spamming a thousand odd updates but this will keep that from happening

            UInt64 newMaxRefVal = 0;

            // Take a peek at the version information...
            XElement gameVersion = World.Element("WorldData").Element("GameVersion");
            toolStripStatusLabelGameVersion.Text = $"Game: {gameVersion.Value}";

            treeViewNavLeft.BeginUpdate();
            DoTreeCleanup();

            // Populate atosphere data...
            
            GlobData._atmosphereDataByThing.Clear();

            var atmospheres = from ln in World.Element("WorldData").Element("Atmospheres").Elements()
                              select ln;
            foreach (XElement ln in atmospheres)
            {
                // Check if ThingReferenceId > 0...
                Int64 thingRefId = Int64.Parse(ln.Element("ThingReferenceId").Value);
                if(thingRefId > 0)
                {
                    GlobData._atmosphereDataByThing[thingRefId] = ln;
                }
            }
            atmospheres = null;

            // Run through and find the highest reference ID, lockers..etc...
            // Precompile Tank regex...
            Regex tankRegex = new Regex(@"^((DynamicGasCanister)|(ItemGasCanister)|(StructureTank(Big|Small)))",RegexOptions.Compiled | RegexOptions.CultureInvariant);

            var things = from ln in World.Element("WorldData").Element("Things").Elements()
                         select ln;
            foreach (XElement ln in things)
            {
                if(ln.Element("ReferenceId") != null)
                {
                    UInt64 newVal = UInt64.Parse(ln.Element("ReferenceId").Value);
                    if( newVal > newMaxRefVal)
                    {
                        newMaxRefVal = newVal;
                    }
                    
                }

                // found a locker ?
                string thingType = (string)ln.Attributes().FirstOrDefault(a => a.Name.LocalName == "type");
                XElement prefabName = ln.Element("PrefabName");
                


                switch (thingType)
                {
                    case "StructureSaveData":
                        if (prefabName.Value == "StructureStorageLocker")
                        {
                            ThingLocker thing = new ThingLocker(ln);
                            TreeNode thisNode = treeViewNavLeft.Nodes["Lockers"].Nodes.Add(thing.Id.ToString(), thing.Name);
                            thisNode.Tag = thing;
                            LockerTreeViewAddChildNodes(thisNode.Nodes, thing);
                            continue;
                        }
                        break;

                    case "HumanSaveData":
                        {
                            ThingPlayer thing = new ThingPlayer(ln);
                            TreeNode thisnode = treeViewNavLeft.Nodes["Players"].Nodes.Add(thing.Id.ToString(), thing.Name);
                            thisnode.Tag = thing;
                        }
                        break;

                    case "SimpleFabricatorSaveData":
                        if (prefabName != null && Properties.Settings.Default.MachinePrefabs.Contains(prefabName.Value))
                        {
                            ThingMachine thing = new ThingMachine(ln);
                            TreeNode thisNode = treeViewNavLeft.Nodes["Machines"].Nodes.Add(thing.Id.ToString(), thing.Name);
                            thisNode.Tag = thing;

                        }
                        break;

                    case "DynamicThingSaveData": // That type will get us into trouble with this switch later...  a goto case will help
                    case "DynamicGasCanisterSaveData":
                    case "DeviceAtmosphericSaveData":
                        // Fixed tanks?
                        Match fixedTank = tankRegex.Match(prefabName.Value);
                        if (fixedTank.Success)
                        {
                            // Tanks find atmo data on their own...
                            ThingAtmosphere thing = new ThingAtmosphere(ln);
                            TreeNode thisNode = treeViewNavLeft.Nodes["Tanks"].Nodes.Add(thing.Id.ToString(), thing.Name);
                            thisNode.Tag = thing;
                        }

                        break;


                    default:

                        break;
                }
            }
            
            things = null;

            treeViewNavLeft.EndUpdate();
            MaxThingRefId = newMaxRefVal;


        }

        private void DoTreeCleanup()
        {
            treeViewNavLeft.BeginUpdate();
            treeViewNavLeft.CollapseAll();
            foreach(TreeNode t in treeViewNavLeft.Nodes)
            {
                t.Nodes.Clear();
            }
            treeViewNavLeft.EndUpdate();
        }

        private void DoCloseCleanup()
        {
            toolStripStatusOpenedFileLabel.Text = "No file...";
            toolStripStatusLabelGameVersion.Text = "";
            // close open tabs....I'm really not clear if this Dispose()s of things...
            rightEditTab.TabPages.Clear();

            DoTreeCleanup();

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
            if(WorldIsModified)
            {
                if (MessageBox.Show("Close without saving?", "Close without saving world?", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }
            DoCloseCleanup();
        }


        private void TreeViewNavLeft_AfterSelect(object sender, TreeViewEventArgs e)
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
                        {
                            // Console.WriteLine("selected locker {0} [{1}]", thing.ToString(), sel.FullPath);
                            // probably reimplement this similar to the Machines+Reagents...
                            ThingLocker thingLocker = (ThingLocker)thing;
                            if (!thingLocker.DataLoadedToNode)
                            {
                                Console.WriteLine($"Locker {thingLocker.Id} not loaded, loading now");
                                LockerTreeViewAddChildNodes(sel.Nodes, thingLocker);
                            }
                        }
                        break;

                    case ThingManager.ThingType.Player:
                        Console.WriteLine("Selected a player....");
                        break;

                    case ThingManager.ThingType.LockerItem:
                        { 
                            ThingLockerItem thingLockerItem = (ThingLockerItem)thing;
                            TabPage lockerItemTabPage;
                            // here we have to check for a null XML and if so stand up an empty item...
                            if (thingLockerItem.XML == null)
                            {
                                // This will have to go into a "select item" control because
                                // of the <States /> bits AND for stored f/ex tanks we'll need to populate their atmo
                                // stubs I'm fairly certain or the game will NRE
                                // grab the parent ThingLocker
                                ThingLocker thingLocker = (ThingLocker)sel.Parent.Tag;
                                // figure out the slot number from the key....
                                // sel.Name should be format "ESLOT <int>"
                                Regex findSlot = new Regex(@"ESLOT\s+(\d+)");
                                Match slotMatch = Regex.Match(sel.Name, @"ESLOT\s+(\d+)");
                                int parentSlotId = Int32.Parse(slotMatch.Groups[1].Value);
                                MaxThingRefId++;
                                thingLockerItem.GenerateNewLockerItem(thingLocker, parentSlotId, (long)MaxThingRefId);
                                // rebuild the tree node bits...
                                sel.Name = thingLockerItem.Id.ToString();
                                sel.Text = thingLockerItem.Name;


                            }
                            string tabKey = $"{thingLockerItem.ParentReferenceId} {thingLockerItem.Name}";
                            if (!rightEditTab.TabPages.ContainsKey(tabKey))
                            {
                                lockerItemTabPage = new TabPage(tabKey);
                                lockerItemTabPage.Name = tabKey;
                                lockerItemTabPage.Controls.Add(new LockerItemEdit(thingLockerItem));
                                rightEditTab.TabPages.Add(lockerItemTabPage);
                                rightEditTab.SelectTab(lockerItemTabPage);
                            }
                            else
                            {
                                rightEditTab.SelectTab(tabKey);
                            }
                        }
                        break;

                    case ThingManager.ThingType.Machine:
                        {
                            ThingMachine thingMachine = (ThingMachine)thing;
                            TabPage machineReagentsPage;
                            // see if it's already open...
                            string tabKey = $"{thingMachine.Id} {thingMachine.GivenName} Reagents";
                            if (!rightEditTab.TabPages.ContainsKey(tabKey))
                            {
                                machineReagentsPage = new TabPage(tabKey)
                                {
                                    Name = tabKey
                                };
                                machineReagentsPage.Controls.Add(new ReagentEdit(thingMachine));
                                rightEditTab.TabPages.Add(machineReagentsPage);
                                rightEditTab.SelectTab(machineReagentsPage);
                            }
                            else
                            {
                                rightEditTab.SelectTab(tabKey);
                            }
                        }
                        break;

                    case ThingManager.ThingType.Tank:
                        {
                            ThingAtmosphere thingAtmosphere = (ThingAtmosphere)thing;
                            TabPage atmosphereThingPage;
                            string tabKey = $"{thingAtmosphere.Id} Tank {thingAtmosphere.GivenName}";
                            if(!rightEditTab.TabPages.ContainsKey(tabKey))
                            {
                                atmosphereThingPage = new TabPage(tabKey)
                                {
                                    Name = tabKey
                                };

                                // Add/build control page...
                                atmosphereThingPage.Controls.Add(new AtmosphereEdit(thingAtmosphere));

                                rightEditTab.TabPages.Add(atmosphereThingPage);
                                rightEditTab.SelectTab(atmosphereThingPage);

                            } else
                            {
                                rightEditTab.SelectTab(tabKey);
                            }

                        }
                        break;

                    default:
                        Console.WriteLine("Shit! Unknown Thing Type in TreeViewNavLeft_AfterSelect [{0}]", thing.TypeOf);
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
                    treeNode = nodes.Add($"ESLOT {loop.ToString()}", $"Slot: {loop} <EMPTY>");
                }
                treeNode.Tag = thing;
                // originally I was delayed loading so this was necessary so the expand happened...
                //treeNode.EnsureVisible();
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

        //
        // basic idea taken from stackoverflow
        // https://stackoverflow.com/questions/36895089/tabcontrol-with-close-and-add-button
        //

        private void rightEditTab_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = this.rightEditTab.TabPages[e.Index];
            var tabRect = this.rightEditTab.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);
            var closeImage = Properties.Resources.CloseXSmall;
            e.Graphics.DrawImage(closeImage,
                (tabRect.Right - closeImage.Width),
                tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                tabRect, tabPage.ForeColor, TextFormatFlags.Left);

        }

        private void rightEditTab_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (var i = 0; i < this.rightEditTab.TabPages.Count; i++)
                {
                    var tabRect = this.rightEditTab.GetTabRect(i);
                    tabRect.Inflate(-2, -2);
                    var closeImage = Properties.Resources.CloseXSmall;
                    var imageRect = new Rectangle(
                        (tabRect.Right - closeImage.Width),
                        tabRect.Top + (tabRect.Height - closeImage.Height) / 2,
                        closeImage.Width,
                        closeImage.Height);
                    if (imageRect.Contains(e.Location))
                    {
                        this.rightEditTab.TabPages.RemoveAt(i);
                        break;
                    }
                }
            }
            if(e.Button == MouseButtons.Middle)
            {
                TabControl tab = sender as TabControl;
                rightEditTab.TabPages.Remove(tab.TabPages[0]);
            }
        }

        private async Task<HttpResponseMessage> FetchRecipes()
        {
            HttpResponseMessage response;
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, Properties.Settings.Default.RecipeDataUrl);
            requestMessage.Headers.Accept.Clear();
            requestMessage.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            if(RecipeDataETag != null && RecipeDataETag.Length >= 1)
            {
                requestMessage.Headers.Add("If-None-Match", RecipeDataETag);
            }

            response = await httpClient.SendAsync(requestMessage);
            return response;
        }

        private async void toolStripMenuFileLoadData_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response;

            // Check to make sure that it's been more than at least a couple of hours...
            DateTime current = DateTime.UtcNow;
            DateTime future = RecipeDataRefreshedAt.AddHours(2);
            if( current < future)
            {
                Console.WriteLine($"Tried to fetch too soon {current} < {future}");
                MessageBox.Show("Cannot comply, wait a couple of hours!");
                return;
            }

            toolStripMenuFileLoadData.Enabled = false;
            // UI thread will go off for other things...
            response = await FetchRecipes();

            // Back in control of the UI thread...
            toolStripMenuFileLoadData.Enabled = true;

            DoRecipeDataCompletion(response);
            
        }

        private async void DoRecipeDataCompletion(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotModified:
                    RecipeDataRefreshedAt = DateTime.UtcNow;
                    break;

                case HttpStatusCode.OK:

                    // Load out data to file

                    if (File.Exists(_recipeDataFile))
                    {
                        File.Delete(_recipeDataFile);
                    }
                    FileStream dataFile = File.Open(_recipeDataFile, FileMode.CreateNew, FileAccess.ReadWrite);
                    byte[] recipeData = await response.Content.ReadAsByteArrayAsync();
                    await dataFile.WriteAsync(recipeData, 0, recipeData.Length);
                    dataFile.Close();

                    // Mark it...
                    RecipeDataRefreshedAt = DateTime.UtcNow;
                    RecipeDataETag = response.Headers.ETag.ToString();

                    // Import into autocompleters....
                    DoAutoCompleterLoad();
                    break;

                default:
                    // Wha? Show a message box...
                    MessageBox.Show($"Unknown Response while fetching recipe data {response.StatusCode}");
                    break;

            }
        }

        private void DoAutoCompleterLoad()
        {

            
            try
            {
                string recipesData = File.ReadAllText(_recipeDataFile);
                // try to deserialize it....
                RecipeDataFile recipesDataFile = JsonConvert.DeserializeObject<RecipeDataFile>(recipesData);
                Console.WriteLine($"Loaded {recipesDataFile.branch} from {recipesDataFile.updated_time} with {recipesDataFile.recipes.Count()}");
                GlobData._recipesDataFile = recipesDataFile;
            } catch (FileNotFoundException)
            {
                Console.WriteLine($"Unable to load recipe data file from {_recipeDataFile}");
                return;
            }

            // Massage into a dictionary...
            GlobData.Recipes.Clear();
            foreach(ItemRecipe ri in GlobData._recipesDataFile.recipes)
            {

                // Instead of exception try/catch, explicitly look

                if(GlobData.Recipes.ContainsKey(ri.item))
                {
                    GlobData.Recipes[ri.item].Add(ri);
                } else
                {
                    List<ItemRecipe> rl = new List<ItemRecipe>();
                    rl.Add(ri);
                    GlobData.Recipes.Add(ri.item, rl);
                }
            }
            return;
        }

        private void MainWindow_Closing(object sender, FormClosingEventArgs e)
        {
            // Save out our settings
            Properties.Settings.Default.Save();
            if (toolStripMenuFileSave.Enabled)
            {
                if(MessageBox.Show("You did not save your world file before closing the application. Would you like to save it?", "Save file?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    toolStripMenuFileSave_Click(toolStripMenuFileSave, new EventArgs());
                }
            }
        }
    }

}
