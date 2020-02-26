using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitsNet;
using OKP1_Stationeers_Editor.Stationeers;

namespace OKP1_Stationeers_Editor
{
    public partial class AtmosphereEdit : UserControl
    {
        private ThingAtmosphere atmosphere = null;
        public AtmosphereEdit()
        {
            InitializeComponent();
        }

        public AtmosphereEdit(ThingAtmosphere newAtmosphere) : this()
        {
            atmosphere = newAtmosphere;
            labelVolume.Text = $"Tank Volume is {atmosphere.gasMixture.Volume.ToString()}L";

            refreshCalculatedLabels();

            listBoxContents.DisplayMember = "Key";
            listBoxContents.ValueMember = "Value";
            listBoxContents.DataSource = new BindingSource(atmosphere.gasMixture.gases, null);

            

        }

        private void refreshCalculatedLabels()
        {
            double atmoPVal = 0.0;
            double atmoTVal = 0.0;

            // Totally empty things give a NaN!
            if (!float.IsNaN(atmosphere.gasMixture.Pressure))
            {
                // Pressure atmo = Pressure.FromPascals(atmosphere.gasMixture.Pressure);
                atmoPVal = atmosphere.gasMixture.Pressure;
            }

            labelPressurekPa.Text = $"{atmoPVal.ToString("F3")} kPa";
            

            if (!float.IsNaN(atmosphere.gasMixture.Temperature))
            {
                Temperature atmoT = Temperature.FromKelvins(atmosphere.gasMixture.Temperature);
                atmoTVal = atmoT.DegreesCelsius;
            }

            labelTemperatureC.Text = $"{atmoTVal.ToString("F2")} C";

            labelMol.Text = $"{atmosphere.gasMixture.TotalMoles} mol";
        }

        private void listBoxContents_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            Mole thing = (Mole)listBoxContents.SelectedValue;
            textBoxQuantity.Text = thing.Quantity.ToString();
            textBoxEnergy.Text = thing.Energy.ToString();
            labelSpecificHeat.Text = thing.SpecificHeat.ToString();
            Console.WriteLine($"Listbox Selected Index changed - val is type {listBoxContents.SelectedValue.GetType().ToString()}");

        }

        private void textBoxQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Mole thing = (Mole)listBoxContents.SelectedValue;
                thing.Quantity = float.Parse(textBoxQuantity.Text);
                refreshCalculatedLabels();
                buttonSave.Enabled = true;
            } catch (Exception)
            {
                return;
            }
            
        }

        private void textBoxEnergy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Mole thing = (Mole)listBoxContents.SelectedValue;
                thing.Energy = float.Parse(textBoxEnergy.Text);
                refreshCalculatedLabels();
                buttonSave.Enabled = true;
            } catch (Exception)
            {
                return;
            }
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            buttonSave.Enabled = false;
            atmosphere.Save();
        }
    }
}
