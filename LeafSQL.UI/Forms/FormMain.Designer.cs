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
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.cmdNewFile = new System.Windows.Forms.ToolStripButton();
            this.cmdOpenFile = new System.Windows.Forms.ToolStripButton();
            this.cmdSave = new System.Windows.Forms.ToolStripButton();
            this.cmdSaveAll = new System.Windows.Forms.ToolStripButton();
            this.cmdCloseFile = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdCut = new System.Windows.Forms.ToolStripButton();
            this.cmdCopy = new System.Windows.Forms.ToolStripButton();
            this.cmdPaste = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdReplace = new System.Windows.Forms.ToolStripButton();
            this.cmdFind = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdUndo = new System.Windows.Forms.ToolStripButton();
            this.cmdRedo = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdDecreaseIndent = new System.Windows.Forms.ToolStripButton();
            this.cmdIncreaseIndent = new System.Windows.Forms.ToolStripButton();
            this.cmdCommentLines = new System.Windows.Forms.ToolStripButton();
            this.cmdUncommentLines = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdRun = new System.Windows.Forms.ToolStripButton();
            this.cmdStop = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdToggleProjectPanel = new System.Windows.Forms.ToolStripButton();
            this.cmdToggleOutput = new System.Windows.Forms.ToolStripButton();
            this.cmdToggleToolsPanel = new System.Windows.Forms.ToolStripButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitContainerCodeAndResult = new System.Windows.Forms.SplitContainer();
            this.tabControlResults = new System.Windows.Forms.TabControl();
            this.tabPageResults = new System.Windows.Forms.TabPage();
            this.dataGridSearchDocuments = new System.Windows.Forms.DataGridView();
            this.tabPagePlan = new System.Windows.Forms.TabPage();
            this.dataGridViewPlan = new System.Windows.Forms.DataGridView();
            this.ColumnPlanOrdinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCoveredAttributes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnScannedNodes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnResultingNodes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnIntersectedNodes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrimaryVerticle)).BeginInit();
            this.splitContainerPrimaryVerticle.Panel1.SuspendLayout();
            this.splitContainerPrimaryVerticle.Panel2.SuspendLayout();
            this.splitContainerPrimaryVerticle.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCodeAndResult)).BeginInit();
            this.splitContainerCodeAndResult.Panel1.SuspendLayout();
            this.splitContainerCodeAndResult.Panel2.SuspendLayout();
            this.splitContainerCodeAndResult.SuspendLayout();
            this.tabControlResults.SuspendLayout();
            this.tabPageResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSearchDocuments)).BeginInit();
            this.tabPagePlan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlan)).BeginInit();
            this.tabPageOutput.SuspendLayout();
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
            this.splitContainerPrimaryVerticle.Panel2.Controls.Add(this.splitContainerCodeAndResult);
            this.splitContainerPrimaryVerticle.Panel2.Controls.Add(this.splitter1);
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
            this.tabControlPages.Size = new System.Drawing.Size(749, 519);
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
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // ToolStripSeparator11
            // 
            this.ToolStripSeparator11.Name = "ToolStripSeparator11";
            this.ToolStripSeparator11.Size = new System.Drawing.Size(6, 25);
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
            // ToolStripSeparator14
            // 
            this.ToolStripSeparator14.Name = "ToolStripSeparator14";
            this.ToolStripSeparator14.Size = new System.Drawing.Size(6, 25);
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
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            this.cmdRun.Click += new System.EventHandler(this.CmdRun_Click);
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
            // ToolStripSeparator18
            // 
            this.ToolStripSeparator18.Name = "ToolStripSeparator18";
            this.ToolStripSeparator18.Size = new System.Drawing.Size(6, 25);
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
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 659);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // splitContainerCodeAndResult
            // 
            this.splitContainerCodeAndResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerCodeAndResult.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerCodeAndResult.Location = new System.Drawing.Point(3, 0);
            this.splitContainerCodeAndResult.Name = "splitContainerCodeAndResult";
            this.splitContainerCodeAndResult.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerCodeAndResult.Panel1
            // 
            this.splitContainerCodeAndResult.Panel1.Controls.Add(this.tabControlPages);
            // 
            // splitContainerCodeAndResult.Panel2
            // 
            this.splitContainerCodeAndResult.Panel2.Controls.Add(this.tabControlResults);
            this.splitContainerCodeAndResult.Size = new System.Drawing.Size(749, 659);
            this.splitContainerCodeAndResult.SplitterDistance = 519;
            this.splitContainerCodeAndResult.TabIndex = 2;
            // 
            // tabControlResults
            // 
            this.tabControlResults.Controls.Add(this.tabPageResults);
            this.tabControlResults.Controls.Add(this.tabPagePlan);
            this.tabControlResults.Controls.Add(this.tabPageOutput);
            this.tabControlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlResults.Location = new System.Drawing.Point(0, 0);
            this.tabControlResults.Name = "tabControlResults";
            this.tabControlResults.SelectedIndex = 0;
            this.tabControlResults.Size = new System.Drawing.Size(749, 136);
            this.tabControlResults.TabIndex = 4;
            // 
            // tabPageResults
            // 
            this.tabPageResults.Controls.Add(this.dataGridSearchDocuments);
            this.tabPageResults.Location = new System.Drawing.Point(4, 22);
            this.tabPageResults.Name = "tabPageResults";
            this.tabPageResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResults.Size = new System.Drawing.Size(741, 110);
            this.tabPageResults.TabIndex = 0;
            this.tabPageResults.Text = "Results";
            this.tabPageResults.UseVisualStyleBackColor = true;
            // 
            // dataGridSearchDocuments
            // 
            this.dataGridSearchDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSearchDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridSearchDocuments.Location = new System.Drawing.Point(3, 3);
            this.dataGridSearchDocuments.Name = "dataGridSearchDocuments";
            this.dataGridSearchDocuments.Size = new System.Drawing.Size(735, 104);
            this.dataGridSearchDocuments.TabIndex = 2;
            // 
            // tabPagePlan
            // 
            this.tabPagePlan.Controls.Add(this.dataGridViewPlan);
            this.tabPagePlan.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlan.Name = "tabPagePlan";
            this.tabPagePlan.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlan.Size = new System.Drawing.Size(741, 110);
            this.tabPagePlan.TabIndex = 1;
            this.tabPagePlan.Text = "Plan";
            this.tabPagePlan.UseVisualStyleBackColor = true;
            // 
            // dataGridViewPlan
            // 
            this.dataGridViewPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnPlanOrdinal,
            this.ColumnOperation,
            this.ColumnIndex,
            this.ColumnCoveredAttributes,
            this.ColumnScannedNodes,
            this.ColumnResultingNodes,
            this.ColumnIntersectedNodes,
            this.ColumnDuration});
            this.dataGridViewPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPlan.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewPlan.Name = "dataGridViewPlan";
            this.dataGridViewPlan.Size = new System.Drawing.Size(735, 104);
            this.dataGridViewPlan.TabIndex = 3;
            // 
            // ColumnPlanOrdinal
            // 
            this.ColumnPlanOrdinal.Frozen = true;
            this.ColumnPlanOrdinal.HeaderText = "Ordinal";
            this.ColumnPlanOrdinal.Name = "ColumnPlanOrdinal";
            this.ColumnPlanOrdinal.ReadOnly = true;
            // 
            // ColumnOperation
            // 
            this.ColumnOperation.Frozen = true;
            this.ColumnOperation.HeaderText = "Operation";
            this.ColumnOperation.Name = "ColumnOperation";
            this.ColumnOperation.ReadOnly = true;
            // 
            // ColumnIndex
            // 
            this.ColumnIndex.HeaderText = "Index";
            this.ColumnIndex.Name = "ColumnIndex";
            this.ColumnIndex.ReadOnly = true;
            // 
            // ColumnCoveredAttributes
            // 
            this.ColumnCoveredAttributes.HeaderText = "Covered Attributes";
            this.ColumnCoveredAttributes.Name = "ColumnCoveredAttributes";
            this.ColumnCoveredAttributes.ReadOnly = true;
            this.ColumnCoveredAttributes.Width = 200;
            // 
            // ColumnScannedNodes
            // 
            this.ColumnScannedNodes.HeaderText = "Scanned Nodes";
            this.ColumnScannedNodes.Name = "ColumnScannedNodes";
            this.ColumnScannedNodes.ReadOnly = true;
            // 
            // ColumnResultingNodes
            // 
            this.ColumnResultingNodes.HeaderText = "Resulting Nodes";
            this.ColumnResultingNodes.Name = "ColumnResultingNodes";
            this.ColumnResultingNodes.ReadOnly = true;
            // 
            // ColumnIntersectedNodes
            // 
            this.ColumnIntersectedNodes.HeaderText = "Intersected Nodes";
            this.ColumnIntersectedNodes.Name = "ColumnIntersectedNodes";
            this.ColumnIntersectedNodes.ReadOnly = true;
            // 
            // ColumnDuration
            // 
            this.ColumnDuration.HeaderText = "Duration (ms)";
            this.ColumnDuration.Name = "ColumnDuration";
            this.ColumnDuration.ReadOnly = true;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.textBoxOutput);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(741, 110);
            this.tabPageOutput.TabIndex = 2;
            this.tabPageOutput.Text = "Output";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOutput.Location = new System.Drawing.Point(3, 3);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(735, 104);
            this.textBoxOutput.TabIndex = 0;
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
            this.splitContainerCodeAndResult.Panel1.ResumeLayout(false);
            this.splitContainerCodeAndResult.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCodeAndResult)).EndInit();
            this.splitContainerCodeAndResult.ResumeLayout(false);
            this.tabControlResults.ResumeLayout(false);
            this.tabPageResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSearchDocuments)).EndInit();
            this.tabPagePlan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlan)).EndInit();
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageOutput.PerformLayout();
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
        private System.Windows.Forms.SplitContainer splitContainerCodeAndResult;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabControl tabControlResults;
        private System.Windows.Forms.TabPage tabPageResults;
        private System.Windows.Forms.DataGridView dataGridSearchDocuments;
        private System.Windows.Forms.TabPage tabPagePlan;
        private System.Windows.Forms.DataGridView dataGridViewPlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPlanOrdinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOperation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCoveredAttributes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnScannedNodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnResultingNodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIntersectedNodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDuration;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.TextBox textBoxOutput;
    }
}

