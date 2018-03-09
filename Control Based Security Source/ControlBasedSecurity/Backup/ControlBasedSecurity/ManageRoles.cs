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
    public partial class ManageRoles : Form
    {

        public ManageRoles()
        {
            InitializeComponent();
            FillUsersInRollsTree();
        }

  


        private void homeToolStripMenuItem_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.OK;
        }

        private void AddNewRole_Click( object sender, EventArgs e )
        {
            string newName = string.Empty;
            newName = NewRoleName.Text;
            NewRoleName.Text = string.Empty; // clear the control

            ControlSecurityDataSet.RolesRow newRolesRow;
            newRolesRow = controlSecurityDataSet.Roles.NewRolesRow();
            newRolesRow.RoleName = newName;
            this.controlSecurityDataSet.Roles.Rows.Add( newRolesRow );

            try
            {
                this.rolesTableAdapter.Update( this.controlSecurityDataSet.Roles );
            }
            catch ( Exception ex )
            {
                this.controlSecurityDataSet.Roles.Rows.Remove( newRolesRow );
                MessageBox.Show( "Unable to add role " + newName + ex.Message,
                "Unable to add role!", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

            RolesListBox.SelectedIndex = -1;

        }

        private void AddNewAppUser_Click( object sender, EventArgs e )
        {
            ControlSecurityDataSet.UsersRow newUsersRow;
            newUsersRow = controlSecurityDataSet.Users.NewUsersRow();
            newUsersRow.Name = NewUserName.Text;
            NewUserName.Text = string.Empty;
            this.controlSecurityDataSet.Users.Rows.Add(newUsersRow);
            this.usersTableAdapter.Update(this.controlSecurityDataSet.Users);
            AppUsersListBox.SelectedIndex = -1;
        }

        private void AddUsersToRole_Click( object sender, EventArgs e )
        {
             ConnectionStringSettingsCollection connectionStrings =
                    ConfigurationManager.ConnectionStrings;
                                                    
            string connString = connectionStrings["ControlBasedSecurity.Properties.Settings.ControlSecurityConnectionString"].ToString() ;
            SqlConnection conn = new SqlConnection( connString );
            conn.Open();
            SqlParameter param;

            foreach ( DataRowView userRow in AppUsersListBox.SelectedItems )
            {
                foreach ( DataRowView roleRow in RolesListBox.SelectedItems )
                {
                    int userID = Convert.ToInt32(userRow["UserID"]);
                    int roleID = Convert.ToInt32(roleRow["RoleID"]);
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "spInsertNewUserInRole";
                        cmd.CommandType = CommandType.StoredProcedure;
                        param = cmd.Parameters.Add( "@USERID", SqlDbType.Int );
                        param.Value = userID;
                        param.Direction = ParameterDirection.Input;
                        param = cmd.Parameters.Add( "@RoleID", SqlDbType.Int );
                        param.Value = roleID;
                        param.Direction = ParameterDirection.Input;
                        int rowsInserted = cmd.ExecuteNonQuery();
                        if ( rowsInserted != 1 )
                        {
                            DisplayError( userID, roleID, "Rows inserted = " + rowsInserted.ToString() );
                        }
                    }
                    catch ( Exception ex )
                    {
                        DisplayError( userID, roleID, ex.Message );
                    }
                        
                }
            }
            conn.Close();
            FillUsersInRollsTree();
            
        }

        private void DisplayError( int userID, int roleID, string message )
        {
            MessageBox.Show( "Unable to add user (" + userID + ") to role (" + roleID + ")" + message,
                "Unable to add user to role",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error );
        }

        
        private void FillUsersInRollsTree()
        {
            ConnectionStringSettingsCollection connectionStrings =
       ConfigurationManager.ConnectionStrings;

            string connString = connectionStrings["ControlBasedSecurity.Properties.Settings.ControlSecurityConnectionString"].ToString();
            SqlConnection conn = new SqlConnection( connString );
            conn.Open();

            string queryString = "select u.Name, r.RoleName from userstoRoles utr " +
            " join users u on u.userID = utr.FKUserID " +
            " join Roles r on r.roleID = utr.FKRoleID ";

            if ( rbName.Checked )
            {
                queryString += "order by Name";
            }
            else
            {
                queryString += "order by RoleName";
            }

            UsersInRoles.BeginUpdate();
            UsersInRoles.Nodes.Clear();
            TreeNode parentNode = null;
            TreeNode subNode = null;

            DataSet ds = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter( queryString, conn );
            dataAdapter.Fill( ds, "usersInRoles" );
            DataTable dt = ds.Tables[0];
            string currentName = string.Empty;
            foreach ( DataRow row in dt.Rows ) 
            {
                if ( rbName.Checked )
                {
                    subNode = new TreeNode( row["roleName"].ToString() );
                    if ( currentName != row["Name"].ToString() )
                    {
                        parentNode = new TreeNode( row["Name"].ToString() );
                        currentName = row["Name"].ToString();
                        UsersInRoles.Nodes.Add( parentNode );
                    }
                }
                else
                {
                    subNode = new TreeNode( row["Name"].ToString() );
                    if ( currentName != row["RoleName"].ToString() )
                    {
                        parentNode = new TreeNode( row["RoleName"].ToString() );
                        currentName = row["RoleName"].ToString();
                        UsersInRoles.Nodes.Add( parentNode );
                    }

                }

                if ( parentNode != null )
                {
                    parentNode.Nodes.Add( subNode );
                }
            }
            UsersInRoles.EndUpdate();
        }

        private void RadioButtonClick( object sender, EventArgs e )
        {
            FillUsersInRollsTree();
        }

        private void ManageRoles_Load( object sender, EventArgs e )
        {
            // TODO: This line of code loads data into the 'controlSecurityDataSet.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill( this.controlSecurityDataSet.Users );
            // TODO: This line of code loads data into the 'controlSecurityDataSet.Roles' table. You can move, or remove it, as needed.
            this.rolesTableAdapter.Fill( this.controlSecurityDataSet.Roles );

        }

    }
}