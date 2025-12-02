using Classroom.EntityClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Guna.UI.WinForms;


namespace Classroom.ControlClasses
{
    class ManageUser
    {
        User u;
        public Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        public Regex NameRegex = new Regex(@"^[a-zA-Z\s]+$");
        public ManageUser()
        {

            //do nothing ...
        }

        public ManageUser(GunaTextBox userName, GunaTextBox userEmail, GunaTextBox userPass)
        {
            if (!string.IsNullOrEmpty(userName.Text) && !string.IsNullOrEmpty(userEmail.Text))
            {
                if (authenticateUser(userName.Text, userEmail.Text) == true)
                {
                    if (userExist == false)
                    {
                        u = new User(userName.Text, userEmail.Text, userPass.Text);
                        setData();
                    }
                    else
                    {

                        MessageBox.Show("UserName exist plese try any other Name");
                    }
                }
                else
                {
                    MessageBox.Show("Rules Voilation");
                }

            }
            else
            {
                MessageBox.Show("Fill Properly");
            }
        }


        public void setData()
        {
            using (SqlCommand cmd = new SqlCommand("RegesterUser", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userName", u.userName);
                cmd.Parameters.AddWithValue("@userEmail", u.userEmail);
                cmd.Parameters.AddWithValue("@userPassword", u.userPassword);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Regestre successful...");
            }
        }

        /// <summary>
        /// cheaking user exist or not 
        /// </summary>
        bool userExist = false;
        public bool authenticateUser(string userName, string userEmail)
        {
            bool isClear = false;
            Match matchEmail = EmailRegex.Match(userEmail);
            Match matchName = NameRegex.Match(userName);
            if (matchEmail.Success && matchName.Success)
            {
                isClear = true;
            }

            using (SqlCommand cmd = new SqlCommand("userExist", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("userName", userName);

                var returnParameter = cmd.Parameters.Add("@Exists", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();
                var result = returnParameter.Value;
                var a = 0;
                if (result == (object)a)
                {

                    userExist = true;
                }
            }


            return isClear;
        }

        public ManageUser(GunaTextBox userEmail, GunaTextBox userPass)
        {
            u = new User(userEmail.Text, userPass.Text);
            cheakUser();
        }
        /// <summary>
        /// cheak user details at login time
        /// </summary>
        public void cheakUser()
        {
            User usr = new User(true);// to refresh all static list in userclass
            using (SqlCommand cmd = new SqlCommand("LoginUser", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userEmail", u.userEmail);
                cmd.Parameters.AddWithValue("@userPassword", u.userPassword);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    u = new User(sdr.GetInt32(0), sdr.GetString(1), sdr.GetString(2), sdr.GetString(3));
                    User.currentUser = u;
                    sdr.Close();
                    getUserAsStudent();
                    getUserAsTeacher();
                    ManageCourse mng = new ManageCourse("Run");
                    Form1 DashBoard = new Form1();
                    DashBoard.Show();
                }
                else
                {
                    MessageBox.Show("login failed");
                    sdr.Close();
                }
            }
        }
        Student std;
        public void getUserAsStudent()
        {
            using (SqlCommand cmd = new SqlCommand("GetUserAsStudent", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", u.currentUserIdPK);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    std = new Student();
                    std.userId = sdr.GetInt32(0);
                    std.studentName = sdr.GetString(1);
                    std.studentId = sdr.GetInt32(2);
                    sdr.Close();
                    User.userAsStudent = std;
                }
                sdr.Close();

            }
        }
        Teacher tec;

        public void getUserAsTeacher()
        {
            using (SqlCommand cmd = new SqlCommand("GetUserAsTeacher", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", u.currentUserIdPK);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    tec = new Teacher();
                    tec.userId = sdr.GetInt32(0);
                    tec.teacherName = sdr.GetString(1);
                    tec.teacherId = sdr.GetInt32(2);
                    sdr.Close();
                    User.userAsTeacher = tec;
                }
                sdr.Close();

            }
        }

    }
}
