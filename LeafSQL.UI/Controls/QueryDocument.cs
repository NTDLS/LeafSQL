using LeafSQL.UI.Properties;
using NTDLS.Windows.Forms;
using System;
using System.Windows.Forms;

namespace LeafSQL.UI.Controls
{
    public partial class QueryDocument : UserControl
    {
        public QueryDocument()
        {
            InitializeComponent();
        }

        public CodeEditorControl Editor => codeEditor;
        public override string Text => codeEditor.Document.Text;
        public string SelectedText => codeEditor.Selection?.Text;

        /// <summary>
        /// Returns the selected text, or if no text is selected returns the entire text.
        /// </summary>
        public string TextOrSelection => String.IsNullOrEmpty(SelectedText) ? Text : codeEditor.Selection.Text;

        private void QueryDocuments_Load(object sender, EventArgs e)
        {
            string syntaxtFileName = RegistryHelper.GetRegistryString("", "Path") + "\\IDE\\Highlighters\\LSQL.syn";
            codeEditor.Document.SyntaxFile = syntaxtFileName;

#if DEBUG
            codeEditor.Document.Text = Resources.DebugSQL;
#endif
        }
    }
}