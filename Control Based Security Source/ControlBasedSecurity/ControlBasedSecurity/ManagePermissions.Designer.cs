namespace ControlBasedSecurity
{
    partial class ManagePermissions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
                components.Dispose();
                components.Dispose();
            }
            base.Dispose( disposing );
           
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ByRoleRB = new System.Windows.Forms.RadioButton();
            this.ByControlRB = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.PermissionTree = new System.Windows.Forms.TreeView();
            this.PermissionRoles = new System.Windows.Forms.ListBox();
            this.rolesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.controlSecurityDataSet = new ControlBasedSecurity.ControlSecurityDataSet();
            this.Save = new System.Windows.Forms.Button();
            this.ControlPermissions = new System.Windows.Forms.GroupBox();
            this.Disabled = new System.Windows.Forms.CheckBox();
            this.InVisible = new System.Windows.Forms.CheckBox();
            this.PageControls = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rolesTableAdapter = new ControlBasedSecurity.ControlSecurityDataSetTableAdapters.RolesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.rolesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSecurityDataSet)).BeginInit();
            this.ControlPermissions.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.Yellow;
            this.Cancel.Location = new System.Drawing.Point(445, 215);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 61;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "Note: By default, all controls are visible and enabled";
            // 
            // ByRoleRB
            // 
            this.ByRoleRB.AutoSize = true;
            this.ByRoleRB.Location = new System.Drawing.Point(499, 470);
            this.ByRoleRB.Name = "ByRoleRB";
            this.ByRoleRB.Size = new System.Drawing.Size(62, 17);
            this.ByRoleRB.TabIndex = 64;
            this.ByRoleRB.Text = "By Role";
            this.ByRoleRB.UseVisualStyleBackColor = true;
            this.ByRoleRB.Click += new System.EventHandler(this.PermissionTreeButtonChanged);
            // 
            // ByControlRB
            // 
            this.ByControlRB.AutoSize = true;
            this.ByControlRB.Checked = true;
            this.ByControlRB.Location = new System.Drawing.Point(417, 470);
            this.ByControlRB.Name = "ByControlRB";
            this.ByControlRB.Size = new System.Drawing.Size(73, 17);
            this.ByControlRB.TabIndex = 63;
            this.ByControlRB.TabStop = true;
            this.ByControlRB.Text = "By Control";
            this.ByControlRB.UseVisualStyleBackColor = true;
            this.ByControlRB.CheckedChanged += new System.EventHandler(this.PermissionTreeButtonChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 322);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 62;
            this.label2.Text = "Current Status";
            // 
            // PermissionTree
            // 
            this.PermissionTree.Location = new System.Drawing.Point(28, 338);
            this.PermissionTree.Name = "PermissionTree";
            this.PermissionTree.Size = new System.Drawing.Size(535, 126);
            this.PermissionTree.TabIndex = 61;
            // 
            // PermissionRoles
            // 
            this.PermissionRoles.DataSource = this.rolesBindingSource;
            this.PermissionRoles.DisplayMember = "RoleName";
            this.PermissionRoles.FormattingEnabled = true;
            this.PermissionRoles.Location = new System.Drawing.Point(299, 26);
            this.PermissionRoles.Name = "PermissionRoles";
            this.PermissionRoles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.PermissionRoles.Size = new System.Drawing.Size(120, 212);
            this.PermissionRoles.TabIndex = 59;
            this.PermissionRoles.ValueMember = "RoleID";
            // 
            // rolesBindingSource
            // 
            this.rolesBindingSource.DataMember = "Roles";
            this.rolesBindingSource.DataSource = this.controlSecurityDataSet;
            // 
            // controlSecurityDataSet
            // 
            this.controlSecurityDataSet.DataSetName = "ControlSecurityDataSet";
            this.controlSecurityDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Save
            // 
            this.Save.BackColor = System.Drawing.Color.Lime;
            this.Save.Location = new System.Drawing.Point(445, 185);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 60;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = false;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // ControlPermissions
            // 
            this.ControlPermissions.Controls.Add(this.Cancel);
            this.ControlPermissions.Controls.Add(this.Save);
            this.ControlPermissions.Controls.Add(this.PermissionRoles);
            this.ControlPermissions.Controls.Add(this.Disabled);
            this.ControlPermissions.Controls.Add(this.InVisible);
            this.ControlPermissions.Controls.Add(this.PageControls);
            this.ControlPermissions.Location = new System.Drawing.Point(13, 57);
            this.ControlPermissions.Name = "ControlPermissions";
            this.ControlPermissions.Size = new System.Drawing.Size(550, 238);
            this.ControlPermissions.TabIndex = 60;
            this.ControlPermissions.TabStop = false;
            this.ControlPermissions.Text = "Control Permissions";
            // 
            // Disabled
            // 
            this.Disabled.AutoSize = true;
            this.Disabled.Location = new System.Drawing.Point(445, 52);
            this.Disabled.Name = "Disabled";
            this.Disabled.Size = new System.Drawing.Size(67, 17);
            this.Disabled.TabIndex = 58;
            this.Disabled.Text = "Disabled";
            this.Disabled.UseVisualStyleBackColor = true;
            // 
            // InVisible
            // 
            this.InVisible.AutoSize = true;
            this.InVisible.Location = new System.Drawing.Point(445, 28);
            this.InVisible.Name = "InVisible";
            this.InVisible.Size = new System.Drawing.Size(64, 17);
            this.InVisible.TabIndex = 57;
            this.InVisible.Text = "Invisible";
            this.InVisible.UseVisualStyleBackColor = true;
            // 
            // PageControls
            // 
            this.PageControls.FormattingEnabled = true;
            this.PageControls.Location = new System.Drawing.Point(15, 26);
            this.PageControls.Name = "PageControls";
            this.PageControls.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.PageControls.Size = new System.Drawing.Size(266, 212);
            this.PageControls.Sorted = true;
            this.PageControls.TabIndex = 56;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(576, 24);
            this.menuStrip1.TabIndex = 66;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.homeToolStripMenuItem.Text = "Home";
            this.homeToolStripMenuItem.Click += new System.EventHandler(this.homeToolStripMenuItem_Click);
            // 
            // rolesTableAdapter
            // 
            this.rolesTableAdapter.ClearBeforeFill = true;
            // 
            // ManagePermissions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(576, 496);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ByRoleRB);
            this.Controls.Add(this.ByControlRB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PermissionTree);
            this.Controls.Add(this.ControlPermissions);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ManagePermissions";
            this.Text = "Manage Permissions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManagePermissions_FormClosing);
            this.Load += new System.EventHandler(this.ManagePermissions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rolesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlSecurityDataSet)).EndInit();
            this.ControlPermissions.ResumeLayout(false);
            this.ControlPermissions.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton ByRoleRB;
        private System.Windows.Forms.RadioButton ByControlRB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView PermissionTree;
        private System.Windows.Forms.ListBox PermissionRoles;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.GroupBox ControlPermissions;
        private System.Windows.Forms.CheckBox Disabled;
        private System.Windows.Forms.CheckBox InVisible;
        private System.Windows.Forms.ListBox PageControls;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private ControlSecurityDataSet controlSecurityDataSet;
        private System.Windows.Forms.BindingSource rolesBindingSource;
        private ControlBasedSecurity.ControlSecurityDataSetTableAdapters.RolesTableAdapter rolesTableAdapter;
    }
}