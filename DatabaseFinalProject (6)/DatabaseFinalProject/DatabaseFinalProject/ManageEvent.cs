using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace DatabaseFinalProject
{
    public partial class ManageEvent : Form
    {
        public ManageEvent manageEventReference { get; set; }
        private MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        private const string ConnectionString = "datasource=localhost;port=3306;username=root;password=";
        private DataTable originalDataTable = new DataTable(); 
        private bool isSearchBoxEmpty = true;
        public ManageEvent()
        {
            InitializeComponent();
            dgvEventList.Refresh();
            if (manageEventReference == null)
                manageEventReference = this;
            LoadData();
            this.WindowState = FormWindowState.Maximized;
            dgvEventList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEventList.MultiSelect = false;
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

                       
                        manageEventReference.dgvEventList.DataSource = dataTable;
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
        private void DeleteRecord(string eventnamae, string location, DateTime date)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "DELETE FROM user.event WHERE eventname = @eventname AND location = @location AND date = @date";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@eventname", eventnamae);
                    command.Parameters.AddWithValue("@location", location);
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

        private DataTable LoadData(string searchTerm = "")
        {
            DataTable dataTable = new DataTable();

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                
                string query = "SELECT * FROM user.event";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query += $" WHERE eventName LIKE '%{searchTerm}%'";
                }

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

           
            dgvEventList.DataSource = dataTable;
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("File not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return dataTable;

        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Hide();
        }

        private void ManageEvent_Load(object sender, EventArgs e)
        {
            
        }

        private void dgvEventList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void lblCreateEvent_Click(object sender, EventArgs e)
        {
            CreateNewEvent newEvent = new CreateNewEvent(dgvEventList);
            newEvent.Show();
        }

       
        private void pbSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text;
            LoadData(searchTerm);
        }

        private void lblStudList_Click(object sender, EventArgs e)
        {
            StudList sl = new StudList();
            this.Hide();
            sl.Show();
        }

        private void lblAddStudent_Click(object sender, EventArgs e)
        {

        }

        private void lblAttRec_Click(object sender, EventArgs e)
        {
            AttendanceRec at = new AttendanceRec();
            this.Hide();
            at.Show();
        }
            private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      

        private void lblDelete_Click(object sender, EventArgs e)
        {
            if (dgvEventList.SelectedRows.Count > 0)
            {
               
                DataGridViewRow selectedRow = dgvEventList.SelectedRows[0];
                string firstname = selectedRow.Cells["eventname"].Value.ToString();
                string lastname = selectedRow.Cells["location"].Value.ToString();
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
            if (dgvEventList.SelectedRows.Count > 0)
            {
               
                DataGridViewRow selectedRow = dgvEventList.SelectedRows[0];
                string eventname = selectedRow.Cells["eventname"].Value.ToString();
                string location = selectedRow.Cells["location"].Value.ToString();
                DateTime date = selectedRow.Cells["date"].Value != DBNull.Value ? Convert.ToDateTime(selectedRow.Cells["date"].Value): DateTime.MinValue;
                string gradelevel = selectedRow.Cells["gradelevel"].Value.ToString();
                string fines = selectedRow.Cells["fines"].Value.ToString();

               
                CreateNewEvent CNE = new CreateNewEvent(dgvEventList, eventname, location, date, gradelevel, fines);

                
                CNE.ShowDialog();

                
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
    }
}
