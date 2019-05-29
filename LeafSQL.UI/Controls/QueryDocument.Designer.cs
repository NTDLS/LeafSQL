namespace LeafSQL.UI.Controls
{
    partial class QueryDocument
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
            NTDLS.Windows.Forms.LineMarginRender lineMarginRender1 = new NTDLS.Windows.Forms.LineMarginRender();
            this.syntaxDocument = new NTDLS.Syntax.SyntaxDocument(this.components);
            this.codeEditor = new NTDLS.Windows.Forms.CodeEditorControl();
            this.SuspendLayout();
            // 
            // syntaxDocument
            // 
            this.syntaxDocument.Lines = new string[] {
        ""};
            this.syntaxDocument.MaxUndoBufferSize = 1000;
            this.syntaxDocument.Modified = true;
            this.syntaxDocument.UndoStep = 0;
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
            this.codeEditor.Size = new System.Drawing.Size(738, 452);
            this.codeEditor.SmoothScroll = false;
            this.codeEditor.SplitviewH = -4;
            this.codeEditor.SplitviewV = -4;
            this.codeEditor.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.codeEditor.TabIndex = 2;
            this.codeEditor.Text = "codeEditorControl1";
            this.codeEditor.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // QueryDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.codeEditor);
            this.Name = "QueryDocument";
            this.Size = new System.Drawing.Size(738, 452);
            this.Load += new System.EventHandler(this.QueryDocuments_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private NTDLS.Syntax.SyntaxDocument syntaxDocument;
        private NTDLS.Windows.Forms.CodeEditorControl codeEditor;
    }
}
