using LeafSQL.Library.Client;
using LeafSQL.Library.Payloads.Models;
using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeafSQL.UI.Forms
{
    public partial class FormMain : Form
    {
        private TabManager tabManager;
        private TreeManager treeManager;
        private LSTreeNode contextNode = null;

        private bool isInitializing = true;
        private LeafSQLClient client;

        public FormMain()
        {
            InitializeComponent();

            tabManager = new TabManager(tabControlPages);
            treeManager = new TreeManager(treeViewDatabase);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            using (var formLogin = new FormLogin())
            {
                if (formLogin.ShowDialog() == DialogResult.OK)
                {
                    client = new LeafSQLClient(formLogin.Address, formLogin.Username, formLogin.Password);
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
            }

            treeManager.PopulateServerExplorer(client);

            tabManager.AddNewTab();

            isInitializing = false;
        }

        #region Tree Context Menu Events.

        private void treeViewDatabase_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (isInitializing)
            {
                return;
            }

            if (e.Node != null)
            {
                LSTreeNode selectedNode = (LSTreeNode)e.Node;

                if (selectedNode.Type == Types.TreeNodeType.Schema || selectedNode.Type == Types.TreeNodeType.Schemas)
                {
                    foreach (LSTreeNode node in selectedNode.Nodes)
                    {
                        if (node.Type == Types.TreeNodeType.Schema || node.Type == Types.TreeNodeType.Schemas)
                        {
                            if (treeManager.ContainsNodeOfType(node, Types.TreeNodeType.Schema) == false)
                            {
                                treeManager.PopulateSchemas(client, node);
                            }
                        }
                    }
                }
            }
        }

        private void treeViewDatabase_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ContextMenu menu = new ContextMenu();

            contextNode = (LSTreeNode)e.Node;

            if (e.Button == MouseButtons.Right)
            {
                if (contextNode.Type == Types.TreeNodeType.Schema)
                {
                    menu.MenuItems.Add("Create Schema", ContextMenu_CreateSchema);

                    if (contextNode.Text != "/")
                    {
                        menu.MenuItems.Add("Browse", ContextMenu_BrowseDocuments);
                        menu.MenuItems.Add("Query", ContextMenu_QueryDocuments);
                    }
                    menu.Show(treeViewDatabase, e.Location);
                }
                else if (contextNode.Type == Types.TreeNodeType.Login)
                {
                    menu.MenuItems.Add("Set Password", ContextMenu_SetLoginPassword);
                    menu.MenuItems.Add("Delete Login", ContextMenu_DeleteLogin);
                    menu.Show(treeViewDatabase, e.Location);
                }
                else if (contextNode.Type == Types.TreeNodeType.Indexes)
                {
                    menu.MenuItems.Add("Create Index", ContextMenu_CreateIndex);
                    menu.Show(treeViewDatabase, e.Location);
                }
                else if (contextNode.Type == Types.TreeNodeType.Index)
                {
                    menu.MenuItems.Add("Rebuild", ContextMenu_RebuildIndex);
                    menu.MenuItems.Add("Delete Index", ContextMenu_DeleteIndex);
                    menu.Show(treeViewDatabase, e.Location);
                }
                else if (contextNode.Type == Types.TreeNodeType.Logins)
                {
                    menu.MenuItems.Add("Create Login", ContextMenu_CreateLogin);
                    menu.Show(treeViewDatabase, e.Location);
                }
            }
        }

        private void ContextMenu_CreateIndex(object sender, EventArgs e)
        {
            string schemaName = treeManager.GetFullSchemaNameFromNode(contextNode);

            using (FormCreateIndex form = new FormCreateIndex())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    client.Schema.Indexes.CreateAsync(schemaName,
                                new Index
                                {
                                    Name = form.IndexName,
                                    IsUnique = form.IsUnique,
                                    Attributes = form.IndexAttributes
                                }).ContinueWith((t) =>
                                {
                                    FormProgress.WaitForVisible();
                                    FormProgress.Complete();

                                    if (t.Status != TaskStatus.RanToCompletion)
                                    {
                                        Program.AsyncExceptionMessage(t, "An error occured while processing your request.");
                                    }

                                    treeManager.PopulateSchemaIndexes(client, contextNode);

                                }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                    FormProgress.Start("Creating index [" + form.IndexName + "]...");
                }
            }
        }

        private void ContextMenu_RebuildIndex(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to rebuild the index [" + contextNode.Text + "]?",
                "Rebuild Index?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string SchemaName = treeManager.GetFullSchemaNameFromNode(contextNode);

            client.Schema.Indexes.RebuildAsync(SchemaName, contextNode.Value.ToString()).ContinueWith((t) =>
                        {
                            FormProgress.WaitForVisible();
                            FormProgress.Complete();

                            if (t.Status != TaskStatus.RanToCompletion)
                            {
                                Program.AsyncExceptionMessage(t, "An error occured while processing your request.");
                            }
                        }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

            FormProgress.Start("Rebuilding index [" + contextNode.Text.ToString() + "]...");
        }

        private void ContextMenu_DeleteIndex(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the index [" + contextNode.Text + "]?",
                "Delete Index?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string schemaName = treeManager.GetFullSchemaNameFromNode(contextNode);

            client.Schema.Indexes.DeleteByNameAsync(schemaName, contextNode.Value.ToString()).ContinueWith((t) =>
            {
                FormProgress.WaitForVisible();
                FormProgress.Complete();

                if (t.Status == TaskStatus.RanToCompletion)
                {
                    contextNode.Remove();
                }
                else
                {
                    Program.AsyncExceptionMessage(t, "An error occured while processing your request.");
                }

            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

            FormProgress.Start("Deleting index [" + contextNode.Text + "]...");
        }

        private void ContextMenu_BrowseDocuments(object sender, EventArgs e)
        {
            /*
            string SchemaName = GetFullSchemaNameFromNode(contextNode);

            string key = String.Format("BrowseDocuments_{0}", SchemaName);

            if (SelectTab(key) == false)
            {
                AddNewTab(key, contextNode.Text, new Controls.BrowseDocuments(client, SchemaName));
            }
            */
        }

        private void ContextMenu_QueryDocuments(object sender, EventArgs e)
        {
            /*
            string SchemaName = GetFullSchemaNameFromNode(contextNode);

            string key = String.Format("QueryDocuments_{0}", SchemaName);

            if (SelectTab(key) == false)
            {
                AddNewTab(key, contextNode.Text, new Controls.QueryDocuments(client, SchemaName));
            }
            */
        }

        private void ContextMenu_SetLoginPassword(object sender, EventArgs e)
        {
            using (FormSetLoginPassword form = new FormSetLoginPassword())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    client.Security.SetLoginPasswordAsync(contextNode.Text, form.PasswordHash).ContinueWith((t) =>
                    {
                        FormProgress.WaitForVisible();
                        FormProgress.Complete();

                        if (t.Status != TaskStatus.RanToCompletion)
                        {
                            Program.AsyncExceptionMessage(t, "An error occured while processing your request.");
                        }
                    }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                    FormProgress.Start("Setting password for [" + contextNode.Text + "]...");
                }
            }
        }

        private void ContextMenu_CreateLogin(object sender, EventArgs e)
        {
            using (FormCreateLogin form = new FormCreateLogin())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    client.Security.CreateLoginAsync(form.Username, form.PasswordHash).ContinueWith((t) =>
                                {
                                    FormProgress.WaitForVisible();
                                    FormProgress.Complete();

                                    if (t.Status != TaskStatus.RanToCompletion)
                                    {
                                        Program.AsyncExceptionMessage(t, "An error occured while processing your request.");
                                    }

                                    treeManager.PopulateLogins(client);

                                }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                    FormProgress.Start("Creating login [" + form.Username + "]...");
                }
            }
        }

        private void ContextMenu_DeleteLogin(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the login [" + contextNode.Text + "]?",
                "Delete Login?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            client.Security.DeleteLoginByNameAsync(contextNode.Text).ContinueWith((t) =>
            {
                FormProgress.WaitForVisible();
                FormProgress.Complete();

                if (t.Status != TaskStatus.RanToCompletion)
                {
                    Program.AsyncExceptionMessage(t, "An error occured while processing your request.");
                }

                treeManager.PopulateLogins(client);

            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

            FormProgress.Start("Deleting login [" + contextNode.Text + "]...");


        }

        private void ContextMenu_CreateSchema(object sender, EventArgs e)
        {
            using (FormCreateSchema form = new FormCreateSchema())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string SchemaName = treeManager.GetFullSchemaNameFromNode(contextNode) + ":" + form.SchemaName;

                    client.Schema.CreateAsync(SchemaName).ContinueWith((t) =>
                    {
                        FormProgress.WaitForVisible();
                        FormProgress.Complete();

                        if (t.Status == TaskStatus.RanToCompletion)
                        {
                            contextNode.Nodes.Clear();
                            treeManager.PopulateSchemas(client, contextNode, true);
                        }
                        else
                        {
                            Program.AsyncExceptionMessage(t, "An error occured while processing your request.");
                        }

                    }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                    FormProgress.Start("Creating Schema [" + SchemaName + "]...");
                }
            }
        }

        #endregion

        #region Menu.

        private void ExecuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteCurrentDocument();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Toolbar.

        private void CmdNewFile_Click(object sender, EventArgs e)
        {
            tabManager.AddNewTab();
        }

        private void CmdExecute_Click(object sender, EventArgs e)
        {
            ExecuteCurrentDocument();
        }

        #endregion

        #region Execution.

        private void ExecuteCurrentDocument()
        {
            var queryDocument = tabManager.CurrentQueryDocument();
            string queryText = queryDocument.TextOrSelection?.Trim();

            if (String.IsNullOrWhiteSpace(queryText))
            {
                return;
            }

            client.Query.ExecuteQueryAsync(queryText).ContinueWith((t) =>
            {
                FormProgress.WaitForVisible();
                FormProgress.Complete();

                if (t.Status == TaskStatus.RanToCompletion)
                {
                    PopulateFormFromResults(t.Result);
                }
                else
                {
                    PopulateException(t.Exception);
                }
            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

            //FormProgress.Instance.CanCancel = true;

            if (FormProgress.Start("Executing query...") == DialogResult.Cancel)
            {
                //cancellationToken.Cancel();
            }
        }

        private void PopulateException(AggregateException ex)
        {
            StringBuilder text = new StringBuilder();
            foreach (var exception in ex.InnerExceptions)
            {
                text.AppendLine(exception.Message);
            }

            textBoxOutput.Text = text.ToString();
            tabControlResults.SelectedTab = tabPageOutput;
        }

        private void PopulateFormFromResults(QueryResult queryResult)
        {
            ClearResults();

            tabControlResults.SelectedTab = tabPageResults;

            foreach (var column in queryResult.Columns)
            {
                dataGridSearchDocuments.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = column.Name,
                    HeaderText = column.Name,
                    ReadOnly = true,
                    Frozen = false
                });
            }

            foreach (var row in queryResult.Rows)
            {
                dataGridSearchDocuments.Rows.Add(row.Values.ToArray());
            }
        }

        private void ClearResults()
        {
            if (dataGridSearchDocuments.Rows != null)
            {
                dataGridSearchDocuments.Rows.Clear();
            }
            if (dataGridSearchDocuments.Columns != null)
            {
                dataGridSearchDocuments.Columns.Clear();
            }
            if (dataGridViewPlan.Rows != null)
            {
                dataGridViewPlan.Rows.Clear();
            }
            textBoxOutput.Text = string.Empty;
        }

        #endregion
    }
}
