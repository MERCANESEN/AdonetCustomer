using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdonetCustomer
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection = new SqlConnection("Server=DESKTOP-ESBPIHB\\SQLEXPRESS; initial catalog=DbCustomer; integrated security=true");
        private void btnList_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Select CustomerId, CustomerName , CustomerSurname," +
                " CustomerBalance, CustomerStatus, CityName From TblCustomer  " +
                "\r\nInner Join TblCity on TblCity.CityId = TblCustomer.CustomerCity", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection.Close();


        }

        private void btnProcedure_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Execute CustomerListWithCity", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection.Close();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from TblCity", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cmbCity.ValueMember = "CityId";
            cmbCity.DisplayMember = "CityName";
            cmbCity.DataSource = dt;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Insert Into TblCustomer (CustomerName , CustomerSurname, " +
                "CustomerCity , CustomerBalance , CustomerStatus) values (@customerName, @customerSurname," +
                "@customerCity , @customerBalance , @customerStatus )", sqlConnection);
            cmd.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            cmd.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            cmd.Parameters.AddWithValue("@customerCity", cmbCity.SelectedValue);
            cmd.Parameters.AddWithValue("@customerBalance", txtBalance.Text);
            if (rdbActive.Checked)
            {
                cmd.Parameters.AddWithValue("@customerStatus", true);
            }
            if (rdbPassive.Checked)
            {
                cmd.Parameters.AddWithValue("@customerStatus", false);
            }
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Başarıyla Eklendi");



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Delete from TblCustomer where CustomerId=@customerId", sqlConnection);
            cmd.Parameters.AddWithValue("@customerId", txtCustomerId.Text);
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Kullanıcı silindi.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Update TblCustomer Set CustomerName=@customerName," +
                "CustomerSurname=@customerSurname,CustomerCity=@customerCity,CustomerBalance=@customerBalance," +
                "CustomerStatus=@customerStatus where CustomerId=@customerId", sqlConnection);
            cmd.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            cmd.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            cmd.Parameters.AddWithValue("@customerCity", cmbCity.SelectedValue);
            cmd.Parameters.AddWithValue("@customerBalance", txtBalance.Text);
            cmd.Parameters.AddWithValue("@customerId", txtCustomerId.Text);
            if (rdbActive.Checked)
            {
                cmd.Parameters.AddWithValue("@customerStatus", true);
            }
            if (rdbPassive.Checked)
            {
                cmd.Parameters.AddWithValue("@customerStatus", false);
            }
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Başarıyla Güncellendi.");

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Select CustomerId,CustomerName,CustomerSurname,CustomerBalance,CustomerStatus,CityName From TblCustomer Inner Join TblCity On TblCity.CityId=TblCustomer.CustomerCity Where CustomerName=@CustomerName", sqlConnection);
            cmd.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection.Close();
        }
    }
}