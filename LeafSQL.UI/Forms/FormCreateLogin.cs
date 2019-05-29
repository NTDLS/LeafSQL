using System;
using System.Windows.Forms;

namespace LeafSQL.UI.Forms
{
    public partial class FormCreateLogin : Form
    {
        public string Username
        {
            get
            {
                return textBoxUsername.Text;
            }
        }
        public string PasswordHash
        {
            get
            {
                return LeafSQL.Library.Utility.HashPassword(textBoxPassword.Text);
            }
        }

        public FormCreateLogin()
        {
            InitializeComponent();

            this.AcceptButton = buttonCreate;
            this.CancelButton = buttonCancel;
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text.Length <= 0)
            {
                MessageBox.Show("You must specify a username.");
                return;
            }

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
    }
}
