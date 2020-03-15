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
    public partial class Login : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-I43AV94;Initial Catalog=StokTakip;Integrated Security=True");
        public Login()
        {
            InitializeComponent();
        }

        /*public string Roleuygulama()
        {
            string returnValue = null;
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlcmd = new SqlCommand("RolValidation", sqlCon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                
                sqlcmd.Parameters.AddWithValue("@UserID", SqlDbType.Int);
                
                var rol = sqlcmd.Parameters.Add("@result", SqlDbType.NVarChar, 50);
                rol.Direction = ParameterDirection.ReturnValue;
                sqlcmd.ExecuteNonQuery();
                returnValue = rol.ToString();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Urun kaydedilirken hata olustu");
            }
            finally
            {
                
                sqlCon.Close();
            }
            return returnValue;
           
        }*/
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.txtUsername.Text == "")
            {
                MessageBox.Show("The username cannot be unfilled");
            }
            if (this.txtPassword.Text.Length == 0)
            {
                MessageBox.Show("Please enter password");
            }
            if (this.txtUsername.Text == "" || this.txtPassword.Text.Length == 0)
            {
                MessageBox.Show("Username or Password is empty");
            }
            string uname = txtUsername.Text.ToString();
            string pass = txtPassword.Text.ToString();
            sqlCon.Open();
            string qry = "select role from LogUser where username ='" + uname + "' and password='" + pass + "'";
            
            SqlDataAdapter sda = new SqlDataAdapter(qry, sqlCon);
            DataTable dt = new DataTable();
            
            sda.Fill(dt);
            
            
            if (dt.Rows.Count == 1 )
            {
                MessageBox.Show("valid user");
                this.Hide();
                if (dt.Rows[0][0].ToString()== "Admin")
                {
                    frmStoktakip obj1 = new frmStoktakip(dt.Rows[0][0].ToString());
                    obj1.Show();
                }
                else if (dt.Rows[0][0].ToString() == "Client")
                {
                    Client obj1 = new Client(dt.Rows[0][0].ToString());
                    obj1.Show();
                }

            }
           
            else
                MessageBox.Show("invalid user");
            sqlCon.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register r = new Register();
            r.Show();
        }
    }
}
