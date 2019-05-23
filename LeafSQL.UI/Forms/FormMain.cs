﻿using System;
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
using LeafSQL.UI;
using LeafSQL.UI.Forms;

namespace LeafSQL.UI.Forms
{
    public partial class FormMain : Form
    {
        bool isInitializing = true;

        LeafSQLClient client;

        private LSDBTreeNode contextNode = null;
        //private LSDBTreeNode ServerNode = null;
        //private LSDBTreeNode NamespacesNode = null;
        //private LSDBTreeNode LoginsNode = null;

        bool ContainsNodeOfType(LSDBTreeNode node, Types.TreeNodeType type)
        {
            foreach (LSDBTreeNode n in node.Nodes)
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
                LSDBTreeNode selectedNode = (LSDBTreeNode)e.Node;

                if (selectedNode.Type == Types.TreeNodeType.Namespace || selectedNode.Type == Types.TreeNodeType.Namespaces)
                {
                    foreach (LSDBTreeNode node in selectedNode.Nodes)
                    {
                        if (node.Type == Types.TreeNodeType.Namespace || node.Type == Types.TreeNodeType.Namespaces)
                        {
                            if (ContainsNodeOfType(node, Types.TreeNodeType.Namespace) == false)
                            {
                                PopulateNamespaces(node);
                            }
                        }
                    }
                }
            }
        }
        private void treeViewDatabase_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ContextMenu menu = new ContextMenu();

            contextNode = (LSDBTreeNode)e.Node;

