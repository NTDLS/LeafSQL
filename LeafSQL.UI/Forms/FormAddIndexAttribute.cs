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
    public partial class FormAddIndexAttribute : Form
    {
        public string AttributeName
        {
            get
            {
                return textBoxAttributeName.Text.Trim();
            }
        }

        public FormAddIndexAttribute()
        {
            InitializeComponent();

            this.AcceptButton = buttonOk;
            this.CancelButton = buttonCancel;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (textBoxAttributeName.Text.Trim().Length == 0)
            {
                MessageBox.Show("You must specify an attribute (column) name.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
