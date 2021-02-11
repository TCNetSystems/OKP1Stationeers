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
    public partial class ReagentEdit : UserControl
    {
        private ThingMachine machine = null;

        public ReagentEdit()
        {
            InitializeComponent();
        }


        public ReagentEdit(ThingMachine newMachine) : this()
        {

            // populate us...
            machine = newMachine;

            listBoxReagents.DataSource = machine.GetReagents();
            listBoxReagents.DisplayMember = "Name";
            listBoxReagents.ValueMember = "Self";
        }

        private void listBoxReagents_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThingReagent thing = (ThingReagent)listBoxReagents.SelectedValue;
            textBoxReagent.Text = thing.TypeName;
            textBoxQuantity.Text = thing.Quantity.ToString();
            // this causes a change event to light the save/cancel buttons, lets undo that...
            buttonSave.Enabled = false;
            buttonCancel.Enabled = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddReagent("Reagents.", 500);
        }

        private void AddReagent(string reagent, int quantity)
        {
            // stub up new empty item....
            XElement newItem = new XElement("Reagent",
                new XElement("TypeName", reagent),
                new XElement("Quantity", quantity)
                );
            machine.XML.Element("Reagents").Add(newItem);
            listBoxReagents.DataSource = machine.GetReagents();
            listBoxReagents.SelectedIndex = listBoxReagents.Items.Count - 1;
        }

        private void buttonAddAll_Click(object sender, EventArgs e)
        {
            foreach (var reagent in textBoxReagent.Items)
            {
                if (!HasReagent(reagent.ToString()))
                {
                    AddReagent(reagent.ToString(), 500);
                }
                //MessageBox.Show(string.Format("{0} {1}", reagent, item));
            }
        }

        private bool HasReagent(string reagent)
        {
            for (int i = 0; i < listBoxReagents.Items.Count; i++)
            {
                if (listBoxReagents.Items[i].ToString() == reagent)
                { return true; }
            }
            return false;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ThingReagent thing = (ThingReagent)listBoxReagents.SelectedValue;
            thing.TypeName = textBoxReagent.Text;
            // this will throw an exception on garbage
            thing.Quantity = Single.Parse(textBoxQuantity.Text);
            thing.Save();
            buttonSave.Enabled = false;
            buttonCancel.Enabled = false;
            // refresh list item information.....
            listBoxReagents.DataSource = machine.GetReagents();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // reset values
            ThingReagent thing = (ThingReagent)listBoxReagents.SelectedValue;
            textBoxReagent.Text = thing.TypeName;
            textBoxQuantity.Text = thing.Quantity.ToString();
            buttonSave.Enabled = false;
            buttonCancel.Enabled = false;
        }

        private void textBoxReagent_TextChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = true;
            buttonCancel.Enabled = true;
        }

        private void textBoxQuantity_TextChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = true;
            buttonCancel.Enabled = true;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // if we've got it do it....
            if (listBoxReagents.SelectedIndex >= 0)
            {
                ThingReagent thing = (ThingReagent)listBoxReagents.SelectedValue;
                thing.XML.Remove();
                listBoxReagents.DataSource = machine.GetReagents();
            }
        }


    }
}

