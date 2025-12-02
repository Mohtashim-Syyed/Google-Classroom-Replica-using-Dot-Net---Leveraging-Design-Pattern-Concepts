using Classroom.Com;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.EntityClasses
{
    class User
    {
        public int currentUserIdPK { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userPassword { get; set; }

        public static User currentUser;
        public static int isStudy { get; set; }


        public static Student userAsStudent = new Student();

        public static Teacher userAsTeacher = new Teacher();

        public static List<Course> userTeach = new List<Course>();

        public static List<Course> userEnroll = new List<Course>();

        //Asssosiation Class...
        public static List<Student_Course> StudentCourse = new List<Student_Course>();

        public User(bool Refresh)
        {
            if (Refresh)
            {
                currentUser = new User();
                userAsStudent = new Student();
                userAsTeacher = new Teacher();
                userTeach = new List<Course>();
                userEnroll = new List<Course>();
                StudentCourse = new List<Student_Course>();
            }
        }

        public User()
        {

        }
        public User(string name, string email, string password)
        {
            this.userName = name;
            this.userEmail = email;
            this.userPassword = Security.Encryption(password);
        }
        public User(string email, string password)
        {
            this.userEmail = email;
            this.userPassword = Security.Encryption(password);
        }
        public User(int userid, string name, string email, string password)
        {
            this.currentUserIdPK = userid;
            this.userName = name;
            this.userEmail = email;
            this.userPassword = password;
            //   currentUser = new User();
        }
    }
}
