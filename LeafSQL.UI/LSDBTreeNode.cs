using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LeafSQL.UI.Types;

namespace LeafSQL.UI
{
    public class LSDBTreeNode: TreeNode
    {
        public object Value { get; set; }
        public TreeNodeType Type { get; set; }

        public LSDBTreeNode(TreeNodeType type, string text, object value)
        {
            this.Type = type;
            this.Text = text;
            this.Value = value;
            this.ImageKey = type.ToString();
        }

        public LSDBTreeNode(TreeNodeType type, string text)
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
