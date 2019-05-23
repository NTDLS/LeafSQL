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
    public partial class FormCreateNamespace : Form
    {
        public string NamespaceName { get; set; }

        public FormCreateNamespace()
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
            if (textBoxNamespaceName.Text.Trim().Length == 0)
            {
                MessageBox.Show("You must specify a namespace name.");
                return;
            }

            NamespaceName = textBoxNamespaceName.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormCreateNamespace_Load(object sender, EventArgs e)
        {
            this.AcceptButton = buttonCreate;
            this.CancelButton = buttonCancel;
        }
    }
}
