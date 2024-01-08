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
   

    public partial class StudList : Form
    {
        private MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        private const string ConnectionString = "datasource=localhost;port=3306;username=root;password=";
        private DataTable originalDataTable = new DataTable();
        private bool isSearchBoxEmpty = true;

        public StudList()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            dgvStudList.Refresh();

            LoadData();
            dgvStudList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudList.MultiSelect = false;
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

       

        private DataTable LoadData(string searchTerm = "")
        {
            DataTable dataTable = new DataTable();

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "SELECT * FROM user.student_info";
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

            dgvStudList.DataSource = dataTable;
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("File not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return dataTable;
        }
        private void DeleteRecord(string firstname, string lastname)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "DELETE FROM user.student_info WHERE firstname = @firstname AND lastname = @lastname";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstname", firstname);
                    command.Parameters.AddWithValue("@lastname", lastname);


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


        private void dgvStudList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //if (e.RowIndex >= 0 && e.ColumnIndex >= dgvStudList.Columns.Count - 2)
            //{

            //    DataGridViewRow selectedRow = dgvStudList.Rows[e.RowIndex];
            //    string firstname = selectedRow.Cells["firstname"].Value.ToString();
            //    string lastname = selectedRow.Cells["lastname"].Value.ToString();

                
            //    if (e.ColumnIndex == dgvStudList.Columns.Count - 2)
            //    {
                   
            //        UpdateAttendance(firstname, lastname, "Present");
            //    }
            //    else if (e.ColumnIndex == dgvStudList.Columns.Count - 1)
            //    {
                   
            //        UpdateAttendance(firstname, lastname, "Absent");
            //    }
            //}
        }
        //private void UpdateAttendance(string firstname, string lastname, string status)
        //{
        //    try
        //    {
        //        if (connection.State != ConnectionState.Open)
        //            connection.Open();


        //        string checkQuery = $"SELECT * FROM user.attendance_rec WHERE firstname = '{firstname}' AND lastname = '{lastname}'";
        //        using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
        //        {
        //            using (MySqlDataReader reader = checkCommand.ExecuteReader())
        //            {
        //                if (!reader.Read())
        //                {
                            
        //                    string insertQuery = $"INSERT INTO user.attendance_rec (firstname, lastname, status) VALUES ('{firstname}', '{lastname}', '{status}')";
        //                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
        //                    {
        //                        insertCommand.ExecuteNonQuery();
        //                    }
        //                }
        //                else
        //                {

        //                    string updateQuery = $"UPDATE user.attendance_rec SET status = '{status}' WHERE firstname = '{firstname}' AND lastname = '{lastname}'";
        //                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
        //                    {
        //                        updateCommand.ExecuteNonQuery();
        //                    }
        //                }
        //            }
        //        }


        //        LoadData();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error updating attendance: " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (connection.State == ConnectionState.Open)
        //            connection.Close();
        //    }
        //}
        private void lblAddStudent_Click(object sender, EventArgs e)
        {
            AddStudentcs ad = new AddStudentcs(dgvStudList);
            ad.Show();
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pbSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text;
            LoadData(searchTerm);

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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblAttRec_Click(object sender, EventArgs e)
        {
            AttendanceRec ats = new AttendanceRec();
            ats.Show();
            this.Hide();
        }

        private void lblStudList_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvStudList.SelectedRows[0];
                string firstname = selectedRow.Cells["firstname"].Value.ToString();
                string lastname = selectedRow.Cells["lastname"].Value.ToString();


                
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    DeleteRecord(firstname, lastname);

                    
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
            if (dgvStudList.SelectedRows.Count > 0)
            {
                
                DataGridViewRow selectedRow = dgvStudList.SelectedRows[0];
                string firstname = selectedRow.Cells["firstname"].Value.ToString();
                string lastname = selectedRow.Cells["lastname"].Value.ToString();
                string contactNo = selectedRow.Cells["contactNo"].Value.ToString();
                string address = selectedRow.Cells["address"].Value.ToString();
                string gradelevel = selectedRow.Cells["gradelevel"].Value.ToString();
                string section = selectedRow.Cells["section"].Value.ToString();

                
                AddStudentcs adSt = new AddStudentcs(dgvStudList, firstname, lastname, contactNo, address, gradelevel, section);

                
                adSt.ShowDialog();


                LoadData();
            }
            else
            {
                MessageBox.Show("Please select a row to edit.");
            }

        }

    }
}