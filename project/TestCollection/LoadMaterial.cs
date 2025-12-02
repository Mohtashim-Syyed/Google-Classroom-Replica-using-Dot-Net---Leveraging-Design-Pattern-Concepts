using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Classroom.TestCollection
{
    class LoadMaterial
    {
        public static string[] names = { "Quiz 1", "Assignment 1", "Quiz 2", "Lecture 1" };
        public static string[] dates = { "Feb 18 (Edited Feb 19)", "Feb 20", "Feb 21 (Edited Feb 22)", "Feb 25" };
        public static string[] userNames = { "Yousuf", "Murad", "Ilsa", "Mohtashim", "Ahmed" };
        public static void load1(FlowLayoutPanel f)
        {

            for (int j = 0; j < 3; j++)
            {

                for (int i = 0; i < names.Length; i++)
                {
                    materialBar m = new materialBar();
                    m.gunaLabel1.Text = (i + 1) + " " + names[i];
                    m.gunaLabel2.Text = dates[i];
                    if (i % 2 == 0)
                    {

                        m.BackColor = Color.FromArgb(120, 40, 75);
                    }
                    else
                    {
                        m.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    m.Margin = new Padding(4, 4, 4, 4);
                    f.Controls.Add(m);

                }

            }



        }
        //----------------------------------------------------------
        //ya puchna  ha ka class main kis tarhan define karun mayhod
        public static void load2(FlowLayoutPanel f)
        {
            ClassworkUC c = new ClassworkUC();
            for (int i = 0; i < names.Length; i++)
            {
                DetailedMaterialBar m = new DetailedMaterialBar();
                m.gunaLabel1.Text = (i + 1) + " " + names[i];
                m.gunaLabel2.Text = dates[i];
                m.Margin = new Padding(4, 4, 4, 4);
                m.Click += new System.EventHandler(c.pClick);
                f.Controls.Add(m);

            }


        }
        DetailedMaterialBar current;
        public void pClick(Object sender, EventArgs e)
        {
            current = (DetailedMaterialBar)sender;
        }
        //--------------------------------------------------------

        public static void loadNames(FlowLayoutPanel teacher, FlowLayoutPanel classmate)
        {
            for (int i = 0; i < names.Length; i++)
            {
                Names m = new Names();
                m.gunaLabel1.Text = (i + 1) + " " + userNames[i];


                m.Margin = new Padding(4, 4, 4, 4);
                teacher.Controls.Add(m);

            }
            for (int i = 0; i < names.Length; i++)
            {
                Names m = new Names();
                m.gunaLabel1.Text = (i + 1) + " " + userNames[i];


                m.Margin = new Padding(4, 4, 4, 4);
                classmate.Controls.Add(m);

            }
        }
        public static void loadClasses(FlowLayoutPanel panel)
        {
            for (int i = 0; i < names.Length; i++)
            {
                ClassImage m = new ClassImage();
                m.gunaLabel3.Text = userNames[i];


                m.Margin = new Padding(8, 8, 8, 8);
                panel.Controls.Add(m);

            }
        }
    }
}
