using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.EntityClasses
{
    class Student
    {
        public int studentId { get; set; }
        public int userId { get; set; }
        public string studentName { get; set; }
        public Student()
        {

        }
        public Student(int id)
        {
            this.userId = id;
        }
    }
}
