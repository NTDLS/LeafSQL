using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeafSQL.Library.Payloads;
using LeafSQL.Library.Client;

namespace LeafSQL.UI.Controls
{
    public partial class BrowseDocuments : UserControl
    {
        private LeafSQLClient client;
        private string namespaceName = null;
        private int currentPage = 0;
        private int maxPageEncountered = 0;

        public BrowseDocuments()
        {
            InitializeComponent();
        }

        public BrowseDocuments(LeafSQLClient client, string namespaceName)
        {
            InitializeComponent();

            this.client = client;
            this.namespaceName = namespaceName;

            PopulatePage();
        }

        bool PopulatePage()
        {
            /*
            dataGridViewDocuments.Rows.Clear();

            DocumentsPagedResult documents = dal.GetAllDocumentsByPage(session, namespaceName, currentPage, 100);

            foreach (Document document in documents.Collection)
            {
                dataGridViewDocuments.Rows.Add(
                    new object[] { document.Id,
                        document.OriginalType,
                        document.Bytes.Length,
                        document.Text }
                    );
            }

            if (documents.TotalPages > maxPageEncountered)
            {
                maxPageEncountered = documents.TotalPages;
            }

            dataGridViewDocuments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            return documents.IsLastPage;
            */

            return true;
        }

        private void toolStripButtonFirst_Click(object sender, EventArgs e)
        {
            currentPage = 0;
            PopulatePage();
        }

        private void toolStripButtonPreviousPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
            }
            PopulatePage();
        }

        private void toolStripButtonNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage < maxPageEncountered)
            {
                currentPage++;
                if (PopulatePage())
                {
                    currentPage--;
                }
            }
        }

        private void toolStripButtonLast_Click(object sender, EventArgs e)
        {
            currentPage = maxPageEncountered;
            PopulatePage();
        }
    }
}
