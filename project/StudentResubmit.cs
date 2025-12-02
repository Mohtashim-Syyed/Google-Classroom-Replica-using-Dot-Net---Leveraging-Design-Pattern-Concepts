using Classroom.ControlClasses;
using Classroom.EntityClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Classroom
{
    public partial class StudentResubmit : Form
    {
        public StudentResubmit()
        {
            InitializeComponent();
        }

        private void StudentResubmit_Load(object sender, EventArgs e)
        {
            ManageMaterial mm = new ManageMaterial();
            mm.fillResubmittionDetails(gunaLabel1, gunaLabel11, panel5, bunifuCustomLabel1, gunaLabel2, gunaGroupBox1, gunaLabel6,bunifuCustomLabel2);
            bunifuFlatButton1.Hide();
            bunifuFlatButton2.Hide();
           // dateTimePicker1.Hide();
            progressBar1.Hide();
            dateTimePicker1.Value = DateTime.Now;
            //progressBar1.Hide();

            DateTime dt2 = Convert.ToDateTime(bunifuCustomLabel2.Text.Trim()).Date;
            if (dateTimePicker1.Value > dt2)
            {
                gunaLabel3.Show();
                gunaLabel3.Text = "Missing";
                gunaLabel3.ForeColor = Color.Red;
            }
            else
            {
                gunaLabel3.Hide();
            }

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            bunifuFlatButton3.Hide();
            bunifuFlatButton1.Show();
            bunifuFlatButton2.Show();
            gunaLabel3.Hide();
            panel1.Hide();
            DateTime dt2 = Convert.ToDateTime(bunifuCustomLabel2.Text.Trim()).Date;
            if (dateTimePicker1.Value > dt2)
            {
                gunaLabel3.Show();
                gunaLabel3.Text = "Missing";
                gunaLabel3.ForeColor = Color.Red;
            }
            else
            {
                gunaLabel3.Hide();
            }
        }
        string a,path = "";
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

            progressBar1.Show();
            timer1.Start();
            if (gunaLabel3.Text=="Missing")
            {
                a = "TurnedIn Late";
            }
            else
            {

                a = "Turned In";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Increment(1);

            }
            else
            {
                timer1.Stop();
                progressBar1.Hide();
                ManageMaterial mm = new ManageMaterial();
                mm.resubmit(dateTimePicker1.Value, path,ManageMaterial.reSubmittied.submittionId,a);
                bunifuFlatButton1.Hide();
                gunaLabel3.Text = a;
                bunifuFlatButton2.Text = a;
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            path = ofd.FileName;
            gunaLabel6.Text = System.IO.Path.GetFileName(ofd.FileName);
            panel1.Show();
        }

        private void gunaGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PDF_FORM pdf = new PDF_FORM();
            ManageMaterial.openFile = gunaLabel11.Text;
            pdf.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PDF_FORM pdf = new PDF_FORM();
            ManageMaterial.openFile = gunaLabel6.Text;
            pdf.Show();
        }
    }
}
