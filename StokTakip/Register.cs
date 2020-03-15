using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace StokTakip
{
    public partial class Register : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-I43AV94;Initial Catalog=StokTakip;Integrated Security=True");
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void btnRegReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login r = new Login();
            r.Show();
        }

        private void btnRegSave_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtRegUsername.Text;
                string password = txtRegPassword.Text;
                string role = cmbRole.SelectedItem.ToString();
                sqlCon.Open();
                string qry = "insert into LogUser (username,password,role) values ('" + username + "','" + password + "','" + role + "')";//mobile is int that is why it doesnt require '
                SqlCommand cmd = new SqlCommand(qry, sqlCon);
                int i = cmd.ExecuteNonQuery();
                if (i >= 1)
                    MessageBox.Show(i + " Number of user Registered with username: " + username);
                else
                    MessageBox.Show("User registration failed. ");
                sqlCon.Close();
                
            }
            catch (System.Exception exp)
            {

                MessageBox.Show("Error Occured at User Registration:" + exp.ToString());
            }
        }
    }
}
