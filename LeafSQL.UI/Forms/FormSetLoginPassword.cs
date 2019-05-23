using System;
using System.Windows.Forms;

namespace LeafSQL.UI.Forms
{
    public partial class FormSetLoginPassword : Form
    {
        public string Password
        {
            get
            {
                return textBoxPassword.Text;
            }
        }

        public FormSetLoginPassword()
        {
            InitializeComponent();

            this.AcceptButton = buttonSet;
            this.CancelButton = buttonCancel;
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.Text.Length < 4)
            {
                MessageBox.Show("The password must be at least 4 characters.");
                return;
            }

            if (textBoxPassword.Text != textBoxConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match.");
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
