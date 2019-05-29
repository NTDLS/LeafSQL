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
        public string TextOrSelection => String.IsNullOrEmpty(SelectedText) ? codeEditor.Selection.Text : Text;

        private void QueryDocuments_Load(object sender, EventArgs e)
        {
            string syntaxtFileName = RegistryHelper.GetRegistryString("", "Path") + "\\IDE\\Highlighters\\LSQL.syn";
            codeEditor.Document.SyntaxFile = syntaxtFileName;

            codeEditor.Document.Text =
            "SELECT TOP 100\r\n"
            + "\tProductID,\r\n"
            + "\tName,\r\n"
            + "\tProductNumber,\r\n"
            + "\tColor,\r\n"
            + "\tSafetyStockLevel\r\n"
            + "FROM\r\n"
            + "\t:AdventureWorks2012:Production:Product\r\n"
            + "WHERE\r\n"
            + "\tcolor = 'Black'\r\n"
            + "\tAND SafetyStockLevel = 500\r\n"
            + "\tAND ProductLine = 'M '\r\n"
            + "\tAND Class = 'L '\r\n";
        }
    }
}