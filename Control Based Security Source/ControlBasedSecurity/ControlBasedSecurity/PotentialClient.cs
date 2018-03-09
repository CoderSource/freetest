using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ControlBasedSecurity
{
    public partial class PotentialClient : Form
    {
        public PotentialClient()
        {
            InitializeComponent();
        }

        private void manageRolesToolStripMenuItem_Click( object sender, EventArgs e )
        {
            ManageRoles1 dlg = new ManageRoles1();
            dlg.ShowDialog();
        }

        private void managePermissionsToolStripMenuItem_Click( object sender, EventArgs e )
        {
            ManagePermissions dlg = 
               new ManagePermissions( this, this.toolTip1, this.toolTip2 );
            dlg.Show();
        }

       private void SaveAndCreateJob_Click( object sender, EventArgs e )
       {
          NewContract dlg = new NewContract();
          dlg.ShowDialog();
       }
    }
}