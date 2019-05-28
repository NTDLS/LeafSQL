namespace LeafSQL.UI.Controls
{
    partial class QueryDocuments
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryDocuments));
            NTDLS.Windows.Forms.LineMarginRender lineMarginRender1 = new NTDLS.Windows.Forms.LineMarginRender();
            this.dataGridSearchDocuments = new System.Windows.Forms.DataGridView();
            this.ColumnOrdinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOriginalType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
            this.splitContainerPrimaryHorizontalSplit = new System.Windows.Forms.SplitContainer();
            this.codeEditor = new NTDLS.Windows.Forms.CodeEditorControl();
            this.syntaxDocument = new NTDLS.Syntax.SyntaxDocument(this.components);
            this.tabControlResults = new System.Windows.Forms.TabControl();
            this.tabPageResults = new System.Windows.Forms.TabPage();
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
            this.statusStripResults = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelRowCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDuration = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSearchDocuments)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrimaryHorizontalSplit)).BeginInit();
            this.splitContainerPrimaryHorizontalSplit.Panel1.SuspendLayout();
            this.splitContainerPrimaryHorizontalSplit.Panel2.SuspendLayout();
            this.splitContainerPrimaryHorizontalSplit.SuspendLayout();
            this.tabControlResults.SuspendLayout();
            this.tabPageResults.SuspendLayout();
            this.tabPagePlan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlan)).BeginInit();
            this.tabPageOutput.SuspendLayout();
            this.statusStripResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridSearchDocuments
            // 
            this.dataGridSearchDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSearchDocuments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnOrdinal,
            this.ColumnId,
            this.ColumnOriginalType,
            this.ColumnLength,
            this.ColumnText});
            this.dataGridSearchDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridSearchDocuments.Location = new System.Drawing.Point(3, 3);
            this.dataGridSearchDocuments.Name = "dataGridSearchDocuments";
            this.dataGridSearchDocuments.Size = new System.Drawing.Size(724, 117);
            this.dataGridSearchDocuments.TabIndex = 2;
            // 
            // ColumnOrdinal
            // 
            this.ColumnOrdinal.Frozen = true;
            this.ColumnOrdinal.HeaderText = "Ordinal";
            this.ColumnOrdinal.Name = "ColumnOrdinal";
            this.ColumnOrdinal.ReadOnly = true;
            // 
            // ColumnId
            // 
            this.ColumnId.Frozen = true;
            this.ColumnId.HeaderText = "RID";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.ReadOnly = true;
            // 
            // ColumnOriginalType
            // 
            this.ColumnOriginalType.HeaderText = "Original Type";
            this.ColumnOriginalType.Name = "ColumnOriginalType";
            this.ColumnOriginalType.ReadOnly = true;
            // 
            // ColumnLength
            // 
            this.ColumnLength.HeaderText = "Length";
            this.ColumnLength.Name = "ColumnLength";
            this.ColumnLength.ReadOnly = true;
            // 
            // ColumnText
            // 
            this.ColumnText.HeaderText = "Text";
            this.ColumnText.Name = "ColumnText";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRun,
            this.toolStripButtonPreview});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(738, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonRun
            // 
            this.toolStripButtonRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRun.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRun.Image")));
            this.toolStripButtonRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRun.Name = "toolStripButtonRun";
            this.toolStripButtonRun.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRun.Text = "Run";
            this.toolStripButtonRun.Click += new System.EventHandler(this.toolStripButtonRun_Click);
            // 
            // toolStripButtonPreview
            // 
            this.toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPreview.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPreview.Image")));
            this.toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreview.Name = "toolStripButtonPreview";
            this.toolStripButtonPreview.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPreview.Text = "Preview";
            this.toolStripButtonPreview.Click += new System.EventHandler(this.toolStripButtonPreview_Click);
            // 
            // splitContainerPrimaryHorizontalSplit
            // 
            this.splitContainerPrimaryHorizontalSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPrimaryHorizontalSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerPrimaryHorizontalSplit.Location = new System.Drawing.Point(0, 25);
            this.splitContainerPrimaryHorizontalSplit.Name = "splitContainerPrimaryHorizontalSplit";
            this.splitContainerPrimaryHorizontalSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerPrimaryHorizontalSplit.Panel1
            // 
            this.splitContainerPrimaryHorizontalSplit.Panel1.Controls.Add(this.codeEditor);
            // 
            // splitContainerPrimaryHorizontalSplit.Panel2
            // 
            this.splitContainerPrimaryHorizontalSplit.Panel2.Controls.Add(this.tabControlResults);
            this.splitContainerPrimaryHorizontalSplit.Panel2.Controls.Add(this.statusStripResults);
            this.splitContainerPrimaryHorizontalSplit.Size = new System.Drawing.Size(738, 427);
            this.splitContainerPrimaryHorizontalSplit.SplitterDistance = 252;
            this.splitContainerPrimaryHorizontalSplit.TabIndex = 4;
            // 
            // codeEditor
            // 
            this.codeEditor.ActiveView = NTDLS.Windows.Forms.CodeEditor.ActiveView.BottomRight;
            this.codeEditor.AutoListPosition = null;
            this.codeEditor.AutoListSelectedText = "";
            this.codeEditor.AutoListVisible = false;
            this.codeEditor.CopyAsRTF = false;
            this.codeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeEditor.Document = this.syntaxDocument;
            this.codeEditor.FileName = null;
            this.codeEditor.InfoTipCount = 1;
            this.codeEditor.InfoTipPosition = null;
            this.codeEditor.InfoTipSelectedIndex = 1;
            this.codeEditor.InfoTipVisible = false;
            lineMarginRender1.Bounds = new System.Drawing.Rectangle(19, 0, 19, 16);
            this.codeEditor.LineMarginRender = lineMarginRender1;
            this.codeEditor.Location = new System.Drawing.Point(0, 0);
            this.codeEditor.LockCursorUpdate = false;
            this.codeEditor.Name = "codeEditor";
            this.codeEditor.Saved = false;
            this.codeEditor.ShowScopeIndicator = false;
            this.codeEditor.Size = new System.Drawing.Size(738, 252);
            this.codeEditor.SmoothScroll = false;
            this.codeEditor.SplitviewH = -4;
            this.codeEditor.SplitviewV = -4;
            this.codeEditor.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.codeEditor.TabIndex = 1;
            this.codeEditor.Text = "codeEditorControl1";
            this.codeEditor.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            this.codeEditor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.codeEditor_KeyUp);
            // 
            // syntaxDocument
            // 
            this.syntaxDocument.Lines = new string[] {
        ""};
            this.syntaxDocument.MaxUndoBufferSize = 1000;
            this.syntaxDocument.Modified = true;
            this.syntaxDocument.UndoStep = 0;
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
            this.tabControlResults.Size = new System.Drawing.Size(738, 149);
            this.tabControlResults.TabIndex = 3;
            // 
            // tabPageResults
            // 
            this.tabPageResults.Controls.Add(this.dataGridSearchDocuments);
            this.tabPageResults.Location = new System.Drawing.Point(4, 22);
            this.tabPageResults.Name = "tabPageResults";
            this.tabPageResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResults.Size = new System.Drawing.Size(730, 123);
            this.tabPageResults.TabIndex = 0;
            this.tabPageResults.Text = "Results";
            this.tabPageResults.UseVisualStyleBackColor = true;
            // 
            // tabPagePlan
            // 
            this.tabPagePlan.Controls.Add(this.dataGridViewPlan);
            this.tabPagePlan.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlan.Name = "tabPagePlan";
            this.tabPagePlan.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlan.Size = new System.Drawing.Size(730, 123);
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
            this.dataGridViewPlan.Size = new System.Drawing.Size(724, 117);
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
            this.tabPageOutput.Size = new System.Drawing.Size(730, 123);
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
            this.textBoxOutput.Size = new System.Drawing.Size(724, 117);
            this.textBoxOutput.TabIndex = 0;
            // 
            // statusStripResults
            // 
            this.statusStripResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelRowCount,
            this.toolStripStatusLabelDuration});
            this.statusStripResults.Location = new System.Drawing.Point(0, 149);
            this.statusStripResults.Name = "statusStripResults";
            this.statusStripResults.Size = new System.Drawing.Size(738, 22);
            this.statusStripResults.SizingGrip = false;
            this.statusStripResults.TabIndex = 4;
            // 
            // toolStripStatusLabelRowCount
            // 
            this.toolStripStatusLabelRowCount.Name = "toolStripStatusLabelRowCount";
            this.toolStripStatusLabelRowCount.Size = new System.Drawing.Size(41, 17);
            this.toolStripStatusLabelRowCount.Text = "0 rows";
            // 
            // toolStripStatusLabelDuration
            // 
            this.toolStripStatusLabelDuration.Name = "toolStripStatusLabelDuration";
            this.toolStripStatusLabelDuration.Size = new System.Drawing.Size(29, 17);
            this.toolStripStatusLabelDuration.Text = "0ms";
            // 
            // QueryDocuments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerPrimaryHorizontalSplit);
            this.Controls.Add(this.toolStrip1);
            this.Name = "QueryDocuments";
            this.Size = new System.Drawing.Size(738, 452);
            this.Load += new System.EventHandler(this.QueryDocuments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSearchDocuments)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainerPrimaryHorizontalSplit.Panel1.ResumeLayout(false);
            this.splitContainerPrimaryHorizontalSplit.Panel2.ResumeLayout(false);
            this.splitContainerPrimaryHorizontalSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrimaryHorizontalSplit)).EndInit();
            this.splitContainerPrimaryHorizontalSplit.ResumeLayout(false);
            this.tabControlResults.ResumeLayout(false);
            this.tabPageResults.ResumeLayout(false);
            this.tabPagePlan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlan)).EndInit();
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageOutput.PerformLayout();
            this.statusStripResults.ResumeLayout(false);
            this.statusStripResults.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridSearchDocuments;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRun;
        private System.Windows.Forms.SplitContainer splitContainerPrimaryHorizontalSplit;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreview;
        private System.Windows.Forms.TabControl tabControlResults;
        private System.Windows.Forms.TabPage tabPageResults;
        private System.Windows.Forms.TabPage tabPagePlan;
        private System.Windows.Forms.DataGridView dataGridViewPlan;
        private NTDLS.Windows.Forms.CodeEditorControl codeEditor;
        private NTDLS.Syntax.SyntaxDocument syntaxDocument;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.StatusStrip statusStripResults;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRowCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOrdinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOriginalType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnText;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPlanOrdinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOperation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCoveredAttributes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnScannedNodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnResultingNodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIntersectedNodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDuration;
    }
}
