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
    public partial class Client : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-I43AV94;Initial Catalog=StokTakip;Integrated Security=True");
        int UrunID = 0;
        public Client(string role)
        {
            InitializeComponent();
            UrunGoster();
            label11.Text = role;
        }
       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlcmd = new SqlCommand("UrunEkle", sqlCon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@mode", "Ekle");
                sqlcmd.Parameters.AddWithValue("@UrunID",0);
                sqlcmd.Parameters.AddWithValue("@Birimi", txtBirim.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@UrunAdi", txtUrunadi.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@Teslimzamani", txtTeslimzamani.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@Stokyeri", cmbStokyeri.SelectedItem.ToString());
                sqlcmd.Parameters.AddWithValue("@Marka", txtMarka.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@UreticiFirma",txtUreticiFirma.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@UrunFiyati", txtUrunfiyati.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@Photo",SavePhoto());

                sqlcmd.ExecuteNonQuery();
                MessageBox.Show("Urun basariyla kaydedildi");
                UrunGoster();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Urun kaydedilirken hata olustu");
            }
            finally
            {
                sqlCon.Close();
            }
            
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pbxImage.Image.Save(ms, pbxImage.Image.RawFormat);           
            return ms.GetBuffer();//return the array of stream
        }
        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }
        void FillDataGridView()
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sda = new SqlDataAdapter("UrunAra", sqlCon);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@UrunAdi",txtIsimara.Text.Trim());
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            dgvUrun.DataSource = dtbl;
            sqlCon.Close();
            }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Urun aratilirken hata meydana geldi"+ex.Message);
            }
        }
        public void UrunGoster()
        {

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sda = new SqlDataAdapter("UrunlariGoster", sqlCon);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            dgvUrun.DataSource = dtbl;
            sqlCon.Close();
        }

        private void btnUrunsil_Click(object sender, EventArgs e)
        {
            try
            {
               

                sqlCon.Open();
                string qry = " DELETE FROM Urun WHERE UrunID='" + UrunID + "'";

                SqlCommand cmd = new SqlCommand(qry, sqlCon);//ISBN='"+isbn +
                int i = cmd.ExecuteNonQuery();
                

                sqlCon.Close();
                
                
            }
            catch (System.Exception exp)
            {

                MessageBox.Show("Error Occured at deletion:" + exp.ToString());
            }
            finally
            {
                MessageBox.Show("Urun basariyla silindi.");
                UrunGoster();
            }
        }

        private void dgvUrun_DoubleClick(object sender, EventArgs e)
        {
            if (dgvUrun.CurrentRow.Index != -1)
            {
                UrunID = Convert.ToInt32(dgvUrun.CurrentRow.Cells[0].Value.ToString());
                txtBirim.Text = dgvUrun.CurrentRow.Cells[1].Value.ToString();
                txtUrunadi.Text = dgvUrun.CurrentRow.Cells[2].Value.ToString();
                txtTeslimzamani.Text = dgvUrun.CurrentRow.Cells[3].Value.ToString();
                cmbStokyeri.SelectedItem = dgvUrun.CurrentRow.Cells[4].Value.ToString();
                txtMarka.Text = dgvUrun.CurrentRow.Cells[5].Value.ToString();
                txtUreticiFirma.Text = dgvUrun.CurrentRow.Cells[6].Value.ToString();
                txtUrunfiyati.Text = dgvUrun.CurrentRow.Cells[7].Value.ToString();
                pbxImage.Image = GetPhoto((byte[])dgvUrun.CurrentRow.Cells[8].Value);

            }
        }

        private void pbxImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            
            ofd.Title = "Select the Photo";
            ofd.Filter = "PNG File (*.png)|*.png|JPG File(*.jpg)|*.jpg|BMP File(*.bmp)|*.bmp|GIF File(*.gif)|*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbxImage.Image = new Bitmap(ofd.FileName);
            }
        }

        private void frmStoktakip_Load(object sender, EventArgs e)
        {
            
                btnUpdate.Enabled = false;
                btnSave.Enabled = false;
            btnUrunsil.Enabled = false;
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlcmd = new SqlCommand("UpdateItem", sqlCon);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("@UrunID", this.UrunID);
                
                sqlcmd.Parameters.AddWithValue("@Birimi", txtBirim.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@UrunAdi", txtUrunadi.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@Teslimzamani", txtTeslimzamani.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@Stokyeri", cmbStokyeri.SelectedItem.ToString());
                sqlcmd.Parameters.AddWithValue("@Marka", txtMarka.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@UreticiFirma", txtUreticiFirma.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@UrunFiyati", txtUrunfiyati.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@Photo", SavePhoto());

                sqlcmd.ExecuteNonQuery();
                MessageBox.Show("Urun basariyla Güncellendi");
                UrunGoster();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Urun Güncellenirken hata olustu");
            }
            finally
            {
                sqlCon.Close();
            }
        }



        /*public void showItems()
        {
            try
            {
                sqlCon.Open();

                SqlDataAdapter sda = new SqlDataAdapter("Select * from Urun", sqlCon);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dgvUrun.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgvUrun.Rows.Add();
                    dgvUrun.Rows[n].Cells[0].Value = item[0].ToString();
                    dgvUrun.Rows[n].Cells[1].Value = item[1].ToString();
                    dgvUrun.Rows[n].Cells[2].Value = item[2].ToString();
                    dgvUrun.Rows[n].Cells[3].Value = item[3].ToString();
                    dgvUrun.Rows[n].Cells[4].Value = item[4].ToString();
                    dgvUrun.Rows[n].Cells[5].Value = item[5].ToString();
                    dgvUrun.Rows[n].Cells[6].Value = item[6].ToString();
                    dgvUrun.Rows[n].Cells[7].Value = item[7].ToString();

                }
                sqlCon.Close();
            }
            catch (Exception exp)
            {

                MessageBox.Show("Error in show items" + exp.ToString());
            }
        }*/
    }
}
