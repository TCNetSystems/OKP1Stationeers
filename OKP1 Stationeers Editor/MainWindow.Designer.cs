namespace OKP1_Stationeers_Editor
{
    partial class MainWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Players");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Lockers");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Machines");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Tanks");
            this.EdMainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuFileLoadData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusOpenedFileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelMaxRef = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelGameVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewNavLeft = new System.Windows.Forms.TreeView();
            this.rightEditTab = new System.Windows.Forms.TabControl();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.EdMainMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EdMainMenuStrip
            // 
            this.EdMainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuFile});
            this.EdMainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.EdMainMenuStrip.Name = "EdMainMenuStrip";
            this.EdMainMenuStrip.Size = new System.Drawing.Size(813, 24);
            this.EdMainMenuStrip.TabIndex = 0;
            this.EdMainMenuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuFile
            // 
            this.toolStripMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuFileOpen,
            this.toolStripMenuFileClose,
            this.toolStripMenuFileSave,
            this.toolStripSeparator1,
            this.toolStripMenuFileLoadData,
            this.toolStripSeparator2,
            this.toolStripMenuFileExit});
            this.toolStripMenuFile.Name = "toolStripMenuFile";
            this.toolStripMenuFile.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuFile.Text = "&File";
            // 
            // toolStripMenuFileOpen
            // 
            this.toolStripMenuFileOpen.Name = "toolStripMenuFileOpen";
            this.toolStripMenuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuFileOpen.Size = new System.Drawing.Size(206, 22);
            this.toolStripMenuFileOpen.Text = "&Open...";
            this.toolStripMenuFileOpen.Click += new System.EventHandler(this.ToolStripMenuFileOpen_Click);
            // 
            // toolStripMenuFileClose
            // 
            this.toolStripMenuFileClose.Enabled = false;
            this.toolStripMenuFileClose.Name = "toolStripMenuFileClose";
            this.toolStripMenuFileClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuFileClose.Size = new System.Drawing.Size(206, 22);
            this.toolStripMenuFileClose.Text = "&Close";
            this.toolStripMenuFileClose.Click += new System.EventHandler(this.ToolStripMenuClose_Click);
            // 
            // toolStripMenuFileSave
            // 
            this.toolStripMenuFileSave.Enabled = false;
            this.toolStripMenuFileSave.Name = "toolStripMenuFileSave";
            this.toolStripMenuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuFileSave.Size = new System.Drawing.Size(206, 22);
            this.toolStripMenuFileSave.Text = "&Save";
            this.toolStripMenuFileSave.Click += new System.EventHandler(this.toolStripMenuFileSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(203, 6);
            // 
            // toolStripMenuFileLoadData
            // 
            this.toolStripMenuFileLoadData.Name = "toolStripMenuFileLoadData";
            this.toolStripMenuFileLoadData.Size = new System.Drawing.Size(206, 22);
            this.toolStripMenuFileLoadData.Text = "Load Autocomplete Data";
            this.toolStripMenuFileLoadData.ToolTipText = "Downloads autocomplete data from stationeering.com";
            this.toolStripMenuFileLoadData.Click += new System.EventHandler(this.toolStripMenuFileLoadData_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(203, 6);
            // 
            // toolStripMenuFileExit
            // 
            this.toolStripMenuFileExit.Name = "toolStripMenuFileExit";
            this.toolStripMenuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.toolStripMenuFileExit.Size = new System.Drawing.Size(206, 22);
            this.toolStripMenuFileExit.Text = "E&xit";
            this.toolStripMenuFileExit.Click += new System.EventHandler(this.ToolStripMenuFileExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusOpenedFileLabel,
            this.toolStripStatusLabelMaxRef,
            this.toolStripStatusLabelGameVersion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 534);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(813, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusOpenedFileLabel
            // 
            this.toolStripStatusOpenedFileLabel.Name = "toolStripStatusOpenedFileLabel";
            this.toolStripStatusOpenedFileLabel.Size = new System.Drawing.Size(51, 17);
            this.toolStripStatusOpenedFileLabel.Text = "No file...";
            // 
            // toolStripStatusLabelMaxRef
            // 
            this.toolStripStatusLabelMaxRef.Name = "toolStripStatusLabelMaxRef";
            this.toolStripStatusLabelMaxRef.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabelGameVersion
            // 
            this.toolStripStatusLabelGameVersion.Name = "toolStripStatusLabelGameVersion";
            this.toolStripStatusLabelGameVersion.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewNavLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rightEditTab);
            this.splitContainer1.Size = new System.Drawing.Size(813, 510);
            this.splitContainer1.SplitterDistance = 271;
            this.splitContainer1.TabIndex = 3;
            // 
            // treeViewNavLeft
            // 
            this.treeViewNavLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewNavLeft.Enabled = false;
            this.treeViewNavLeft.Location = new System.Drawing.Point(0, 0);
            this.treeViewNavLeft.Name = "treeViewNavLeft";
            treeNode1.Name = "Players";
            treeNode1.Text = "Players";
            treeNode2.Name = "Lockers";
            treeNode2.Text = "Lockers";
            treeNode3.Name = "Machines";
            treeNode3.Text = "Machines";
            treeNode4.Name = "Tanks";
            treeNode4.Text = "Tanks";
            this.treeViewNavLeft.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.treeViewNavLeft.Size = new System.Drawing.Size(271, 510);
            this.treeViewNavLeft.TabIndex = 0;
            this.treeViewNavLeft.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewNavLeft_AfterSelect);
            // 
            // rightEditTab
            // 
            this.rightEditTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightEditTab.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.rightEditTab.Location = new System.Drawing.Point(0, 0);
            this.rightEditTab.Name = "rightEditTab";
            this.rightEditTab.Padding = new System.Drawing.Point(12, 4);
            this.rightEditTab.SelectedIndex = 0;
            this.rightEditTab.Size = new System.Drawing.Size(538, 510);
            this.rightEditTab.TabIndex = 0;
            this.rightEditTab.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.rightEditTab_DrawItem);
            this.rightEditTab.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rightEditTab_MouseClick);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(23, 23);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 556);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.EdMainMenuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.EdMainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "OKP1 Stationeers Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_Closing);
            this.EdMainMenuStrip.ResumeLayout(false);
            this.EdMainMenuStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip EdMainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFileClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFileExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFileSave;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusOpenedFileLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewNavLeft;
        private System.Windows.Forms.TabControl rightEditTab;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMaxRef;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelGameVersion;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFileLoadData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}