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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Clear();
            txtUsername.Focus();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //to check login username and password

             

            if (txtUsername.Text == "admin" && txtPassword.Text == "admin@123")
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show();


            }
            else
            {
                MessageBox.Show("Invalid Username and Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }





        }
    }
}
