using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseFinalProject
{
    public partial class CreateAccount : Form
    {
        private MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        private string resetCode;
        public CreateAccount()
        {
            InitializeComponent();
            txtPass.PasswordChar = '*';
        }

        private void btnCAccount_Click(object sender, EventArgs e)
        {
            
            
          
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Please Input Username and Password");
                return;
            }

            try
            {
                
                connection.Open();

              
                string selectQuery = "SELECT * FROM user.login WHERE Username = @username;";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@username", txtUser.Text);

                
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        
                        MessageBox.Show("Username Not Available");
                    }
                    else
                    {
                      
                        reader.Close();

                      
                        string insertQuery = "INSERT INTO user.login (Username, Password) VALUES (@username, @password);";
                        MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@username", txtUser.Text);
                        insertCommand.Parameters.AddWithValue("@password", txtPass.Text);

                    
                        insertCommand.ExecuteNonQuery();

                       
                        MessageBox.Show("Account Successfully Created");
                    }
                }
                LoginForm lg = new LoginForm();
                lg.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Error during registration: " + ex.Message);
            }
            finally
            {
                
                connection.Close();
                
            }
            
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

