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
    public partial class EditEventForm : Form
    {
        public DataGridView dgvEventList = new DataGridView(); 

        private MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        private const string ConnectionString = "datasource=localhost;port=3306;username=root;password=";

        public EditEventForm()
        {
            InitializeComponent();
            dgvEventList = new DataGridView();

            LoadData();
            dgvEventList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEventList.MultiSelect = false;
        }
        private DataTable LoadData()
        {
            DataTable dataTable = new DataTable();

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "SELECT * FROM user.event";
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

            return dataTable;
        }

        public void InitializeData(string eventName, string location, DateTime date, string gradeLevel, string fines)
        {
          
            txtEName.Text = eventName;
            txtLoc.Text = location;
            dtpEvent.Value = date;
            cbxGlevel.Text = gradeLevel;
            txtFines.Text = fines;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
           
            if (dgvEventList.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvEventList.SelectedRows[0].Index;

                string eventName = dgvEventList.Rows[selectedIndex].Cells["eventname"].Value.ToString();
                string location = dgvEventList.Rows[selectedIndex].Cells["location"].Value.ToString();
                DateTime date = Convert.ToDateTime(dgvEventList.Rows[selectedIndex].Cells["date"].Value);
                string gradeLevel = dgvEventList.Rows[selectedIndex].Cells["gradelevel"].Value.ToString();
                string fines = dgvEventList.Rows[selectedIndex].Cells["fines"].Value.ToString();

               
                EditEventForm editForm = new EditEventForm();
                editForm.InitializeData(eventName, location, date, gradeLevel, fines);
                editForm.ShowDialog();

              
                LoadData();
            }
            else
            {
                MessageBox.Show("Please select a row to edit.");
            }
        }
    }
    }

