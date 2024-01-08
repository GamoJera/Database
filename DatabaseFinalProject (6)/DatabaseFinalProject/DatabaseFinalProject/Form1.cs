using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DatabaseFinalProject
{
    public partial class LoginForm : Form
    {
       
        private MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");

       
        private string resetCode;

        
        public LoginForm()
        {
            InitializeComponent();

            
            txtPass.PasswordChar = '*';
        }

       
        private void btnLogin_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Please input Username And Password");
                return;
            }

            try
            {
               
                connection.Open();

               
                string selectQuery = "SELECT * FROM user.login WHERE Username = @username AND Password = @password;";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@username", txtUser.Text);
                command.Parameters.AddWithValue("@password", txtPass.Text);

               
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                       
                        MessageBox.Show("Login Successful");
                        this.Hide();
                        MainForm mf = new MainForm();
                        mf.Show();
                    }
                    else
                    {
                       
                        MessageBox.Show("Invalid Credentials!");
                        txtUser.Clear();
                        txtPass.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Error during login: " + ex.Message);
            }
            finally
            {
              
                connection.Close();
            }
        }

        
        private void lblCAccount_Click(object sender, EventArgs e)
        {
            CreateAccount Ca = new CreateAccount();
            Ca.Show();
            this.Hide();
        }
        
        private void cbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            
            if (cbShowPass.Checked)
            {
               
                txtPass.PasswordChar = '\0';
            }
            else
            {
               
                txtPass.PasswordChar = '*';
            }
        }
    }
}
