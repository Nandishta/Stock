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

namespace Stock
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Product_Load(object sender, EventArgs e)
        {
            comboStatus.SelectedIndex = 0;
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string ProductName = txtProductName.Text;
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-2E5LIVNB\\MSSQLSERVER2016;Initial Catalog=stock;Integrated Security=True");
            //Insert logic
            con.Open();
            bool status = false;
            if (comboStatus.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            var sqlQuery = "";
            if(IfProductExists(con, txtProductCode.Text))
            {
                sqlQuery = @"UPDATE [product] SET [ProductName] = '" + ProductName + "' ,[ProductStatus] = '" + status + "' WHERE [ProductCode] = '" + txtProductCode.Text+ "'";
            }
            else
            {
                sqlQuery = @"INSERT INTO [dbo].[product] ([ProductCode],[ProductName] ,[ProductStatus]) VALUES ('" + txtProductCode.Text + "', '" + ProductName + "','" + status + "') ";
            }




            SqlCommand scm = new SqlCommand(sqlQuery, con);
            scm.ExecuteNonQuery();
            con.Close();
            LoadData();


        }

        private bool IfProductExists(SqlConnection con, string productCode)
        {
            
            
            SqlDataAdapter sda = new SqlDataAdapter("Select 1 FROM [product] WHERE [ProductCode] = '" + productCode + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count > 0)

            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void LoadData()
        {
            //Reading data

            SqlConnection con = new SqlConnection("Data Source=LAPTOP-2E5LIVNB\\MSSQLSERVER2016;Initial Catalog=stock;Integrated Security=True");

            SqlDataAdapter sda = new SqlDataAdapter("Select * From [dbo].[product]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvProduct.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dgvProduct.Rows.Add();
                dgvProduct.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dgvProduct.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                if ((bool)item["ProductStatus"])
                {
                    dgvProduct.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dgvProduct.Rows[n].Cells[2].Value = "Deactive";
                }

            }


        }

        private void dgvProduct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtProductCode.Text = dgvProduct.SelectedRows[0].Cells[0].Value.ToString();
            txtProductName.Text = dgvProduct.SelectedRows[0].Cells[1].Value.ToString();
            if (dgvProduct.SelectedRows[0].Cells[2].Value.ToString() == " Active")
            {
                comboStatus.SelectedIndex = 0;
            }
            else
            {

                comboStatus.SelectedIndex = 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-2E5LIVNB\\MSSQLSERVER2016;Initial Catalog=stock;Integrated Security=True");

            var sqlQuery = "";
            if (IfProductExists(con, txtProductCode.Text))
            {
                con.Open();

                sqlQuery = @"DELETE FROM [product] WHERE [ProductCode] = '" + txtProductCode.Text + "'";
                SqlCommand scm = new SqlCommand(sqlQuery, con);
                scm.ExecuteNonQuery();
                con.Close();

            }
            else
            {
                MessageBox.Show("Record Not Exists!");
            }
            LoadData();
              

        }
       
    }
}
