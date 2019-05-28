using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeafSQL.Library;
using LeafSQL.Library.Client;
using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Models;
using LeafSQL.UI;
using LeafSQL.UI.Forms;

namespace LeafSQL.UI.Forms
{
    public partial class FormMain : Form
    {
        private TabManager tabManager;

        private bool isInitializing = true;
        private LeafSQLClient client;
        private LSTreeNode contextNode = null;
        private LSTreeNode ServerNode = null;
        private LSTreeNode SchemaNode = null;
        private LSTreeNode LoginsNode = null;

        public FormMain()
        {
            InitializeComponent();
            tabManager = new TabManager(tabControlPages);
        }

        bool ContainsNodeOfType(LSTreeNode node, Types.TreeNodeType type)
        {
            foreach (LSTreeNode n in node.Nodes)
            {
                if (n.Type == type)
                {
                    return true;
                }
            }
            return false;
        }

        #region Events.

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
                            if (ContainsNodeOfType(node, Types.TreeNodeType.Schema) == false)
                            {
                                PopulateSchemas(node);
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
            string schemaName = GetFullSchemaNameFromNode(contextNode);

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

                                    PopulateSchemaIndexes(contextNode);

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

            string SchemaName = GetFullSchemaNameFromNode(contextNode);

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

            string schemaName = GetFullSchemaNameFromNode(contextNode);

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
            /*
            using (FormSetLoginPassword form = new FormSetLoginPassword())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {

                    client.SetLoginPasswordByIdAsync((Guid)contextNode.Value, form.Password).ContinueWith((t) =>
                    {
                        FormProgress.WaitForVisible();
                        FormProgress.Complete();

                        if (t.Status == TaskStatus.RanToCompletion && t.Result != null && t.Result.Success == true)
                        {
                            //Success
                        }
                        else
                        {
                            Program.AsyncResultMessage(t.Result, "An error occured while processing your request.");
                        }
                    }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                    FormProgress.Start("Setting password for [" + contextNode.Text + "]...");
                }
            }
            */
        }

        private void ContextMenu_CreateLogin(object sender, EventArgs e)
        {
            /*
            using (FormCreateLogin form = new FormCreateLogin())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    client.CreateLoginAsync(new Login
                        {
                            UserName = form.Username,
                            Password = form.Password,
                            ServerRole = form.ServerRole
                        }).ContinueWith((t) =>
                    {
                        FormProgress.WaitForVisible();
                        FormProgress.Complete();

                        if (t.Status == TaskStatus.RanToCompletion && t.Result != null && t.Result.Success == true)
                        {
                            //Success
                            PopulateLogins();
                        }
                        else
                        {
                            Program.AsyncResultMessage(t.Result, "An error occured while processing your request.");
                        }
                    }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                    FormProgress.Start("Creating login [" + form.Username + "]...");
                }
            }
            */
        }

        private void ContextMenu_DeleteLogin(object sender, EventArgs e)
        {
            /*
            if (MessageBox.Show("Are you sure you want to delete the login [" + contextNode.Text + "]?",
                "Delete Login?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            client.DeleteLoginByIdAsync((Guid)contextNode.Value).ContinueWith((t) =>
                {
                    FormProgress.WaitForVisible();
                    FormProgress.Complete();

                    if (t.Status == TaskStatus.RanToCompletion && t.Result != null && t.Result.Success == true)
                    {
                        //Success
                        contextNode.Remove();
                    }
                    else
                    {
                        Program.AsyncResultMessage(t.Result, "An error occured while processing your request.");
                    }
                }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

            FormProgress.Start("Deleting login [" + contextNode.Text + "]...");
            */
        }

