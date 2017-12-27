using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace OKP1_Stationeers_Editor
{
    public partial class LockerItemEdit : UserControl
    {
        private ThingLockerItem lockerItem = null;
        public LockerItemEdit()
        {
            InitializeComponent();
        }

        public LockerItemEdit(ThingLockerItem thing)
        {
            InitializeComponent();

            lockerItem = thing;

            // populate form elements

            textBoxXML.Text = lockerItem.XML.ToString();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            TabPage myPage = (TabPage)this.Parent;
            TabControl myTab = (TabControl)myPage.Parent;
            myTab.TabPages.Remove(myPage);

        }
    }
}
