using Classroom.EntityClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Classroom.ControlClasses
{
    class ManageCourse
    {
        Student s;
        Course c, c1, c2;
        Teacher t;
        Student_Course std_course;
        public ManageCourse()
        {

        }
        public ManageCourse(string Run)
        {
            getTeacherCourses();
            getStudentCourses();
            loadStudentCourses();
        }
        public void joinClass(string ClassCode)
        {
            if (CheakStudentExistInCourse(ClassCode) == true)
            {
                using (SqlCommand cmd = new SqlCommand("joinClass", Connection.Connect()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@joinCode", ClassCode);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        c = new Course(sdr.GetInt32(0).ToString(), sdr.GetInt32(1).ToString(), sdr.GetString(2), sdr.GetString(3), sdr.GetInt32(5).ToString());
                        sdr.Close();

                        createStudent();
                        setStudentInCourse(s, c);

                    }
                    else
                    {
                        MessageBox.Show("No Class Found...!");
                        sdr.Close();
                    }

                }
            }
            else
            {
                MessageBox.Show("You are already a student");
            }

        }

        public bool CheakStudentExistInCourse(string ClassCode)
        {
            bool a = true;
            using (SqlCommand cmd = new SqlCommand("CheakStudentExistInCourse", Connection.Connect()))
            {
                if (User.userAsStudent.studentId != null)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@courseCode", ClassCode);
                    cmd.Parameters.AddWithValue("@studentId", User.userAsStudent.studentId);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        a = false;
                    }

                    sdr.Close();


                }
                return a;

            }
        }
        public void createStudent()
        {
            s = new Student(getId());
            if (CheakStudentExist() == 0)
            {
                using (SqlCommand cmd = new SqlCommand("CreateStudent", Connection.Connect()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", s.userId);
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("getStudentId", Connection.Connect()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", s.userId);
                    var returnParameter = cmd.Parameters.Add("@StudentId", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    cmd.ExecuteNonQuery();
                    var result = returnParameter.Value;
                    if (result != null)
                    {
                        s.studentId = (int)result;
                    }
                }
            }
            else
            {
                s.studentId = CheakStudentExist();
            }
        }
        public void setStudentInCourse(Student s, Course c)
        {
            using (SqlCommand cmd = new SqlCommand("SetStudentInCourse", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@studentId", s.studentId);
                cmd.Parameters.AddWithValue("@courseId", c.courseIdPK);
                cmd.ExecuteNonQuery();
                MessageBox.Show("You Are InRolled...");
            }
        }

        public static int getId()
        {
            return User.currentUser.currentUserIdPK;
        }

        public void createTeacher()
        {
            t = new Teacher(getId());
            if (CheakTeacherExist() == 0)
            {
                using (SqlCommand cmd = new SqlCommand("CreateTeacher", Connection.Connect()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", t.userId);
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("getTeacherId", Connection.Connect()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", t.userId);
                    var returnParameter = cmd.Parameters.Add("@TeacherId", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    cmd.ExecuteNonQuery();
                    var result = returnParameter.Value;


                    if (result != null)
                    {
                        t.teacherId = (int)result;
                    }
                }
            }
            else
            {
                t.teacherId = CheakTeacherExist();
            }
        }

        //  Course c;
        public void setDataInCourse(string classId, string courseName, string courseCode, Teacher t)
        {
            c = new Course(classId, courseName, courseCode, t);


            using (SqlCommand cmd = new SqlCommand("CreateClass", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@classID", c.classId);
                cmd.Parameters.AddWithValue("@courseName", c.courseName);
                cmd.Parameters.AddWithValue("@courseCode", c.courseCode);
                cmd.Parameters.AddWithValue("@teacherID", c.teacherId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Class Created SuccessFully...");

            }
        }
        public Teacher returnTeacher()
        {
            return t;
        }
        public int CheakTeacherExist()
        {
            int returnValue = 0;
            using (SqlCommand cmd = new SqlCommand("CheakTeacherExist", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", t.userId);
                var returnParameter = cmd.Parameters.Add("@Exists", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                var result = returnParameter.Value;
                if ((int)result == 0)
                {
                    returnValue = 0;
                }
                else
                {
                    returnValue = (int)result;
                }
            }

            return returnValue;
        }

        public int CheakStudentExist()
        {
            int returnValue = 0;
            using (SqlCommand cmd = new SqlCommand("CheakStudentExist", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", s.userId);
                var returnParameter = cmd.Parameters.Add("@Exists", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                var result = returnParameter.Value;
                if ((int)result == 0)
                {
                    returnValue = 0;
                }
                else
                {
                    returnValue = (int)result;
                }
            }

            return returnValue;
        }

        public void getTeacherCourses()
        {

            if (User.userAsTeacher.teacherId != null)
            {
                using (SqlCommand cmd = new SqlCommand("GetTeacherCourses", Connection.Connect()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@teacherId", User.userAsTeacher.teacherId);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        c1 = new Course(sdr.GetInt32(0).ToString(), sdr.GetInt32(1).ToString(), sdr.GetString(2), sdr.GetString(3), sdr.GetInt32(5).ToString());
                        User.userTeach.Add(c1);
                    }
                    sdr.Close();


                }
            }
        }

        public void getStudentCourses()
        {
            if (User.userAsStudent.studentId != null)
            {
                using (SqlCommand cmd = new SqlCommand("GetStudentCourses", Connection.Connect()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studentId", User.userAsStudent.studentId);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        std_course = new Student_Course();
                        std_course.courseId = sdr.GetInt32(1);
                        std_course.studentId = sdr.GetInt32(0);
                        User.StudentCourse.Add(std_course);
                    }
                    sdr.Close();


                }
            }

        }
        public void loadStudentCourses()
        {
            User.userEnroll.Clear();
            for (int i = 0; i < User.StudentCourse.Count; i++)
            {
                using (SqlCommand cmd = new SqlCommand("loadStudentCourses", Connection.Connect()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@courseId", User.StudentCourse[i].courseId);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        c1 = new Course(sdr.GetInt32(0).ToString(), sdr.GetInt32(1).ToString(), sdr.GetString(2), sdr.GetString(3), sdr.GetInt32(5).ToString());
                        User.userEnroll.Add(c1);

                    }

                    sdr.Close();
                }
            }

        }

        public void loadClassBox(FlowLayoutPanel flp)
        {
            for (int i = 0; i < User.userTeach.Count; i++)
            {

                ClassImage cImg = new ClassImage();

                cImg.gunaLabel1.Text = User.userTeach[i].courseIdPK.ToString();
                cImg.gunaLabel4.Text = User.userTeach[i].courseName;
                cImg.gunaLabel3.Text = "Code: " + User.userTeach[i].courseCode;
                cImg.Margin = new Padding(8, 8, 8, 8);

                cImg.Click += new System.EventHandler(pClick);
                flp.Controls.Add(cImg);
            }
            for (int i = 0; i < User.userEnroll.Count; i++)
            {

                ClassImage cImg = new ClassImage();

                cImg.gunaLabel1.Text = User.userEnroll[i].courseIdPK.ToString();
                cImg.gunaLabel4.Text = User.userEnroll[i].courseName;
                cImg.gunaLabel3.Text = "Class Id : " + User.userEnroll[i].classId;
                cImg.Margin = new Padding(8, 8, 8, 8);
                cImg.Click += new System.EventHandler(pClick);
                flp.Controls.Add(cImg);



            }

        }

        ClassForm cf;
        ClassImage current;

        public void pClick(Object sender, EventArgs e)
        {
            bool a = false;

            ManagePeople m = new ManagePeople(true);// for refresh static lists in managePeople
            cf = new ClassForm();

            current = (ClassImage)sender;
            ManagePeople mp = new ManagePeople();
            ManageMaterial mm = new ManageMaterial();

            mp.loadTeacher(int.Parse(current.gunaLabel1.Text));
            mp.getStudentIds(int.Parse(current.gunaLabel1.Text));
            mp.getStudentNames();
            string[] array = current.gunaLabel3.Text.Split(':');
            if (array[0] == "Code")
            {
                a = true;
                User.isStudy = 0;
            }
            else
            {
                User.isStudy = 1;
            }
            c2 = new Course(current.gunaLabel4.Text, int.Parse(current.gunaLabel1.Text), current.gunaLabel3.Text, a);
            mm.getMaterial(int.Parse(current.gunaLabel1.Text));
            cf.Show();
        }
    }
}
