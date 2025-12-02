using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classroom.ControlClasses;
using Classroom.EntityClasses;

namespace Classroom
{
    public partial class StuffUC : UserControl
    {

        Label l;
        public StuffUC()
        {
            InitializeComponent();
        }

        private void StuffUC_Load(object sender, EventArgs e)
        {
            gunaDateTimePicker2.Value = DateTime.Now;
           // gunaDateTimePicker2.Hide();
            panel4.Hide();

         //  gunaDateTimePicker3.Hide();
         //  gunaDateTimePicker3.Value = new DateTime(2000, 01, 01);
            l = new Label();
            l.Text = "No File Choosen";

        }

        private void gunaGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void gunaGroupBox2_Click(object sender, EventArgs e)
        {

        }
        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            
            ManageMaterial m = new ManageMaterial();
           // lecturer
            if (string.IsNullOrEmpty(gunaTextBox3.Text))
            {
                                                                                                      //duedate                   //posteddate
                m.setMaterial(Course.currentCourse.courseIdPK, gunaComboBox1.Text, gunaTextBox2.Text, gunaDateTimePicker1.Value, gunaDateTimePicker2.Value, gunaDateTimePicker2.Value, l.Text, -1, gunaTextBox1.Text);
            }
            else
            {
                m.setMaterial(Course.currentCourse.courseIdPK, gunaComboBox1.Text, gunaTextBox2.Text, gunaDateTimePicker1.Value, gunaDateTimePicker2.Value, gunaDateTimePicker2.Value, l.Text, int.Parse(gunaTextBox3.Text), gunaTextBox1.Text);

            }
            gunaTextBox3.Text = string.Empty;
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            l.Text = ofd.FileName;
            string fileName = System.IO.Path.GetFileName(ofd.FileName);
            gunaLabel4.Text = fileName;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaTextBox1_Enter(object sender, EventArgs e)
        {
            if (gunaTextBox1.Text=="Title...")
            {
                gunaTextBox1.Text = "";

            }
        }

        private void gunaTextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(gunaTextBox1.Text))
            {
                gunaTextBox1.Text = "Title...";
            }
        }

        private void gunaTextBox2_Enter(object sender, EventArgs e)
        {
            //Instructions(optional)
            if (gunaTextBox2.Text == "Instructions(optional)")
            {
                gunaTextBox2.Text = "";

            }
        }

        private void gunaTextBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(gunaTextBox2.Text))
            {
                gunaTextBox2.Text = "Instructions(optional)";
            }
        }

        private void gunaDateTimePicker1_Leave(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Now.Date;
            DateTime dt2 = Convert.ToDateTime(gunaDateTimePicker1.Text.Trim()).Date;
            if (dt1 <= dt2)
            {
                //nothing...
            }
            else
            {
                MessageBox.Show("Invalid Date...");
            }
        }

        private void gunaComboBox1_Leave(object sender, EventArgs e)
        {
           
        }

        private void gunaComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gunaComboBox1.Text == "Assignment" || gunaComboBox1.Text == "Quiz")
            {
                panel4.Show();
            }
            else
            {
                panel4.Hide();
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
