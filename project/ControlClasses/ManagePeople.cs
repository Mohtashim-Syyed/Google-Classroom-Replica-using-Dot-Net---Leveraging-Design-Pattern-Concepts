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
    class ManagePeople
    {
        public static List<Teacher> teacher = new List<Teacher>();
        public static List<Student> students = new List<Student>();
        public static List<Student> s1 = new List<Student>();
        public static List<Student> studentFullObject = new List<Student>();

        public ManagePeople(bool Refresh)
        {
            if (Refresh)
            {

                teacher = new List<Teacher>();//teacher name
                students = new List<Student>();//student names
                s1 = new List<Student>();//studentids
                studentFullObject = new List<Student>();//full obj name+id
            }
        }
        Teacher t;
        Student s;
        public ManagePeople()
        {

        }

        public void loadTeacher(int courseId)
        {


            using (SqlCommand cmd = new SqlCommand("GetTeacherNameByCourseId", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@courseId", courseId);
                SqlParameter result = cmd.Parameters.Add("@teacherName", SqlDbType.VarChar);
                result.Direction = ParameterDirection.ReturnValue;
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    t = new Teacher();
                    t.teacherName = dataReader.GetString(0);
                    teacher.Add(t);
                }
                dataReader.Close();
            }

        }
        public void getStudentIds(int courseId)
        {
            using (SqlCommand cmd = new SqlCommand("GetStudentIdsByCourseId", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@courseId", courseId);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {

                    s = new Student();
                    s.studentId = sdr.GetInt32(0);
                    s1.Add(s);



                }
                sdr.Close();


            }
        }
        public void getStudentNames()
        {
            for (int i = 0; i < s1.Count; i++)
            {
                using (SqlCommand command = new SqlCommand("GetStudentNameByStudentId", Connection.Connect()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@studentId", s1[i].studentId);

                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                    {
                        s = new Student();//Aggregatio by Ref...ce
                        s.studentName = dataReader.GetString(0);
                        students.Add(s);

                    }
                    dataReader.Close();


                }
            }

        }


        /// <summary>
        /// id ki alg list thi 
        /// names ki alg list thi 
        /// dono ko consecitively utha k ek obj bana dia ha 
        /// q k student kay name user k table say uthanay partay ..lambi hoti 
        /// </summary>
        public void fillFullObj(List<Student> id,List<Student> name)
        {
            studentFullObject = new List<Student>();
            for (int i = 0; i < id.Count; i++)
            {
                s = new Student();
                s.studentId = id[i].studentId;
                s.studentName = name[i].studentName;
                studentFullObject.Add(s);
            }
        }
        Names n;
        public void showPeople(FlowLayoutPanel f1, FlowLayoutPanel f2)
        {
            n = new Names();
            n.gunaLabel1.Text = teacher[0].teacherName;
            n.Margin = new Padding(4, 4, 4, 4);
            f1.Controls.Add(n);

            for (int i = 0; i < students.Count; i++)
            {
                n = new Names();
                n.gunaLabel1.Text = students[i].studentName;
                n.Margin = new Padding(4, 4, 4, 4);
                f2.Controls.Add(n);
            }


            fillFullObj(s1, students);

        }

    }
}
