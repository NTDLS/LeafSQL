using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeafSQL.Library.Payloads;

namespace LeafSQL.UI.Forms
{
    public partial class FormLogin : Form
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
        public string Address
        {
            get
            {
                return textBoxAddress.Text;
            }
        }

        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                DataAccessClient dal = new DataAccessClient();

                dal.Endpoint.Address = new EndpointAddress(textBoxAddress.Text);

                UserSession session = dal.Login(textBoxUsername.Text, textBoxPassword.Text);
                if (session == null)
                {
                    MessageBox.Show("Login failed.");
                    return;
                }

                dal.Logout(session);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.CancelButton = buttonCancel;
            this.AcceptButton = buttonLogin;
        }
    }
}
