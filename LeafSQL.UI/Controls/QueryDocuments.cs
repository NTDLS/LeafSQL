using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeafSQL.Library.Client;
using LeafSQL.Library.Payloads;
using LeafSQL.UI.Forms;

namespace LeafSQL.UI.Controls
{
    public partial class QueryDocuments : UserControl
    {
        private LeafSQLClient client;
        private string namespaceName = null;
        private int currentPage = 0;

        public QueryDocuments()
        {
            InitializeComponent();
        }

        public QueryDocuments(LeafSQLClient client, string namespaceName)
        {
            InitializeComponent();

            this.client = client;
            this.namespaceName = namespaceName;
        }

        void Preview()
        {
            /*
            DocumentsPagedResult documents = dal.GetAllDocumentsByPage(session, namespaceName, currentPage, 100);

            dataGridSearchDocuments.Rows.Clear();

            foreach (Document document in documents.Collection)
            {
                dataGridSearchDocuments.Rows.Add(
                    new object[] { document.Id,
                        document.OriginalType,
                        document.Bytes.Length,
                        document.Text }
                    );
            }

            dataGridViewPlan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridSearchDocuments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            */
        }

        void DoSearch()
        {
            if (dataGridSearchDocuments.Rows != null && dataGridSearchDocuments.Rows.Count > 0)
            {
                dataGridSearchDocuments.Rows.Clear();
            }
            if (dataGridSearchDocuments.Columns != null && dataGridSearchDocuments.Rows.Count > 0)
            {
                dataGridSearchDocuments.Columns.Clear();
            }
            if (dataGridViewPlan.Rows != null && dataGridViewPlan.Rows.Count > 0)
            {
                dataGridViewPlan.Rows.Clear();
            }

            textBoxOutput.Text = string.Empty;

            string queryText = codeEditor.Document.Text;
            if (codeEditor.Selection != null && codeEditor.Selection.Text.Length > 0)
            {
                queryText = codeEditor.Selection.Text;
            }


            FormProgress.Reset();

            //CancellationTokenSource cancellationToken = new CancellationTokenSource();

            /*
            dal.ExecuteQueryAsync(session, queryText).ContinueWith((t) =>
            {
                FormProgress.WaitForVisible();
                FormProgress.Complete();

                if (t.Status == TaskStatus.RanToCompletion && t.Result != null && t.Result.Success == true)
                {
                    //Success
                    QueryResult documents = t.Result;

                    if (documents != null)
                    {
                        textBoxOutput.Text += documents.RowCount.ToString("N0") + " rows affected." + "\r\n";
                        textBoxOutput.Text += documents.Message == null ? string.Empty : documents.Message + "\r\n";
                        textBoxOutput.Text += documents.Exception == null ? string.Empty : documents.Exception + "\r\n";

                        toolStripStatusLabelDuration.Text = (documents.ExecutionTime.TotalMilliseconds / 1000.0).ToString("N2") + "s";

                        if (documents.Explanation != null)
                        {
                            int ordinal = 0;

                            foreach (PlanExplanationNode explainStep in documents.Explanation.Steps)
                            {
                                dataGridViewPlan.Rows.Add(
                                    new object[] {
                                            ordinal++,
                                            explainStep.Operation,
                                            explainStep.IndexName,
                                            String.Join(", ", explainStep.CoveredAttributes),
                                            explainStep.ScannedNodes,
                                            explainStep.ResultingNodes,
                                            explainStep.IntersectedNodes,
                                            explainStep.Duration.TotalMilliseconds
                                    });
                            }
                        }

                        if (documents.Documents != null && documents.Documents.Count > 0)
                        {
                            toolStripStatusLabelRowCount.Text = documents.Documents.Count.ToString("N0") + " rows";

                            int ordinal = 0;

                            PopulateDefaultResultColumns();

                            foreach (Document document in documents.Documents)
                            {
                                dataGridSearchDocuments.Rows.Add(
                                    new object[] {
                                            ordinal++,
                                            document.Id,
                                            document.OriginalType,
                                            document.Bytes.Length,
                                            document.Text
                                    });
                            }
                        }
                        else if (documents.Columns != null && documents.Columns.Count > 0)
                        {
                            toolStripStatusLabelRowCount.Text = documents.Rows.Count.ToString("N0") + " rows";

                            foreach (string columnName in documents.Columns)
                            {
                                dataGridSearchDocuments.Columns.Add(new DataGridViewTextBoxColumn
                                {
                                    Name = columnName,
                                    HeaderText = columnName,
                                    ReadOnly = true,
                                    Frozen = false
                                });
                            }

                            foreach (List<string> row in documents.Rows)
                            {
                                dataGridSearchDocuments.Rows.Add(row.ToArray());
                            }
                        }

                        if (dataGridSearchDocuments.Rows.Count > 0)
                        {
                            tabControlResults.SelectedTab = tabPageResults;
                        }
                        else if (textBoxOutput.Text.Trim().Length > 0)
                        {
                            tabControlResults.SelectedTab = tabPageOutput;
                        }

                        dataGridViewPlan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                        dataGridSearchDocuments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    }
                }
                else
                {
                    textBoxOutput.Text += "An error occured while processing your request.\r\n";
                    if (t.Result != null)
                    {
                        textBoxOutput.Text += t.Result.Message == null ? string.Empty : t.Result.Message + "\r\n";
                        textBoxOutput.Text += t.Result.Exception == null ? string.Empty : t.Result.Exception + "\r\n";
                    }

                    tabControlResults.SelectedTab = tabPageOutput;
                }
            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

            //FormProgress.Instance.CanCancel = true;

            if (FormProgress.Start("Executing query...") == DialogResult.Cancel)
            {
                //cancellationToken.Cancel();
            }
            */
        }

        private void PopulateDefaultResultColumns()
        {
            dataGridSearchDocuments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Ordinal",
                HeaderText = "Ordinal",
                ReadOnly = true,
                Frozen = true
            });
            dataGridSearchDocuments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                ReadOnly = true,
                Frozen = true
            });
            dataGridSearchDocuments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Data Type",
                HeaderText = "Data Type",
                ReadOnly = true,
                Frozen = false
            });
            dataGridSearchDocuments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Length",
                HeaderText = "Length",
                ReadOnly = true,
                Frozen = false
            });
            dataGridSearchDocuments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Text",
                HeaderText = "Text",
                ReadOnly = true,
                Frozen = false
            });
        }

        private void toolStripButtonRun_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void toolStripButtonPreview_Click(object sender, EventArgs e)
        {
            Preview();
        }

        private void codeEditor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                DoSearch();
            }
        }

        private void QueryDocuments_Load(object sender, EventArgs e)
        {
            codeEditor.Document.SyntaxFile = @"C:\NTDLS\ClosedSource\LeafSQL\Setup\Highlighters\LSQL.syn";
        }
    }
}