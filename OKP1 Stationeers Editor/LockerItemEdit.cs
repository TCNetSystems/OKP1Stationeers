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
        enum EditType { Unknown, Stackable };
        private EditType _editType = EditType.Unknown;
        public LockerItemEdit()
        {
            InitializeComponent();
        }

        public LockerItemEdit(ThingLockerItem thing)
        {
            InitializeComponent();
            SuspendLayout();
            lockerItem = thing;
            textBoxXML.Text = lockerItem.XML.ToString();

            // figure out what we're editing
            switch (lockerItem.XML.Attributes().FirstOrDefault(a => a.Name.LocalName == "type").Value)
            {
                case "StackableSaveData":
                    _editType = EditType.Stackable;
                    break;
                default:
                    _editType = EditType.Unknown;
                    break;
            }

            _generateControls();


            ResumeLayout(true);

        }

        private void _generateControls()
        {
            switch (_editType)
            {
                 case EditType.Stackable:
                    {
                        TableLayoutPanel editorLayoutPanel = new TableLayoutPanel();   
                        Label itemPrefabNameLabel = new Label();
                        Label itemQuantityLabel = new Label();
                        TextBox itemPrefabName = new TextBox();
                        TextBox itemQuantity = new TextBox();
                        Button buttonSave = new Button();

                        tableLayoutPanel1.Controls.Add(editorLayoutPanel, 0, 0);

                        editorLayoutPanel.AutoSize = true;
                        editorLayoutPanel.ColumnCount = 2;
                        editorLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                        editorLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                        editorLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
                        editorLayoutPanel.Location = new System.Drawing.Point(3, 3);
                        editorLayoutPanel.Name = "editorLayoutPanel";
                        editorLayoutPanel.RowCount = 2;
                        editorLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
                        editorLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
                        editorLayoutPanel.Size = new System.Drawing.Size(259, 40);
                        editorLayoutPanel.TabIndex = 2;


                        itemPrefabNameLabel.Name = "itemPrefabNameLabel";
                        itemPrefabNameLabel.AutoSize = true;
                        itemPrefabNameLabel.Text = "Prefab Name";

                        // figure out how much space to fill to the left?

                        editorLayoutPanel.Controls.Add(itemPrefabNameLabel,0,0);

                        itemPrefabName.Name = "itemPrefabName";
                        itemPrefabName.Size = new Size(124, 20);
                        itemPrefabName.Text = lockerItem.PrefabName;

                        editorLayoutPanel.Controls.Add(itemPrefabName,1,0);
                        

                        itemQuantityLabel.Name = "itemQuantityLabel";
                        itemQuantityLabel.AutoSize = true;
                        itemQuantityLabel.Text = "Quantity";

                        editorLayoutPanel.Controls.Add(itemQuantityLabel,0,1);


                        itemQuantity.Name = "itemQuantity";
                        itemQuantity.Size = new Size(124, 20);
                        itemQuantity.Text = lockerItem.Quantity.ToString();

                        editorLayoutPanel.Controls.Add(itemQuantity,1,1);

                        


                    }
                    break;

                default:
                    {
                        Label unsupportedEdit = new Label();
                        unsupportedEdit.Text = "Unknown/Unusupported Edit Type";
                        unsupportedEdit.Name = "unsupportedEdit";
                        unsupportedEdit.Size = new Size(180, 14);
                        tableLayoutPanel1.Controls.Add(unsupportedEdit,0,0);
                    }
                    break;
            }
        }
    }
}
