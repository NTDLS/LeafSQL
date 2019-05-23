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
        public string Password
        {
            get
            {
                return textBoxPassword.Text;
            }
        }

        /*
        public Library.ServerRole ServerRole
        {
            get
            {
                return (Library.ServerRole) Enum.Parse(typeof(Library.ServerRole), comboBoxServerRole.Text);
            }
        }
        */

        public FormCreateLogin()
        {
            InitializeComponent();

            this.AcceptButton = buttonCreate;
            this.CancelButton = buttonCancel;
        }

        private void FormCreateLogin_Load(object sender, EventArgs e)
        {
            /*
            foreach (Library.ServerRole role in Enum.GetValues(typeof(Library.ServerRole)))
            {
                comboBoxServerRole.Items.Add(role.ToString());
            }
            */
            comboBoxServerRole.SelectedItem = "None";
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
