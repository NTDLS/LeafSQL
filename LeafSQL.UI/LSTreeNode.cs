using System.Windows.Forms;
using static LeafSQL.UI.Types;

namespace LeafSQL.UI
{
    public class LSTreeNode: TreeNode
    {
        public object Value { get; set; }
        public TreeNodeType Type { get; set; }

        public LSTreeNode(TreeNodeType type, string text, object value)
        {
            this.Type = type;
            this.Text = text;
            this.Value = value;
            this.ImageKey = type.ToString();
        }

        public LSTreeNode(TreeNodeType type, string text)
        {
            this.Type = type;
            this.Text = text;
            this.Value = text;
            this.ImageKey = type.ToString();
        }

        public new string ImageKey
        {
            get
            {
                return base.ImageKey;
            }
            set
            {
                base.ImageKey = value;
                base.SelectedImageKey = value;
            }
        }

    }
}
