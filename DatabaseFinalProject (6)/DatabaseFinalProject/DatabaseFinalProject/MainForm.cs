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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

      

        private void lblManEvent_Click(object sender, EventArgs e)
        {

            ManageEvent me = new ManageEvent();
            me.Show();
            this.Hide();

        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

      

        private void lblStudList_Click(object sender, EventArgs e)
        {
            StudList sl = new StudList();
            sl.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void lblAttRec_Click(object sender, EventArgs e)
        {
            AttendanceRec at = new AttendanceRec();
            at.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }   
}

