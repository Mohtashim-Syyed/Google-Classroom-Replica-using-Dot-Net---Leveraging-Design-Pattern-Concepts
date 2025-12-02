using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;

namespace Classroom
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.BringToFront();
            panel2.Dock = DockStyle.Fill;
            panel3.SendToBack();
            panel4.SendToBack();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.BringToFront();
            panel3.Dock = DockStyle.Fill;
            panel2.SendToBack();
            panel4.SendToBack();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.BringToFront();
            panel4.Dock = DockStyle.Fill;
            panel3.SendToBack();
            panel2.SendToBack();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            from.Text = "khan.yousufkh@gmail.com";
            to.Text = "khan.0322kh@gmail.com";
            username.Text = "khan.yousufkh@gmail.com";
            password.Text = "codechef";


        }

        private void send_Click(object sender, EventArgs e)
        {
            int n = 10;
            string classa = "Os";
            string teacher = "Isra Khan";
            TextBox a = new TextBox();
             a.Text = "ClassName: " + classa + Environment.NewLine + "Teacher: " + teacher + Environment.NewLine + "Marks: " + n+ Environment.NewLine;
            /// a = a.Replace("@" +);
            try
            {
                MailMessage mail = new MailMessage(from.Text, to.Text, subject.Text, richTextBox1.Text+a.Text);
                SmtpClient client = new SmtpClient(smtp.SelectedItem.ToString());
                client.Port = 587;
                client.Credentials = new NetworkCredential(username.Text, password.Text);
                client.EnableSsl = true;
                mail.IsBodyHtml = Regex.IsMatch(richTextBox1.Text, @"<\s*([^ >]+)[^>]*>.*?<\s*/\s*\1\s*>");
                client.Send(mail);
                MessageBox.Show("Send Successfull");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }







        }
    }
}
