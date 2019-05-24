using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeafSQL.UI.Forms
{
    public partial class FormCreateSchema : Form
    {
        public string SchemaName { get; set; }

        public FormCreateSchema()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (textBoxSchemaName.Text.Trim().Length == 0)
            {
                MessageBox.Show("You must specify a schema name.");
                return;
            }

            SchemaName = textBoxSchemaName.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormCreateSchema_Load(object sender, EventArgs e)
        {
            this.AcceptButton = buttonCreate;
            this.CancelButton = buttonCancel;
        }
    }
}
