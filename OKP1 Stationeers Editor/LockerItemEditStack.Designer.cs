
namespace OKP1_Stationeers_Editor
{
    partial class LockerItemEditStack
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lockerStackEditPanel = new System.Windows.Forms.TableLayoutPanel();
            this.itemPrefabNameLabel = new System.Windows.Forms.Label();
            this.itemQuantityLabel = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.lockerStackEditPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lockerStackEditPanel
            // 
            this.lockerStackEditPanel.ColumnCount = 2;
            this.lockerStackEditPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.lockerStackEditPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.lockerStackEditPanel.Controls.Add(this.itemPrefabNameLabel, 0, 0);
            this.lockerStackEditPanel.Controls.Add(this.itemQuantityLabel, 0, 1);
            this.lockerStackEditPanel.Controls.Add(this.buttonSave, 0, 2);
            this.lockerStackEditPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lockerStackEditPanel.Location = new System.Drawing.Point(0, 0);
            this.lockerStackEditPanel.Name = "lockerStackEditPanel";
            this.lockerStackEditPanel.RowCount = 3;
            this.lockerStackEditPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.lockerStackEditPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.lockerStackEditPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.lockerStackEditPanel.Size = new System.Drawing.Size(265, 242);
            this.lockerStackEditPanel.TabIndex = 2;
            // 
            // itemPrefabNameLabel
            // 
            this.itemPrefabNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.itemPrefabNameLabel.AutoSize = true;
            this.itemPrefabNameLabel.Location = new System.Drawing.Point(60, 3);
            this.itemPrefabNameLabel.Name = "itemPrefabNameLabel";
            this.itemPrefabNameLabel.Size = new System.Drawing.Size(69, 13);
            this.itemPrefabNameLabel.TabIndex = 0;
            this.itemPrefabNameLabel.Text = "Prefab Name";
            // 
            // itemQuantityLabel
            // 
            this.itemQuantityLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.itemQuantityLabel.AutoSize = true;
            this.itemQuantityLabel.Location = new System.Drawing.Point(83, 23);
            this.itemQuantityLabel.Name = "itemQuantityLabel";
            this.itemQuantityLabel.Size = new System.Drawing.Size(46, 13);
            this.itemQuantityLabel.TabIndex = 1;
            this.itemQuantityLabel.Text = "Quantity";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonSave.Location = new System.Drawing.Point(28, 43);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // LockerItemEditStack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lockerStackEditPanel);
            this.Name = "LockerItemEditStack";
            this.Size = new System.Drawing.Size(265, 242);
            this.lockerStackEditPanel.ResumeLayout(false);
            this.lockerStackEditPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel lockerStackEditPanel;
        private System.Windows.Forms.Label itemPrefabNameLabel;
        private System.Windows.Forms.Label itemQuantityLabel;
        private System.Windows.Forms.Button buttonSave;
    }
}
