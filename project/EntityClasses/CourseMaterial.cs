using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.EntityClasses
{
    class CourseMaterial
    {
        public int submittionId { get; set; }
        public int materialId { get; set; }
        public int courseId { get; set; }
        public string materialCatagory { get; set; }
        public string materialDiscription { get; set; }
        public DateTime materialDueDate { get; set; }
        public DateTime materialPostedDate { get; set; }
        public DateTime materialEditDate { get; set; }
        public string materialFilePath { get; set; }
        public int materialPoints { get; set; }
        public string materialTitle { get; set; }

        //For student Submittion....
        public string materialStatus { get; set; }
        public int studentId { get; set; }
        public CourseMaterial()
        {

        }
        public CourseMaterial(int materialId, int studentId)
        {
            this.materialId = materialId;
            this.studentId = studentId;
        }
        public CourseMaterial(int courseId, string materialCatagory, string materialDiscription, DateTime materialDueDate, DateTime materialPostedDate, DateTime materialEditDate, string materialFilePath, int materialPoints, string materialTitle)
        {
            this.courseId = courseId;
            this.materialCatagory = materialCatagory;
            this.materialDiscription = materialDiscription;
            this.materialDueDate = materialDueDate;
            this.materialPostedDate = materialPostedDate;
            this.materialEditDate = materialEditDate;
            this.materialFilePath = materialFilePath;
            this.materialPoints = materialPoints;
            this.materialTitle = materialTitle;

        }
        public CourseMaterial(int materialId, int studentId, int courseId, string filePath, string status, DateTime date, int marks)
        {
            this.materialId = materialId;
            this.studentId = studentId;
            this.courseId = courseId;
            this.materialFilePath = filePath;
            this.materialStatus = status;
            this.materialPostedDate = date;
            this.materialPoints = marks;
        }
        public CourseMaterial(string filePath, DateTime date,string status)
        {
            this.materialFilePath = filePath;
            this.materialPostedDate = date;
            this.materialStatus = status;
        }
    }
}
