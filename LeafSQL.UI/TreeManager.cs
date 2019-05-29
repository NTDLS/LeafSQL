using LeafSQL.Library.Client;
using LeafSQL.Library.Payloads.Models;
using LeafSQL.UI.Properties;
using System.Linq;
using System.Windows.Forms;

namespace LeafSQL.UI
{
    public class TreeManager
    {
        private TreeView treeView;
        private LSTreeNode ServerNode = null;
        private LSTreeNode SchemaNode = null;
        private LSTreeNode LoginsNode = null;
        private ImageList imageListTreeView;

        public TreeManager(TreeView treeView)
        {
            this.treeView = treeView;

            imageListTreeView = new ImageList()
            {
                ColorDepth = ColorDepth.Depth32Bit,
                TransparentColor = System.Drawing.Color.Transparent
            };

            imageListTreeView.Images.Add("Document", Resources.TreeImage_Document);
            imageListTreeView.Images.Add("Documents", Resources.TreeImage_Documents);
            imageListTreeView.Images.Add("Index", Resources.TreeImage_Index);
            imageListTreeView.Images.Add("IndexAttribute", Resources.TreeImage_IndexAttribute);
            imageListTreeView.Images.Add("Indexes", Resources.TreeImage_Indexes);
            imageListTreeView.Images.Add("Login", Resources.TreeImage_Login);
            imageListTreeView.Images.Add("Logins", Resources.TreeImage_Logins);
            imageListTreeView.Images.Add("Schema", Resources.TreeImage_Schema);
            imageListTreeView.Images.Add("Schemas", Resources.TreeImage_Schemas);
            imageListTreeView.Images.Add("Server", Resources.TreeImage_Server);
        }

        public void PopulateSchemas(LeafSQLClient client, LSTreeNode node)
        {
            PopulateSchemas(client, node, false);
        }

        public void PopulateSchemas(LeafSQLClient client, LSTreeNode node, bool populateOneLevelDeeper)
        {
            string SchemaName = GetFullSchemaNameFromNode(node);

            var schemas = client.Schema.List(SchemaName);

            if (node.Nodes.OfType<TreeNode>().FirstOrDefault(o => o.Text == "Indexes") == null)
            {
                LSTreeNode parentIndexesNode = new LSTreeNode(Types.TreeNodeType.Indexes, "Indexes", "Indexes");
                node.Nodes.Add(parentIndexesNode);
                PopulateSchemaIndexes(client, parentIndexesNode);
            }

            foreach (var schema in schemas)
            {
                LSTreeNode SchemaNode = new LSTreeNode(Types.TreeNodeType.Schema, schema.Name, schema.Name);
                LSTreeNode indexesNode = new LSTreeNode(Types.TreeNodeType.Indexes, "Indexes", "Indexes");

                SchemaNode.Nodes.Add(indexesNode);
                node.Nodes.Add(SchemaNode);

                PopulateSchemaIndexes(client, indexesNode);
            }

            if (populateOneLevelDeeper)
            {
                foreach (LSTreeNode subNode in node.Nodes)
                {
                    if (subNode.Type == Types.TreeNodeType.Schema)
                    {
                        PopulateSchemas(client, subNode, false);
                    }
                }
            }
        }

        public void PopulateSchemaIndexes(LeafSQLClient client, LSTreeNode node)
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

        public void PopulateLogins(LeafSQLClient client)
        {
            LoginsNode.Nodes.Clear();

            var logins = client.Security.GetLogins();

            foreach (var login in logins.OrderBy(o => o.Name))
            {
                LoginsNode.Nodes.Add(new LSTreeNode(Types.TreeNodeType.Login, login.Name, login.Id));
            }
        }

        public string GetFullSchemaNameFromNode(LSTreeNode node)
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

        public void PopulateServerExplorer(LeafSQLClient client)
        {
            treeView.Nodes.Clear();
            treeView.ImageList = imageListTreeView;

            var serverSettings = client.Server.Settings.Get();
            var serverVersion = client.Server.Settings.GetVersion();

            ServerNode = new LSTreeNode(Types.TreeNodeType.Server, $"{serverSettings.Name} ({serverVersion.Version})", serverSettings.Name);

            SchemaNode = new LSTreeNode(Types.TreeNodeType.Schemas, "Schemas", "Schemas");
            SchemaNode.Nodes.Add(new LSTreeNode(Types.TreeNodeType.Schema, "<root>", ":"));
            SchemaNode.Expand();
            ServerNode.Nodes.Add(SchemaNode);
            SchemaNode.Nodes[0].Expand();
            PopulateSchemas(client, (LSTreeNode)SchemaNode.Nodes[0], true);

            LoginsNode = new LSTreeNode(Types.TreeNodeType.Logins, "Logins");
            LoginsNode.ImageKey = "Logins";
            LoginsNode.Expand();
            ServerNode.Nodes.Add(LoginsNode);
            PopulateLogins(client);

            treeView.Nodes.Add(ServerNode);
            ServerNode.Expand();
        }

        public bool ContainsNodeOfType(LSTreeNode node, Types.TreeNodeType type)
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
    }
}
