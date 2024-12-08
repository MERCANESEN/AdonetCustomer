using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdonetCustomer
{
    public partial class FrmCity : Form
    {
        public FrmCity()
        {
            InitializeComponent();
        }


        SqlConnection sqlConnection = new SqlConnection("Server=DESKTOP-ESBPIHB\\SQLEXPRESS; initial catalog=DbCustomer; integrated security=true");
        private void btnList_Click(object sender, EventArgs e)
        {
            
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Select * From TblCity", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection.Close();

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("insert into TblCity (CityName, CityCountry) values (@cityName , @cityCountry)" , sqlConnection);
            cmd.Parameters.AddWithValue("@cityName", txtCityName.Text);
            cmd.Parameters.AddWithValue("@cityCountry", txtCountry.Text);
            cmd.ExecuteNonQuery(); //kaayıt sayısı döner ve değişiklikleri veritabanına yansıtır 
            sqlConnection.Close();
            MessageBox.Show("Veriler Kaydedildi.");



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Delete from TblCity where CityId=@cityId ", sqlConnection);
            cmd.Parameters.AddWithValue("@cityId", txtCityId.Text);
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show(  "Kullanıcı silindi." , "Uyarı!" , MessageBoxButtons.OK , MessageBoxIcon.Information );


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Update TblCity Set CityName=@cityName , CityCountry=@cityCountry where CityId=@cityId", sqlConnection);
            cmd.Parameters.AddWithValue("@cityName", txtCityName.Text);
            cmd.Parameters.AddWithValue("@cityCountry", txtCountry.Text);
            cmd.Parameters.AddWithValue("@cityId", txtCityId.Text);
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Veritabanı Güncellendi.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Select * from TblCity where CityName=@cityName", sqlConnection);
            cmd.Parameters.AddWithValue("@cityName", txtCityName.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection.Close();
            
        }


    }
}
