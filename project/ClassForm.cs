using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classroom.ControlClasses;
using Classroom.EntityClasses;
using Classroom.TestCollection;

namespace Classroom
{
    public partial class ClassForm : Form
    {
        public ClassForm()
        {
            InitializeComponent();
        }

        private void ClassForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            stuffUC1.Hide();
            gunaAdvenceButton1.Hide();

         //   panel3.Hide();
            streamUC1.Dock = DockStyle.Fill;
            classworkUC1.Hide();
            streamUC1.BringToFront();
            if (Course.currentCourse.isTeach)
            {

                bunifuFlatButton4.Show();
            }
            else
            {
                bunifuFlatButton4.Hide();
            }

        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f = new Form1();
            f.Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            gunaAdvenceButton1.Hide();
            panel2.Location = new Point(413, 61);
            streamUC1.Dock = DockStyle.Fill;
            streamUC1.Show();
            classworkUC2.Hide();
            //peopleUC1.Hide();
            gradesUC1.Hide();
            stdClassworkUC1.Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (Course.currentCourse.isTeach)
            {

                gunaAdvenceButton1.Show();
            }
            else
            {
                gunaAdvenceButton1.Hide();
            }
           // panel3.Hide();
            panel2.Location = new Point(559, 61);
            classworkUC2.Dock = DockStyle.Fill;
            classworkUC2.Show();
            streamUC1.Hide();
            peopleUC1.Hide();
            gradesUC1.Hide();
            stdClassworkUC1.Hide();

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            //flowLayoutPanel2.Show();
            //flowLayoutPanel3.Show();
           // panel3.Hide();
            stuffUC1.Hide();
            panel2.Location = new Point(705,61);
            peopleUC1.Dock = DockStyle.Fill;
            peopleUC1.Show();
            classworkUC2.Hide();
            streamUC1.Hide();
            gradesUC1.Hide();
            stdClassworkUC1.Hide();




        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            stuffUC1.Hide();
            gunaAdvenceButton1.Hide();
          //  panel3.Hide();
            panel2.Location = new Point(851, 61);
            gradesUC1.Dock = DockStyle.Fill;
            gradesUC1.Show();
            classworkUC2.Hide();
            streamUC1.Hide();
            peopleUC1.Hide();
            stdClassworkUC1.Hide();
        }

        //ya puchna ha 

        private void classworkUC1_Load_1(object sender, EventArgs e)
        {
            //LoadMaterial l = new LoadMaterial();
            //l.load2(flowLayoutPanel1);, 

        }


        private void detailedMaterialBar1_Load(object sender, EventArgs e)
        {

        }

        private void detailedMaterialBar1_Click(object sender, EventArgs e)
        {
            // timer2.Start();
        }
        // bool flag = false;
        private void timer2_Tick(object sender, EventArgs e)
        {
            //if (flag == false)
            //{
            //    while (current.Height < 271)
            //    {
            //        current.Height += 5;
            //    }
            //    flag = true;
            //    timer2.Stop();
            //}
            //else
            //{
            //    while (current.Height > 61)
            //    {
            //        current.Height -= 5;
            //    }
            //    flag = false;
            //    timer2.Stop();
            //}
        }

        private void peopleUC1_Load(object sender, EventArgs e)
        {
            // 1  700, 130
            // 2 700, 329
            //flowLayoutPanel2.Height = 130;
            //flowLayoutPanel2.Width = 700;
            //flowLayoutPanel3.Height = 329;
            //flowLayoutPanel3.Width = 700;
            //flowLayoutPanel2.Location = new Point(137, 57);
            //flowLayoutPanel3.Location = new Point(137, 237);

        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            stuffUC1.Dock = DockStyle.Fill;
            stuffUC1.Show();
            classworkUC2.Hide();
            streamUC1.Hide();
            peopleUC1.Hide();
            gradesUC1.Hide();
            gunaAdvenceButton1.Hide();
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            stuffUC1.Dock = DockStyle.Fill;
            stuffUC1.Show();
            classworkUC2.Hide();
            streamUC1.Hide();
            peopleUC1.Hide();
            gradesUC1.Hide();
           // panel3.Hide();
            gunaAdvenceButton1.Hide();

        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            //stdSubmittedStuff1.Dock = DockStyle.Fill;
            //stdSubmittedStuff1.Show();
            //classworkUC2.Hide();
            //// gunaAdvenceButton1.Hide();
            //stdClassworkUC1.Hide();
            //gunaAdvenceButton1.Hide();
            //panel3.Hide();
            //gunaAdvenceButton1.Hide();

        }

        private void bunifuFlatButton10_Click(object sender, EventArgs e)
        {
            stdClassworkUC1.Show();
            stdClassworkUC1.Dock = DockStyle.Fill;
            classworkUC2.Hide();
            streamUC1.Hide();
            peopleUC1.Hide();
            gradesUC1.Hide();
            stuffUC1.Hide();
           // panel3.Hide();
            gunaAdvenceButton1.Hide();

        }

        private void bunifuFlatButton11_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
