using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.EntityClasses
{
    class Teacher
    {
        public int teacherId { get; set; }
        public int userId { get; set; }
        public string teacherName { get; set; }
        public Teacher()
        {

        }
        public Teacher(int userId)
        {
            this.userId = userId;

        }

    }
}