            if (e.Button == MouseButtons.Right)
            {
                if (contextNode.Type == Types.TreeNodeType.Namespace)
                {
                    menu.MenuItems.Add("Create Namespace", ContextMenu_CreateNamespace);

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
            /*
            string namespaceName = GetFullNamespaceNameFromNode(contextNode);

            using (FormCreateIndex form = new FormCreateIndex())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    client.CreateNamespaceIndexAsync(namespaceName,
                                new Index
                                {
                                    Name = form.IndexName,
                                    AllowNulls = form.AllowNulls,
                                    IndexType = form.IndexType,
                                    Attributes = form.IndexAttributes
                                }).ContinueWith((t) =>
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

                        PopulateNamespaceIndexes(contextNode);

                    }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                    FormProgress.Start("Creating index [" + form.IndexName + "]...");
                }
            }
            */
        }

        private void ContextMenu_RebuildIndex(object sender, EventArgs e)
        {
            /*
            if (MessageBox.Show("Are you sure you want to rebuild the index [" + contextNode.Text + "]?",
                "Rebuild Index?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string namespaceName = GetFullNamespaceNameFromNode(contextNode);

            client.RebuildIndexAsync(namespaceName, contextNode.Value.ToString()).ContinueWith((t) =>
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

            FormProgress.Start("Rebuilding index [" + contextNode.Text.ToString() + "]...");
            */
        }

        private void ContextMenu_DeleteIndex(object sender, EventArgs e)
        {
            /*
            if (MessageBox.Show("Are you sure you want to delete the index [" + contextNode.Text + "]?",
                "Delete Index?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string namespaceName = GetFullNamespaceNameFromNode(contextNode);

            client.DropNamespaceIndexAsync(namespaceName, contextNode.Value.ToString()).ContinueWith((t) =>
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

            FormProgress.Start("Deleting index [" + contextNode.Text + "]...");
            */
        }

        private void ContextMenu_BrowseDocuments(object sender, EventArgs e)
        {
            /*
            string namespaceName = GetFullNamespaceNameFromNode(contextNode);

            string key = String.Format("BrowseDocuments_{0}", namespaceName);

            if (SelectTab(key) == false)
            {
                AddNewTab(key, contextNode.Text, new Controls.BrowseDocuments(client, namespaceName));
            }
            */
        }

        private void ContextMenu_QueryDocuments(object sender, EventArgs e)
        {
            /*
            string namespaceName = GetFullNamespaceNameFromNode(contextNode);

            string key = String.Format("QueryDocuments_{0}", namespaceName);

            if (SelectTab(key) == false)
            {
                AddNewTab(key, contextNode.Text, new Controls.QueryDocuments(client, namespaceName));
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

        private void ContextMenu_CreateNamespace(object sender, EventArgs e)
        {
            /*
            using (FormCreateNamespace form = new FormCreateNamespace())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string namespaceName = GetFullNamespaceNameFromNode(contextNode) + "/" + form.NamespaceName;

                    client.CreateNamespaceAsync(namespaceName).ContinueWith((t) =>
                    {
                        FormProgress.WaitForVisible();
                        FormProgress.Complete();

                        if (t.Status == TaskStatus.RanToCompletion && t.Result != null && t.Result.Success == true)
                        {
                            //Success
                            contextNode.Nodes.Clear();
                            PopulateNamespaces(contextNode, true);
                        }
                        else
                        {
                            Program.AsyncResultMessage(t.Result, "An error occured while processing your request.");
                        }

                    }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                    FormProgress.Start("Creating namespace [" + namespaceName + "]...");
                }
            }
            */
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

        #region Tabs.

        private TabPage FindTab(string key)
        {
            foreach (TabPage tab in tabControlPages.TabPages)
            {
                if (tab.Name == key)
                {
                    return tab;
                }
            }
            return null;
        }

        private bool SelectTab(string key)
        {
            TabPage page = FindTab(key);

            if (page != null)
            {
                tabControlPages.SelectedTab = page;
                return true;
            }

            return false;
        }

        private void AddNewTab(string key, string text, Control control)
        {
            TabPage tab = new TabPage(text);
            control.Parent = tab;
            control.Visible = true;
            control.Dock = DockStyle.Fill;
            tab.Name = key;
            tabControlPages.TabPages.Add(tab);
            tabControlPages.SelectedTab = tab;
        }

        #endregion

        public FormMain()
        {
            InitializeComponent();
        }

        void PopulateNamespaces(LSDBTreeNode node)
        {
            PopulateNamespaces(node, false);
        }

        void PopulateNamespaces(LSDBTreeNode node, bool populateOneLevelDeeper)
        {
            /*
            string namespaceName = GetFullNamespaceNameFromNode(node);

            NamespaceResult result = client.GetNamespaces(namespaceName);

            foreach (var ns in result.Collection)
            {
                LSDBTreeNode namespaceNode = new LSDBTreeNode(Types.TreeNodeType.Namespace, ns.Name, ns.Name);
                LSDBTreeNode indexesNode = new LSDBTreeNode(Types.TreeNodeType.Indexes, "Indexes", "Indexes");

                namespaceNode.Nodes.Add(indexesNode);
                node.Nodes.Add(namespaceNode);

                PopulateNamespaceIndexes(indexesNode);
            }

            if (populateOneLevelDeeper)
            {
                foreach (LSDBTreeNode subNode in node.Nodes)
                {
                    if (subNode.Type == Types.TreeNodeType.Namespace)
                    {
                        PopulateNamespaces(subNode, false);
                    }
                }
            }
            */
        }

        void PopulateNamespaceIndexes(LSDBTreeNode node)
        {
            /*
            node.Nodes.Clear();

            string namespaceName = GetFullNamespaceNameFromNode(node);

            IndexsResult indexes = client.GetNamespaceIndexes(namespaceName);
            foreach (Index index in indexes.Collection.OrderBy(o => o.Name))
            {
                LSDBTreeNode indexNode = new LSDBTreeNode(Types.TreeNodeType.Index, index.Name);

                foreach (IndexAttribute attribute in index.Attributes)
                {
                    indexNode.Nodes.Add( new LSDBTreeNode(Types.TreeNodeType.IndexAttribute, attribute.Name));
                }

                node.Nodes.Add(indexNode);
            }
            */
        }

        void PopulateLogins()
        {
            /*
            LoginsResult result = client.GetLogins();

            LoginsNode.Nodes.Clear();

            foreach (var login in result.Collection.OrderBy(o=> o.UserName))
            {
                LoginsNode.Nodes.Add(new LSDBTreeNode(Types.TreeNodeType.Login, login.UserName, login.UserId));
            }
            */
        }

        string GetFullNamespaceNameFromNode(LSDBTreeNode node)
        {
            string namespaceName = string.Empty;

            if (node.Type == Types.TreeNodeType.Namespace)
            {
                namespaceName = node.Value.ToString();
            }

            LSDBTreeNode current = (LSDBTreeNode)node.Parent;

            while (current.Type != Types.TreeNodeType.Namespaces)
            {
                if (current.Type == Types.TreeNodeType.Namespace)
                {
                    if (current.Text == "/")
                    {
                        namespaceName = "/" + namespaceName;
                    }
                    else
                    {
                        namespaceName = current.Value.ToString() + "/" + namespaceName;
                    }
                }
                current = (LSDBTreeNode)current.Parent;
            }

            return namespaceName;
        }

        private void PopulateServerExplorer()
        {
            /*
            treeViewDatabase.Nodes.Clear();

            treeViewDatabase.ImageList = imageListTreeView;

            ServerInfo serverInfo = client.GetServerInfo();

            ServerNode = new LSDBTreeNode(Types.TreeNodeType.Server, serverInfo.NickName, serverInfo.NickName);

            NamespacesNode = new LSDBTreeNode(Types.TreeNodeType.Namespaces, "Namespaces", "Namespaces");
            NamespacesNode.Nodes.Add(new LSDBTreeNode(Types.TreeNodeType.Namespace, "/", "/"));
            NamespacesNode.Expand();
            ServerNode.Nodes.Add(NamespacesNode);
            NamespacesNode.Nodes[0].Expand();
            PopulateNamespaces((LSDBTreeNode)NamespacesNode.Nodes[0], true);

            LoginsNode = new LSDBTreeNode(Types.TreeNodeType.Logins, "Logins");
            LoginsNode.ImageKey = "Logins";
            LoginsNode.Expand();
            ServerNode.Nodes.Add(LoginsNode);
            PopulateLogins();

            treeViewDatabase.Nodes.Add(ServerNode);
            ServerNode.Expand();
            */
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}