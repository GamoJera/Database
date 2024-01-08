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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DatabaseFinalProject
{
    public partial class AttendanceRec : Form
    {

        public AttendanceRec AttendanceRecFormReference { get; set; }
        private MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        private const string ConnectionString = "datasource=localhost;port=3306;username=root;password=";
        private DataTable originalDataTable = new DataTable();
        private bool isSearchBoxEmpty = true;
        public AttendanceRec()
        {
            InitializeComponent();
           
          
            dgvAttRec.Refresh();
            if (AttendanceRecFormReference == null)
                AttendanceRecFormReference = this;

            LoadData();
            this.WindowState = FormWindowState.Maximized;
            dgvAttRec.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAttRec.MultiSelect = false;
        }
        
        private void LoadData()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "SELECT * FROM user.attendance_rec";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        
                        AttendanceRecFormReference.dgvAttRec.DataSource = dataTable;
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


        private DataTable LoadData(string searchTerm = "")
        {
            DataTable dataTable = new DataTable();

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "SELECT * FROM user.attendance_rec";
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query += $" WHERE firstname LIKE '%{searchTerm}%'";
                }

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

              
                originalDataTable = dataTable.Copy();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

          
            dgvAttRec.DataSource = dataTable;
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("File not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return dataTable;
        }

        private void dgvAttRec_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblAddAtt_Click(object sender, EventArgs e)
        {
            AddAttendanceForm ad = new AddAttendanceForm(dgvAttRec);
            ad.Show();
        }

        private void pbSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text;
            LoadData(searchTerm);

        }

        private void label2_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Hide();
        }

        private void lblManEvent_Click(object sender, EventArgs e)
        {
            ManageEvent me = new ManageEvent();
            me.Show();
            this.Hide();
        }

        private void lblStudList_Click(object sender, EventArgs e)
        {
            StudList sl = new StudList();
            this.Hide();
            sl.Show();
        }
        private void DeleteRecord(string firstname, string lastname, DateTime date)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "DELETE FROM user.attendance_rec WHERE firstname = @firstname AND lastname = @lastname AND date = @date";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstname", firstname);
                    command.Parameters.AddWithValue("@lastname", lastname);
                    command.Parameters.AddWithValue("@date", date);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void lblDelete_Click(object sender, EventArgs e)
        {
            if (dgvAttRec.SelectedRows.Count > 0)
            {
                
                DataGridViewRow selectedRow = dgvAttRec.SelectedRows[0];
                string firstname = selectedRow.Cells["firstname"].Value.ToString();
                string lastname = selectedRow.Cells["lastname"].Value.ToString();
                DateTime date = selectedRow.Cells["date"].Value != DBNull.Value ? Convert.ToDateTime(selectedRow.Cells["date"].Value) : DateTime.MinValue;

                
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                  
                    DeleteRecord(firstname, lastname, date);

                    
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            if (dgvAttRec.SelectedRows.Count > 0)
            {
                
                DataGridViewRow selectedRow = dgvAttRec.SelectedRows[0];
                string firstname = selectedRow.Cells["firstname"].Value.ToString();
                string lastname = selectedRow.Cells["lastname"].Value.ToString();
                string gradelevel = selectedRow.Cells["gradelevel"].Value.ToString();
                string section = selectedRow.Cells["section"].Value.ToString();
                DateTime date = selectedRow.Cells["date"].Value != DBNull.Value ? Convert.ToDateTime(selectedRow.Cells["date"].Value) : DateTime.MinValue;
                string eventname = selectedRow.Cells["eventname"].Value.ToString();
                string attendance = selectedRow.Cells["attendance"].Value.ToString();
                string fines = selectedRow.Cells["fines"].Value.ToString();

               
                AddAttendanceForm addAttendanceForm = new AddAttendanceForm(dgvAttRec, firstname, lastname, gradelevel, section, date, eventname, attendance, fines);

             
                addAttendanceForm.ShowDialog();

 
                LoadData();
            }
            else
            {
                MessageBox.Show("Please select a row to edit.");
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)sender;

            if (isSearchBoxEmpty)
            {
                textBox.Text = string.Empty;
                isSearchBoxEmpty = false;
            }
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblatt_Click(object sender, EventArgs e)
        {

        }
    }

}
    
    

