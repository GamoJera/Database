using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace DatabaseFinalProject
{

    public partial class AddStudentcs : Form
    {
        public DataGridView dgvStudList { get; set; }
        private MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        private const string ConnectionString = "datasource=localhost;port=3306;username=root;password=";
        private bool isEditing = false;

        public AddStudentcs(DataGridView dgvStudList, string firstname = "", string lastname = "", string contactNo = "", string address = "", string gradelevel = "", string section = "")
        {

            InitializeComponent();
            this.dgvStudList = dgvStudList != null ? dgvStudList : new DataGridView();


            LoadData();

            if (!string.IsNullOrEmpty(firstname))
            {
                isEditing = true;

                txtFname.Text = firstname;
                txtLastName.Text = lastname;
                txtCNum .Text = contactNo;
                txtAddress.Text = address;
                cbxGLevel.Text = gradelevel;
                cbxSection.Text = section;
               
            }
        }
        private void LoadData()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "SELECT * FROM user.student_info";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Set the DataTable as the data source for the DataGridView
                        dgvStudList.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        private void UpdateRecord(string firstname, string lastname, string contactNo, string address, string gradelevel, string section)
        {
            try
            {
                string query = "UPDATE user.student_info " +
                               "SET contactNo = @contactNo, address = @address, gradelevel = @gradelevel, section = @section " +
                               "WHERE firstname = @firstname AND lastname = @lastname";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstname", firstname);
                    command.Parameters.AddWithValue("@lastname", lastname);
                    command.Parameters.AddWithValue("@contactNo", contactNo);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@gradelevel", gradelevel);
                    command.Parameters.AddWithValue("@section", section);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message);
            }
        }

        private bool IsValidContactNumber(string contactNo)
        {
           
            return contactNo.Length == 11 && contactNo.All(char.IsDigit);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string firstname = txtFname.Text;
            string lastname = txtLastName.Text;
            string contactNo = txtCNum.Text;
            string address = txtAddress.Text;
            string gradelevel = cbxGLevel.Text;
            string section = cbxSection.Text;

          
            try
            {
                connection.Open();

                if (isEditing)
                {
                   
                    UpdateRecord(firstname, lastname, contactNo, address, gradelevel, section);

                    MessageBox.Show("Information Updated");
                }
                else
                {
                  
                    InsertRecord(firstname, lastname, contactNo, address, gradelevel, section);
                    MessageBox.Show("Information Saved");
                }

             
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        private void InsertRecord(string firstname, string lastname, string contactNo, string address, string gradelevel, string section)
        {
            try
            {
                string query = "INSERT INTO user.student_info (firstname, lastname, contactNo, address,gradelevel, section) " +
                               "VALUES (@firstname, @lastname, @contactNo,@address, @gradelevel, @section)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstname", firstname);
                    command.Parameters.AddWithValue("@lastname", lastname);
                    command.Parameters.AddWithValue("@contactNo", contactNo);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@gradelevel", gradelevel);
                    command.Parameters.AddWithValue("@section", section);
                    

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting record: " + ex.Message);
            }
        }

        private void txtFname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLname_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtCNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtSection_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cbxGLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

      
    }
}