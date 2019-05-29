using LeafSQL.Library.Client;
using LeafSQL.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeafSQL.UI
{
    public class TabManager
    {
        private TabControl tabControlPages;

        public TabManager(TabControl tabControlPages)
        {
            this.tabControlPages = tabControlPages;
        }

        public TabPage CurrentTab()
        {
            return tabControlPages.SelectedTab;
        }

        public QueryDocument CurrentQueryDocument()
        {
            return tabControlPages?.SelectedTab.Controls.OfType<QueryDocument>().FirstOrDefault();
        }

        public TabPage FindTab(string key)
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

        public bool SelectTab(string key)
        {
            TabPage page = FindTab(key);

            if (page != null)
            {
                tabControlPages.SelectedTab = page;
                return true;
            }

            return false;
        }

        public void AddTab(string key, string text, QueryDocument control)
        {
            TabPage tab = new TabPage(text);
            control.Parent = tab;
            control.Visible = true;
            control.Dock = DockStyle.Fill;
            tab.Name = key;
            tabControlPages.TabPages.Add(tab);
            tabControlPages.SelectedTab = tab;
        }

        public void AddNewTab()
        {
            string fileName = Utility.GetNextFileName();
            AddTab(fileName, fileName, new Controls.QueryDocument());
        }
    }
}
