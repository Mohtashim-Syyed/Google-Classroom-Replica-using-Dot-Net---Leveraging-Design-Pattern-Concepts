using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.EntityClasses
{

    class Course
    {
        public static Course currentCourse { get; set; }
        public int courseIdPK { get; set; }
        public int classId { get; set; }
        public string courseName { get; set; }
        public string courseCode { get; set; }
        public int teacherId { get; set; }

        public bool isTeach;
        public Course()
        {

        }
        public Course(string courseName, int courseId, string code_teacherName, bool isTeach)  //Refresher
        {
            currentCourse = new Course();
            currentCourse.courseIdPK = courseId;
            currentCourse.courseName = courseName;
            currentCourse.courseCode = code_teacherName;
            currentCourse.isTeach = isTeach;
            // teacher ya code kuch bhi ho sakta ha ...
        }
        public Course(string classId, string courseName, string courseCode, Teacher t)
        {
            this.classId = int.Parse(classId);
            this.courseName = courseName;
            this.courseCode = courseCode;
            this.teacherId = t.teacherId;
        }
        public Course(string courseIdpk, string classId, string courseName, string courseCode, string teacherId)
        {
            this.courseIdPK = int.Parse(courseIdpk);
            this.classId = int.Parse(classId);
            this.courseName = courseName;
            this.courseCode = courseCode;
            this.teacherId = int.Parse(teacherId);
        }
      
    }
}
