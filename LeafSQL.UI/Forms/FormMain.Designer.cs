namespace LeafSQL.UI.Forms
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.splitContainerPrimaryVerticle = new System.Windows.Forms.SplitContainer();
            this.treeViewDatabase = new System.Windows.Forms.TreeView();
            this.tabControlPages = new System.Windows.Forms.TabControl();
            this.imageListTreeView = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrimaryVerticle)).BeginInit();
            this.splitContainerPrimaryVerticle.Panel1.SuspendLayout();
            this.splitContainerPrimaryVerticle.Panel2.SuspendLayout();
            this.splitContainerPrimaryVerticle.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerPrimaryVerticle
            // 
            this.splitContainerPrimaryVerticle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPrimaryVerticle.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerPrimaryVerticle.Location = new System.Drawing.Point(0, 0);
            this.splitContainerPrimaryVerticle.Name = "splitContainerPrimaryVerticle";
            // 
            // splitContainerPrimaryVerticle.Panel1
            // 
            this.splitContainerPrimaryVerticle.Panel1.Controls.Add(this.treeViewDatabase);
            // 
            // splitContainerPrimaryVerticle.Panel2
            // 
            this.splitContainerPrimaryVerticle.Panel2.Controls.Add(this.tabControlPages);
            this.splitContainerPrimaryVerticle.Size = new System.Drawing.Size(1008, 730);
            this.splitContainerPrimaryVerticle.SplitterDistance = 252;
            this.splitContainerPrimaryVerticle.TabIndex = 0;
            // 
            // treeViewDatabase
            // 
            this.treeViewDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDatabase.Location = new System.Drawing.Point(0, 0);
            this.treeViewDatabase.Name = "treeViewDatabase";
            this.treeViewDatabase.Size = new System.Drawing.Size(252, 730);
            this.treeViewDatabase.TabIndex = 0;
            this.treeViewDatabase.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewDatabase_BeforeExpand);
            this.treeViewDatabase.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewDatabase_NodeMouseClick);
            // 
            // tabControlPages
            // 
            this.tabControlPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPages.Location = new System.Drawing.Point(0, 0);
            this.tabControlPages.Name = "tabControlPages";
            this.tabControlPages.SelectedIndex = 0;
            this.tabControlPages.Size = new System.Drawing.Size(752, 730);
            this.tabControlPages.TabIndex = 0;
            // 
            // imageListTreeView
            // 
            this.imageListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeView.ImageStream")));
            this.imageListTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTreeView.Images.SetKeyName(0, "Server");
            this.imageListTreeView.Images.SetKeyName(1, "Document");
            this.imageListTreeView.Images.SetKeyName(2, "Documents");
            this.imageListTreeView.Images.SetKeyName(3, "Logins");
            this.imageListTreeView.Images.SetKeyName(4, "Schema");
            this.imageListTreeView.Images.SetKeyName(5, "Schemas");
            this.imageListTreeView.Images.SetKeyName(6, "Login");
            this.imageListTreeView.Images.SetKeyName(7, "Index");
            this.imageListTreeView.Images.SetKeyName(8, "Indexes");
            this.imageListTreeView.Images.SetKeyName(9, "IndexAttribute");
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splitContainerPrimaryVerticle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LeafSQL Manager";
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.splitContainerPrimaryVerticle.Panel1.ResumeLayout(false);
            this.splitContainerPrimaryVerticle.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrimaryVerticle)).EndInit();
            this.splitContainerPrimaryVerticle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerPrimaryVerticle;
        private System.Windows.Forms.TreeView treeViewDatabase;
        private System.Windows.Forms.TabControl tabControlPages;
        private System.Windows.Forms.ImageList imageListTreeView;
    }
}

