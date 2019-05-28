namespace LeafSQL.UI.Forms
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.splitContainerPrimaryVerticle = new System.Windows.Forms.SplitContainer();
            this.treeViewDatabase = new System.Windows.Forms.TreeView();
            this.tabControlPages = new System.Windows.Forms.TabControl();
            this.imageListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdNewFile = new System.Windows.Forms.ToolStripButton();
            this.cmdOpenFile = new System.Windows.Forms.ToolStripButton();
            this.cmdSave = new System.Windows.Forms.ToolStripButton();
            this.cmdSaveAll = new System.Windows.Forms.ToolStripButton();
            this.cmdCloseFile = new System.Windows.Forms.ToolStripButton();
            this.cmdCut = new System.Windows.Forms.ToolStripButton();
            this.cmdCopy = new System.Windows.Forms.ToolStripButton();
            this.cmdPaste = new System.Windows.Forms.ToolStripButton();
            this.cmdReplace = new System.Windows.Forms.ToolStripButton();
            this.cmdFind = new System.Windows.Forms.ToolStripButton();
            this.cmdUndo = new System.Windows.Forms.ToolStripButton();
            this.cmdRedo = new System.Windows.Forms.ToolStripButton();
            this.cmdDecreaseIndent = new System.Windows.Forms.ToolStripButton();
            this.cmdIncreaseIndent = new System.Windows.Forms.ToolStripButton();
            this.cmdCommentLines = new System.Windows.Forms.ToolStripButton();
            this.cmdUncommentLines = new System.Windows.Forms.ToolStripButton();
            this.cmdRun = new System.Windows.Forms.ToolStripButton();
            this.cmdStop = new System.Windows.Forms.ToolStripButton();
            this.cmdToggleProjectPanel = new System.Windows.Forms.ToolStripButton();
            this.cmdToggleOutput = new System.Windows.Forms.ToolStripButton();
            this.cmdToggleToolsPanel = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrimaryVerticle)).BeginInit();
            this.splitContainerPrimaryVerticle.Panel1.SuspendLayout();
            this.splitContainerPrimaryVerticle.Panel2.SuspendLayout();
            this.splitContainerPrimaryVerticle.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerPrimaryVerticle
            // 
            this.splitContainerPrimaryVerticle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPrimaryVerticle.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerPrimaryVerticle.Location = new System.Drawing.Point(0, 49);
            this.splitContainerPrimaryVerticle.Name = "splitContainerPrimaryVerticle";
            // 
            // splitContainerPrimaryVerticle.Panel1
            // 
            this.splitContainerPrimaryVerticle.Panel1.Controls.Add(this.treeViewDatabase);
            // 
            // splitContainerPrimaryVerticle.Panel2
            // 
            this.splitContainerPrimaryVerticle.Panel2.Controls.Add(this.tabControlPages);
            this.splitContainerPrimaryVerticle.Size = new System.Drawing.Size(1008, 659);
            this.splitContainerPrimaryVerticle.SplitterDistance = 252;
            this.splitContainerPrimaryVerticle.TabIndex = 0;
            // 
            // treeViewDatabase
            // 
            this.treeViewDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDatabase.Location = new System.Drawing.Point(0, 0);
            this.treeViewDatabase.Name = "treeViewDatabase";
            this.treeViewDatabase.Size = new System.Drawing.Size(252, 659);
            this.treeViewDatabase.TabIndex = 0;
            this.treeViewDatabase.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewDatabase_BeforeExpand);
            this.treeViewDatabase.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewDatabase_NodeMouseClick);
            // 
            // tabControlPages
            // 
            this.tabControlPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPages.Location = new System.Drawing.Point(0, 0);
            this.tabControlPages.Name = "tabControlPages";
            this.tabControlPages.SelectedIndex = 0;
            this.tabControlPages.Size = new System.Drawing.Size(752, 659);
            this.tabControlPages.TabIndex = 0;
            // 
            // imageListTreeView
            // 
            this.imageListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeView.ImageStream")));
            this.imageListTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTreeView.Images.SetKeyName(0, "Server");
            this.imageListTreeView.Images.SetKeyName(1, "Document");
            this.imageListTreeView.Images.SetKeyName(2, "Documents");
            this.imageListTreeView.Images.SetKeyName(3, "Logins");
            this.imageListTreeView.Images.SetKeyName(4, "Schema");
            this.imageListTreeView.Images.SetKeyName(5, "Schemas");
            this.imageListTreeView.Images.SetKeyName(6, "Login");
            this.imageListTreeView.Images.SetKeyName(7, "Index");
            this.imageListTreeView.Images.SetKeyName(8, "Indexes");
            this.imageListTreeView.Images.SetKeyName(9, "IndexAttribute");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdNewFile,
            this.cmdOpenFile,
            this.cmdSave,
            this.cmdSaveAll,
            this.cmdCloseFile,
            this.ToolStripSeparator1,
            this.cmdCut,
            this.cmdCopy,
            this.cmdPaste,
            this.ToolStripSeparator11,
            this.cmdReplace,
            this.cmdFind,
            this.ToolStripSeparator14,
            this.cmdUndo,
            this.cmdRedo,
            this.ToolStripSeparator2,
            this.cmdDecreaseIndent,
            this.cmdIncreaseIndent,
            this.cmdCommentLines,
            this.cmdUncommentLines,
            this.ToolStripSeparator3,
            this.cmdRun,
            this.cmdStop,
            this.ToolStripSeparator18,
            this.cmdToggleProjectPanel,
            this.cmdToggleOutput,
            this.cmdToggleToolsPanel});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip.TabIndex = 16;
            this.toolStrip.TabStop = true;
            this.toolStrip.Text = "toolStrip";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripSeparator11
            // 
            this.ToolStripSeparator11.Name = "ToolStripSeparator11";
            this.ToolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripSeparator14
            // 
            this.ToolStripSeparator14.Name = "ToolStripSeparator14";
            this.ToolStripSeparator14.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripSeparator18
            // 
            this.ToolStripSeparator18.Name = "ToolStripSeparator18";
            this.ToolStripSeparator18.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // cmdNewFile
            // 
            this.cmdNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdNewFile.Image = global::LeafSQL.UI.Properties.Resources.ToolNewFile;
            this.cmdNewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdNewFile.Name = "cmdNewFile";
            this.cmdNewFile.Size = new System.Drawing.Size(23, 22);
            this.cmdNewFile.Text = "New File";
            this.cmdNewFile.Click += new System.EventHandler(this.CmdNewFile_Click);
            // 
            // cmdOpenFile
            // 
            this.cmdOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdOpenFile.Image = global::LeafSQL.UI.Properties.Resources.ToolOpenFile;
            this.cmdOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdOpenFile.Name = "cmdOpenFile";
            this.cmdOpenFile.Size = new System.Drawing.Size(23, 22);
            this.cmdOpenFile.Text = "Open File";
            // 
            // cmdSave
            // 
            this.cmdSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdSave.Image = global::LeafSQL.UI.Properties.Resources.ToolSave;
            this.cmdSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(23, 22);
            this.cmdSave.Text = "Save Current Document";
            // 
            // cmdSaveAll
            // 
            this.cmdSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdSaveAll.Image = global::LeafSQL.UI.Properties.Resources.ToolSaveAll;
            this.cmdSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSaveAll.Name = "cmdSaveAll";
            this.cmdSaveAll.Size = new System.Drawing.Size(23, 22);
            this.cmdSaveAll.Text = "Save All Open Documents";
            // 
            // cmdCloseFile
            // 
            this.cmdCloseFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdCloseFile.Image = global::LeafSQL.UI.Properties.Resources.ToolCloseFile;
            this.cmdCloseFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdCloseFile.Name = "cmdCloseFile";
            this.cmdCloseFile.Size = new System.Drawing.Size(23, 22);
            this.cmdCloseFile.Text = "Close File";
            // 
            // cmdCut
            // 
            this.cmdCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdCut.Image = global::LeafSQL.UI.Properties.Resources.ToolCut;
            this.cmdCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdCut.Name = "cmdCut";
            this.cmdCut.Size = new System.Drawing.Size(23, 22);
            this.cmdCut.Text = "Cut";
            // 
            // cmdCopy
            // 
            this.cmdCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdCopy.Image = global::LeafSQL.UI.Properties.Resources.ToolCopy;
            this.cmdCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(23, 22);
            this.cmdCopy.Text = "Copy";
            // 
            // cmdPaste
            // 
            this.cmdPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdPaste.Image = global::LeafSQL.UI.Properties.Resources.ToolPaste;
            this.cmdPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdPaste.Name = "cmdPaste";
            this.cmdPaste.Size = new System.Drawing.Size(23, 22);
            this.cmdPaste.Text = "Paste";
            // 
            // cmdReplace
            // 
            this.cmdReplace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdReplace.Image = global::LeafSQL.UI.Properties.Resources.ToolReplace;
            this.cmdReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdReplace.Name = "cmdReplace";
            this.cmdReplace.Size = new System.Drawing.Size(23, 22);
            this.cmdReplace.Text = "Replace";
            // 
            // cmdFind
            // 
            this.cmdFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdFind.Image = global::LeafSQL.UI.Properties.Resources.ToolFind;
            this.cmdFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.Size = new System.Drawing.Size(23, 22);
            this.cmdFind.Text = "Find";
            // 
            // cmdUndo
            // 
            this.cmdUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdUndo.Image = global::LeafSQL.UI.Properties.Resources.ToolUndo;
            this.cmdUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdUndo.Name = "cmdUndo";
            this.cmdUndo.Size = new System.Drawing.Size(23, 22);
            this.cmdUndo.Text = "Undo";
            // 
            // cmdRedo
            // 
            this.cmdRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdRedo.Image = global::LeafSQL.UI.Properties.Resources.ToolRedo;
            this.cmdRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdRedo.Name = "cmdRedo";
            this.cmdRedo.Size = new System.Drawing.Size(23, 22);
            this.cmdRedo.Text = "Redo";
            // 
            // cmdDecreaseIndent
            // 
            this.cmdDecreaseIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdDecreaseIndent.Image = global::LeafSQL.UI.Properties.Resources.ToolDecreaseIndent;
            this.cmdDecreaseIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDecreaseIndent.Name = "cmdDecreaseIndent";
            this.cmdDecreaseIndent.Size = new System.Drawing.Size(23, 22);
            this.cmdDecreaseIndent.Text = "Decrease Indent";
            // 
            // cmdIncreaseIndent
            // 
            this.cmdIncreaseIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdIncreaseIndent.Image = global::LeafSQL.UI.Properties.Resources.ToolIncreaseIndent;
            this.cmdIncreaseIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdIncreaseIndent.Name = "cmdIncreaseIndent";
            this.cmdIncreaseIndent.Size = new System.Drawing.Size(23, 22);
            this.cmdIncreaseIndent.Text = "Increase Indent";
            // 
            // cmdCommentLines
            // 
            this.cmdCommentLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdCommentLines.Image = global::LeafSQL.UI.Properties.Resources.ToolCommentLines;
            this.cmdCommentLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdCommentLines.Name = "cmdCommentLines";
            this.cmdCommentLines.Size = new System.Drawing.Size(23, 22);
            this.cmdCommentLines.Text = "Comment Lines";
            // 
            // cmdUncommentLines
            // 
            this.cmdUncommentLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdUncommentLines.Image = global::LeafSQL.UI.Properties.Resources.ToolUnCommentLines;
            this.cmdUncommentLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdUncommentLines.Name = "cmdUncommentLines";
            this.cmdUncommentLines.Size = new System.Drawing.Size(23, 22);
            this.cmdUncommentLines.Text = "Uncomment Lines";
            // 
            // cmdRun
            // 
            this.cmdRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdRun.Image = global::LeafSQL.UI.Properties.Resources.ToolRun;
            this.cmdRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdRun.Name = "cmdRun";
            this.cmdRun.Size = new System.Drawing.Size(23, 22);
            this.cmdRun.Text = "Run";
            this.cmdRun.ToolTipText = "Run";
            // 
            // cmdStop
            // 
            this.cmdStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdStop.Image = global::LeafSQL.UI.Properties.Resources.ToolStop;
            this.cmdStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(23, 22);
            this.cmdStop.Text = "Stop";
            // 
            // cmdToggleProjectPanel
            // 
            this.cmdToggleProjectPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdToggleProjectPanel.Image = global::LeafSQL.UI.Properties.Resources.ToolProjectPanel;
            this.cmdToggleProjectPanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdToggleProjectPanel.Name = "cmdToggleProjectPanel";
            this.cmdToggleProjectPanel.Size = new System.Drawing.Size(23, 22);
            this.cmdToggleProjectPanel.Text = "Toggle Project Panel";
            // 
            // cmdToggleOutput
            // 
            this.cmdToggleOutput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdToggleOutput.Image = global::LeafSQL.UI.Properties.Resources.ToolOutputPanel;
            this.cmdToggleOutput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdToggleOutput.Name = "cmdToggleOutput";
            this.cmdToggleOutput.Size = new System.Drawing.Size(23, 22);
            this.cmdToggleOutput.Text = "Toggle Output Panel";
            // 
            // cmdToggleToolsPanel
            // 
            this.cmdToggleToolsPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdToggleToolsPanel.Image = global::LeafSQL.UI.Properties.Resources.ToolToolsPanel;
            this.cmdToggleToolsPanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdToggleToolsPanel.Name = "cmdToggleToolsPanel";
            this.cmdToggleToolsPanel.Size = new System.Drawing.Size(23, 22);
            this.cmdToggleToolsPanel.Text = "Toggle Tools Panel";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splitContainerPrimaryVerticle);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LeafSQL Manager";
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.splitContainerPrimaryVerticle.Panel1.ResumeLayout(false);
            this.splitContainerPrimaryVerticle.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrimaryVerticle)).EndInit();
            this.splitContainerPrimaryVerticle.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerPrimaryVerticle;
        private System.Windows.Forms.TreeView treeViewDatabase;
        private System.Windows.Forms.TabControl tabControlPages;
        private System.Windows.Forms.ImageList imageListTreeView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        internal System.Windows.Forms.ToolStrip toolStrip;
        internal System.Windows.Forms.ToolStripButton cmdNewFile;
        internal System.Windows.Forms.ToolStripButton cmdOpenFile;
        internal System.Windows.Forms.ToolStripButton cmdSave;
        internal System.Windows.Forms.ToolStripButton cmdSaveAll;
        internal System.Windows.Forms.ToolStripButton cmdCloseFile;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton cmdCut;
        internal System.Windows.Forms.ToolStripButton cmdCopy;
        internal System.Windows.Forms.ToolStripButton cmdPaste;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator11;
        internal System.Windows.Forms.ToolStripButton cmdReplace;
        internal System.Windows.Forms.ToolStripButton cmdFind;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator14;
        internal System.Windows.Forms.ToolStripButton cmdUndo;
        internal System.Windows.Forms.ToolStripButton cmdRedo;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        public System.Windows.Forms.ToolStripButton cmdDecreaseIndent;
        internal System.Windows.Forms.ToolStripButton cmdIncreaseIndent;
        internal System.Windows.Forms.ToolStripButton cmdCommentLines;
        internal System.Windows.Forms.ToolStripButton cmdUncommentLines;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton cmdRun;
        internal System.Windows.Forms.ToolStripButton cmdStop;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator18;
        internal System.Windows.Forms.ToolStripButton cmdToggleProjectPanel;
        internal System.Windows.Forms.ToolStripButton cmdToggleOutput;
        internal System.Windows.Forms.ToolStripButton cmdToggleToolsPanel;
    }
}

