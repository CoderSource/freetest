using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ControlBasedSecurity
{
    public partial class ManagePermissions : Form
    {
        private Dictionary<string, string> oldMenuToolTips = 
            new Dictionary<string, string>();
        private Form workingForm;
        private ToolTip formToolTip1 = null;
        private ToolTip formToolTip2 = null;

        public ManagePermissions( Form f, ToolTip toolTip1, ToolTip toolTip2 )
        {
            InitializeComponent();
            workingForm = f;

            formToolTip1 = toolTip1;
            formToolTip2 = toolTip2;
            formToolTip1.Active = false;
            formToolTip2.Active = true;

            this.Text += " for page " + f.Name;
            ShowControls( f.Controls );
            PopulatePermissionTree();

        }

        private void PopulatePermissionTree()
        {

             ConnectionStringSettingsCollection connectionStrings =
              ConfigurationManager.ConnectionStrings;
            string connString = connectionStrings[
               "ControlBasedSecurity.Properties.Settings.ControlSecurityConnectionString"].
                   ToString();
            SqlConnection conn = new SqlConnection( connString );
            conn.Open();

            string queryString = "select controlID, Invisible, Disabled, RoleName " +
            "from ControlsToRoles ctr " +
            " join controls c on c.ControlID = ctr.FKControlID and c.Page = ctr.FKPage " +
            " join roles r on r.RoleID = ctr.FKRole ";

            if ( ByControlRB.Checked )
            {
                queryString += " order by ControlID";
            }
            else
            {
                queryString += " order by RoleName";
            }

            DataSet ds = new DataSet();
            SqlDataAdapter dataAdapter = null;
            DataTable dt = null;
            try
            {
                dataAdapter = new SqlDataAdapter( queryString, conn );
                dataAdapter.Fill( ds, "controlsToRoles" );
                dt = ds.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show( "Unable to retrieve permissions: " + e.Message, 
                    "Error retrieving permissions", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error );
            }
            finally
            {
                conn.Close();
            }
           
            PermissionTree.BeginUpdate();
            PermissionTree.Nodes.Clear();
            TreeNode parentNode = null;
            TreeNode subNode = null;

            string currentName = string.Empty;
            foreach ( DataRow row in dt.Rows )
            {
                string subNodeText = ByControlRB.Checked ? row["RoleName"].ToString() : row["ControlID"].ToString();
                subNodeText += ":";
                subNodeText += Convert.ToInt32( row["Invisible"] ) == 0 ? " visible " : " not visible ";
                subNodeText += " and ";
                subNodeText += Convert.ToInt32( row["Disabled"] ) == 0 ? " enabled " : " disabled ";

                subNode = new TreeNode ( subNodeText );
                string dataName = ByControlRB.Checked ? row["ControlID"].ToString() : row["RoleName"].ToString();
                if ( currentName != dataName )
                {
                    parentNode = new TreeNode( dataName );
                    currentName = dataName;
                    PermissionTree.Nodes.Add( parentNode );
                }

                if ( parentNode != null )
                {
                    parentNode.Nodes.Add( subNode );
                }
            }
            PermissionTree.EndUpdate();
        }
 
        private void PermissionTreeButtonChanged( object sender, EventArgs e )
        {
           PopulatePermissionTree();
        }

        private void Save_Click( object sender, EventArgs e )
        {

            ConnectionStringSettingsCollection connectionStrings =
                   ConfigurationManager.ConnectionStrings;

            string connString = connectionStrings["ControlBasedSecurity.Properties.Settings.ControlSecurityConnectionString"].ToString();
            SqlConnection conn = new SqlConnection( connString );
            conn.Open();
            SqlParameter param;

            foreach ( String controlID in PageControls.SelectedItems )
            {
                foreach ( DataRowView roleRow in PermissionRoles.SelectedItems )
                {
                    
                    int roleID = Convert.ToInt32( roleRow["RoleID"] );
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "spInsertNewControlToRole";
                        cmd.CommandType = CommandType.StoredProcedure;

                        param = cmd.Parameters.Add( "@RoleID", SqlDbType.Int );
                        param.Value = roleID;
                        param.Direction = ParameterDirection.Input;

                        param = cmd.Parameters.Add( "@PageName", SqlDbType.VarChar, 50 );
                        param.Value = workingForm.Name.ToString();
                        param.Direction = ParameterDirection.Input;

                        param = cmd.Parameters.Add( "@ControlID", SqlDbType.VarChar, 50 );
                        param.Value = controlID;
                        param.Direction = ParameterDirection.Input;

                        param = cmd.Parameters.Add( "@invisible", SqlDbType.Int );
                        param.Value = InVisible.Checked ? 1 : 0;
                        param.Direction = ParameterDirection.Input;

                        param = cmd.Parameters.Add( "@disabled", SqlDbType.Int );
                        param.Value = Disabled.Checked ? 1 : 0;
                        param.Direction = ParameterDirection.Input;


                        int rowsInserted = cmd.ExecuteNonQuery();
                        if ( rowsInserted < 1 || rowsInserted > 2 )
                        {
                            DisplayError( controlID, roleID, "Rows inserted = " + rowsInserted.ToString() );
                        }
                    }
                    catch ( Exception ex )
                    {
                        DisplayError( controlID, roleID, ex.Message );
                    }
                }
            }
            conn.Close();
            PopulatePermissionTree();
        }

        private void ShowControls( Control.ControlCollection controlCollection )
        {
            foreach ( Control c in controlCollection )
            {
                if ( c.Controls.Count > 0 )
                {
                    ShowControls( c.Controls );
                }
                if ( c is MenuStrip )
                {
                    MenuStrip menuStrip = c as MenuStrip;
                    ShowToolStipItems( menuStrip.Items );
                }

                if ( c is Button || c is ComboBox || c is TextBox ||
                    c is ListBox || c is DataGridView || c is RadioButton ||
                    c is RichTextBox || c is TabPage )
                {

                    formToolTip2.SetToolTip( c, c.Name );

                    PageControls.Items.Add( c.Name );

                }
            }
        }

        private void ShowToolStipItems( ToolStripItemCollection toolStripItems )
        {
            foreach ( ToolStripMenuItem mi in toolStripItems )
            {
                oldMenuToolTips.Add( mi.Name, mi.ToolTipText );
                mi.ToolTipText = mi.Name;

                if ( mi.DropDownItems.Count > 0 )
                {
                    ShowToolStipItems( mi.DropDownItems );
                }

                PageControls.Items.Add( mi.Name );
            }
        }

        private void homeToolStripMenuItem_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.OK;
        }

        private void ManagePermissions_FormClosing(object sender, FormClosingEventArgs e)
        {
           foreach ( Control c in workingForm.Controls )
           {
               if ( c is MenuStrip )
               {
                   MenuStrip ms = c as MenuStrip;
                   RestoreMenuStripToolTips(ms.Items);
               }
           }

           formToolTip1.Active = true;
           formToolTip2.Active = false;

        }

        private void RestoreMenuStripToolTips( ToolStripItemCollection toolStripItems )
        {
            foreach ( ToolStripMenuItem mi in toolStripItems )
            {
                if ( mi.DropDownItems.Count > 0 )
                {
                    RestoreMenuStripToolTips( mi.DropDownItems );
                }

                if ( oldMenuToolTips.ContainsKey( mi.Name ) )
                {
                    mi.ToolTipText = oldMenuToolTips[mi.Name];
                }
                else
                {
                    mi.ToolTipText = string.Empty;
                }       // end else
            }           // end foreach
        }               // end RestoreMenuStripToolTips

        private void DisplayError( string controlID, int roleID, string message )
        {
            MessageBox.Show( "Unable to add control (" + controlID + ") to role (" + roleID + ")" + message,
                "Unable to add control to role",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error );
        }

        private void ManagePermissions_Load( object sender, EventArgs e )
       {
           this.rolesTableAdapter.Fill( this.controlSecurityDataSet.Roles );
       }                // end RestoreMenuStripToolTips

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }                   // end form
}                       // end namespace