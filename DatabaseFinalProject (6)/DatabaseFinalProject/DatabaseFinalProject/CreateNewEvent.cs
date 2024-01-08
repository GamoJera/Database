using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace DatabaseFinalProject
{
    public partial class CreateNewEvent : Form
    {
        public DataGridView dgvEventList { get; set; }
        private MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        private const string ConnectionString = "datasource=localhost;port=3306;username=root;password=";
        private bool isEditing = false;
        public CreateNewEvent(DataGridView dgvEventList, string eventname = "", string location = "",DateTime date = default, string gradelevel = "", string fines = "")
        {
            InitializeComponent();
            this.dgvEventList = dgvEventList != null ? dgvEventList : new DataGridView();
            LoadData();
            if (!string.IsNullOrEmpty(eventname))
            {
                isEditing = true;

                txtEName.Text = eventname;
                txtLoc.Text = location;
                dtpEvent.Value = date != DateTime.MinValue ? date : DateTimePicker.MinimumDateTime;
                cbxGlevel.Text = gradelevel;
                txtFines.Text = fines;
                
               
            }
        }
        private void LoadData()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "SELECT * FROM user.event";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                       
                        dgvEventList.DataSource = dataTable;
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
       private void UpdateRecord(string eventname, string location, DateTime date, string gradelevel, string fines)
{
    try
    {
        string query = "UPDATE user.event " +
                       "SET gradelevel = @gradelevel, fines = @fines " +
                       "WHERE eventname = @eventname AND location = @location AND date = @date";

        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@eventname", eventname);
            command.Parameters.AddWithValue("@location", location);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@gradelevel", gradelevel);
            command.Parameters.AddWithValue("@fines", fines);

            if (connection.State != ConnectionState.Open)
                connection.Open();

            command.ExecuteNonQuery();
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error updating record: " + ex.Message);
    }
    finally
    {
        if (connection.State == ConnectionState.Open)
            connection.Close();
    }
}




        private void btnSave_Click(object sender, EventArgs e)
        {
            string eventname = txtEName.Text;
            string location = txtLoc.Text;
            DateTime date = dtpEvent.Value;
            string gradelevel = cbxGlevel.Text;
            string fines = txtFines.Text;

            try
            {
                connection.Open();

                if (isEditing)
                {
                  
                    UpdateRecord(eventname, location, date, gradelevel, fines);

                    MessageBox.Show("Information Updated");
                }
                else
                {
                  
                    InsertRecord(eventname, location, date, gradelevel, fines);
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
        private void InsertRecord(string eventname, string location, DateTime date, string gradelevel, string fines)
        {
            try
            {
                string query = "INSERT INTO user.event (eventname, location, date, gradelevel,  fines) " +
                               "VALUES (@eventname, @location, @date, @gradelevel, @fines)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@eventname", eventname);
                    command.Parameters.AddWithValue("@location", location);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@gradelevel", gradelevel); 
                    command.Parameters.AddWithValue("@fines", fines);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting record: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtEName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblManEvent_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void txtFines_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dtpEvent_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbxGlevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    }


