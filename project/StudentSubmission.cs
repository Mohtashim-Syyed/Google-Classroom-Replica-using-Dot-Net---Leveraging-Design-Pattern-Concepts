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
    public partial class StudentSubmission : Form
    {
        public StudentSubmission()
        {
            InitializeComponent();
        }

        private void StudentSubmission_Load(object sender, EventArgs e)
        {
            ManageMaterial mm = new ManageMaterial();
            mm.fillSubmittionDetails(gunaLabel1, gunaLabel11, panel5, bunifuCustomLabel1, gunaLabel2, gunaGroupBox1, bunifuCustomLabel2);
            panel1.Hide();
            gunaLabel3.Hide();
            bunifuFlatButton2.Hide();
            gunaDateTimePicker2.Value = DateTime.Now;
            progressBar1.Hide();
            DateTime dt2 = Convert.ToDateTime(bunifuCustomLabel2.Text.Trim()).Date;
            if (gunaDateTimePicker2.Value > dt2)
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
        string path = "";
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            path = ofd.FileName;
            gunaLabel6.Text = System.IO.Path.GetFileName(ofd.FileName);
            panel1.Show();
            bunifuFlatButton2.Show();

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)

        {
            progressBar1.Show();
            timer1.Start();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PDF_FORM pdf = new PDF_FORM();
            ManageMaterial.openFile = gunaLabel11.Text;
            pdf.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Increment(1);

            }
            else
            {
                string a = "";
                if (gunaLabel3.Text == "Missing")
                {
                    a = "TurnedIn Late";
                }
                else
                {
                    a = "TurnedIn";
                }
                timer1.Stop();
                progressBar1.Hide();
                ManageMaterial mm = new ManageMaterial();
                mm.studentSubmits(int.Parse(gunaLabel2.Text), User.userAsStudent.studentId, Course.currentCourse.courseIdPK, path, a, gunaDateTimePicker2.Value, -1, gunaLabel3);
                bunifuFlatButton1.Hide();
                bunifuFlatButton2.Text = a;
                gunaLabel3.Text = a;
                gunaLabel3.ForeColor = Color.Green;
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void gunaDateTimePicker1_Leave(object sender, EventArgs e)
        {
            //DateTime dt2 = Convert.ToDateTime(bunifuCustomLabel2.Text.Trim()).Date;
            //if (gunaDateTimePicker1.Value > dt2)
            //{
            //    gunaLabel3.Show();
            //    gunaLabel3.Text = "Missing";
            //    gunaLabel3.ForeColor = Color.Red;
            //}
            //else
            //{
            //    gunaLabel3.Show();

            //    gunaLabel3.Text = "temp";
            //    gunaLabel3.ForeColor = Color.Green;
            //}
        }
    }
}
