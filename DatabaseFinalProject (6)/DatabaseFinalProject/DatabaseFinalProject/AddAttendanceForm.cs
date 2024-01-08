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
    public partial class AddAttendanceForm : Form
    {
        public DataGridView dgvAttRec { get; set; }
        private MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        private const string ConnectionString = "datasource=localhost;port=3306;username=root;password=";
        private bool isEditing = false;
        private bool isAddingAttendance = false;


        public AddAttendanceForm(DataGridView dgvAttRec, string firstname = "", string lastname = "", string gradelevel = "", string section = "", DateTime date = default, string eventname = "", string attendance = "", string fines = "")
        {

            InitializeComponent();
            this.dgvAttRec = dgvAttRec != null ? dgvAttRec : new DataGridView();


            LoadData();
          
            if (!string.IsNullOrEmpty(firstname))
            {
                isEditing = true;

                txtFname.Text = firstname;
                txtLastName.Text = lastname;
                cbxGLevel.Text = gradelevel;
                txtSection.Text = section;
                dtpdate.Value = date != DateTime.MinValue ? date : DateTimePicker.MinimumDateTime;
                txteventname.Text = eventname;
                cbxAtt.Text = attendance;
                txtfines.Text = fines;
            }
            if (!string.IsNullOrEmpty(firstname))
            {
                isAddingAttendance = true;

                txtFname.Text = firstname;
                txtLastName.Text = lastname;
                cbxGLevel.Text = gradelevel;
                txtSection.Text = section;
                dtpdate.Value = date != DateTime.MinValue ? date : DateTimePicker.MinimumDateTime;
                txteventname.Text = eventname;
                cbxAtt.Text = attendance;
                txtfines.Text = fines;
            }
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

                        
                        dgvAttRec.DataSource = dataTable;
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
        private void UpdateRecord(string firstname, string lastname, DateTime date, string gradelevel, string section, string eventname, string attendance, string fines)
        {
            try
            {
                string query = "UPDATE user.attendance_rec " +
                               "SET gradelevel = @gradelevel, section = @section, eventname = @eventname, attendance = @attendance, fines = @fines " +
                               "WHERE firstname = @firstname AND lastname = @lastname AND date = @date";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstname", firstname);
                    command.Parameters.AddWithValue("@lastname", lastname);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@gradelevel", gradelevel);
                    command.Parameters.AddWithValue("@section", section);
                    command.Parameters.AddWithValue("@eventname", eventname);
                    command.Parameters.AddWithValue("@attendance", attendance);
                    command.Parameters.AddWithValue("@fines", fines);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message);
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            string firstname = txtFname.Text;
            string lastname = txtLastName.Text;
            string gradelevel = cbxGLevel.Text;
            string section = txtSection.Text;
            DateTime date = dtpdate.Value;
            string eventname = txteventname.Text;
            string attendance = cbxAtt.Text;
            string fines = txtfines.Text;

            try
            {
                connection.Open();

                if (isEditing)
                {
                   
                    UpdateRecord( firstname, lastname, date, gradelevel, section, eventname, attendance, fines);

                    MessageBox.Show("Information Updated");
                }
                else
                {
                    
                    InsertRecord(firstname, lastname, date, gradelevel, section, eventname, attendance, fines);
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

       


        private void InsertRecord(string firstname, string lastname, DateTime date, string gradelevel, string section, string eventname, string attendance, string fines)
        {
            try
            {
                string query = "INSERT INTO user.attendance_rec (firstname, lastname, date, gradelevel, section, eventname, attendance, fines) " +
                               "VALUES (@firstname, @lastname, @date, @gradelevel, @section, @eventname, @attendance, @fines)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstname", firstname);
                    command.Parameters.AddWithValue("@lastname", lastname);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@gradelevel", gradelevel);
                    command.Parameters.AddWithValue("@section", section);
                    command.Parameters.AddWithValue("@eventname", eventname);
                    command.Parameters.AddWithValue("@attendance", attendance);
                    command.Parameters.AddWithValue("@fines", fines);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting record: " + ex.Message);
            }
        }
    }
}

