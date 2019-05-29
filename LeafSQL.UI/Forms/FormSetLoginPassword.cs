using System;
using System.Windows.Forms;

namespace LeafSQL.UI.Forms
{
    public partial class FormSetLoginPassword : Form
    {
        public string PasswordHash
        {
            get
            {
                return LeafSQL.Library.Utility.HashPassword(textBoxPassword.Text);
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
