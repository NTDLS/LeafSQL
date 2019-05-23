using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeafSQL.Library.Payloads;

namespace LeafSQL.UI.Forms
{
    public partial class FormCreateIndex : Form
    {
        public bool IsUnique
        {
            get
            {
                return checkBoxIsUnique.Checked;
            }
        }

        public string IndexName
        {
            get
            {
                return textBoxIndexName.Text;
            }
        }

        public List<Library.Payloads.IndexAttribute> IndexAttributes
        {
            get
            {
                List<Library.Payloads.IndexAttribute> attributes = new List<Library.Payloads.IndexAttribute>();

                foreach (ListViewItem item in listViewAttributes.Items)
                {
                    attributes.Add(new IndexAttribute()
                    {
                        Name = item.Text
                    });
                }

                return attributes;
            }
        }

        public FormCreateIndex()
        {
            InitializeComponent();
        }

        private void FormCreateIndex_Load(object sender, EventArgs e)
        {
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (textBoxIndexName.Text.Trim().Length == 0)
            {
                MessageBox.Show("You must specify an index name.");
                return;
            }

            if (listViewAttributes.Items.Count == 0)
            {
                MessageBox.Show("You must specify at least one index attribute (column) name.");
                return;
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using (FormAddIndexAttribute form = new FormAddIndexAttribute())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (DoesListContain(form.AttributeName))
                    {
                        MessageBox.Show("Attribute names must be unique.");
                        return;
                    }
                    listViewAttributes.Items.Add(form.AttributeName);
                }
            }
        }

        bool DoesListContain(string attributeName)
        {
            foreach (ListViewItem item in listViewAttributes.Items)
            {
                if (item.Text.ToLower() == attributeName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            if (listViewAttributes.SelectedItems != null && listViewAttributes.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewAttributes.SelectedItems[0];
                if (item != null)
                {
                    if (item.Index > 0)
                    {
                        int index = item.Index - 1;
                        listViewAttributes.Items.RemoveAt(item.Index);
                        listViewAttributes.Items.Insert(index, item);
                    }
                }
            }
        }

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            if (listViewAttributes.SelectedItems != null && listViewAttributes.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewAttributes.SelectedItems[0];
                if (item != null)
                {
                    if (item.Index < listViewAttributes.Items.Count - 1)
                    {
                        int index = item.Index + 1;
                        listViewAttributes.Items.RemoveAt(item.Index);
                        listViewAttributes.Items.Insert(index, item);
                    }
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewAttributes.SelectedItems != null && listViewAttributes.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewAttributes.SelectedItems[0];
                if (item != null)
                {
                    listViewAttributes.Items.Remove(item);
                }
            }
        }
    }
}