        private void ContextMenu_CreateSchema(object sender, EventArgs e)
        {
            using (FormCreateSchema form = new FormCreateSchema())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string SchemaName = GetFullSchemaNameFromNode(contextNode) + ":" + form.SchemaName;

                    client.Schema.CreateAsync(SchemaName).ContinueWith((t) =>
                    {
                        FormProgress.WaitForVisible();
                        FormProgress.Complete();

                        if (t.Status == TaskStatus.RanToCompletion)
                        {
                            contextNode.Nodes.Clear();
                            PopulateSchemas(contextNode, true);
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

            PopulateServerExplorer();

            isInitializing = false;
        }

        #endregion

        #region Tree Population.

        void PopulateSchemas(LSTreeNode node)
        {
            PopulateSchemas(node, false);
        }

        void PopulateSchemas(LSTreeNode node, bool populateOneLevelDeeper)
        {
            string SchemaName = GetFullSchemaNameFromNode(node);

            var schemas = client.Schema.List(SchemaName);

            if (node.Nodes.OfType<TreeNode>().FirstOrDefault(o => o.Text == "Indexes") == null)
            {
                LSTreeNode parentIndexesNode = new LSTreeNode(Types.TreeNodeType.Indexes, "Indexes", "Indexes");
                node.Nodes.Add(parentIndexesNode);
                PopulateSchemaIndexes(parentIndexesNode);
            }

            foreach (var schema in schemas)
            {
                LSTreeNode SchemaNode = new LSTreeNode(Types.TreeNodeType.Schema, schema.Name, schema.Name);
                LSTreeNode indexesNode = new LSTreeNode(Types.TreeNodeType.Indexes, "Indexes", "Indexes");

                SchemaNode.Nodes.Add(indexesNode);
                node.Nodes.Add(SchemaNode);

                PopulateSchemaIndexes(indexesNode);
            }

            if (populateOneLevelDeeper)
            {
                foreach (LSTreeNode subNode in node.Nodes)
                {
                    if (subNode.Type == Types.TreeNodeType.Schema)
                    {
                        PopulateSchemas(subNode, false);
                    }
                }
            }
        }

        void PopulateSchemaIndexes(LSTreeNode node)
        {
            node.Nodes.Clear();

            string SchemaName = GetFullSchemaNameFromNode(node);

            var indexes = client.Schema.Indexes.List(SchemaName);

            foreach (Index index in indexes.OrderBy(o => o.Name))
            {
                var indexNode = new LSTreeNode(Types.TreeNodeType.Index, index.Name);

                foreach (IndexAttribute attribute in index.Attributes)
                {
                    indexNode.Nodes.Add(new LSTreeNode(Types.TreeNodeType.IndexAttribute, attribute.Name));
                }

                node.Nodes.Add(indexNode);
            }
        }


        void PopulateLogins()
        {
            LoginsNode.Nodes.Clear();

            var logins = client.Security.GetLogins();

            foreach (var login in logins.OrderBy(o => o.Name))
            {
                LoginsNode.Nodes.Add(new LSTreeNode(Types.TreeNodeType.Login, login.Name, login.Id));
            }
        }

        string GetFullSchemaNameFromNode(LSTreeNode node)
        {
            string schemaName = string.Empty;

            if (node.Type == Types.TreeNodeType.Schema)
            {
                schemaName = node.Value.ToString();
            }

            LSTreeNode current = (LSTreeNode)node.Parent;

            while (current.Type != Types.TreeNodeType.Schemas)
            {
                if (current.Type == Types.TreeNodeType.Schema)
                {
                    if (current.Text == "<root>")
                    {
                        schemaName = ":" + schemaName;
                    }
                    else
                    {
                        schemaName = current.Value.ToString() + ":" + schemaName;
                    }
                }
                current = (LSTreeNode)current.Parent;
            }

            schemaName = schemaName.Replace("::", ":");

            if (schemaName.Length > 1 && schemaName.EndsWith(":"))
            {
                schemaName = schemaName.Substring(0, schemaName.Length - 1);
            }

            return schemaName;
        }

        private void PopulateServerExplorer()
        {
            treeViewDatabase.Nodes.Clear();

            treeViewDatabase.ImageList = imageListTreeView;

            var serverSettings = client.Server.Settings.Get();
            var serverVersion = client.Server.Settings.GetVersion();

            ServerNode = new LSTreeNode(Types.TreeNodeType.Server, $"{serverSettings.Name} ({serverVersion.Version})", serverSettings.Name);

            SchemaNode = new LSTreeNode(Types.TreeNodeType.Schemas, "Schemas", "Schemas");
            SchemaNode.Nodes.Add(new LSTreeNode(Types.TreeNodeType.Schema, "<root>", ":"));
            SchemaNode.Expand();
            ServerNode.Nodes.Add(SchemaNode);
            SchemaNode.Nodes[0].Expand();
            PopulateSchemas((LSTreeNode)SchemaNode.Nodes[0], true);

            LoginsNode = new LSTreeNode(Types.TreeNodeType.Logins, "Logins");
            LoginsNode.ImageKey = "Logins";
            LoginsNode.Expand();
            ServerNode.Nodes.Add(LoginsNode);
            PopulateLogins();

            treeViewDatabase.Nodes.Add(ServerNode);
            ServerNode.Expand();
        }

        #endregion

        #region Toolstrip.

        private void CmdNewFile_Click(object sender, EventArgs e)
        {
            tabManager.AddNewTab();
        }

        #endregion

        #region Menu.

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
