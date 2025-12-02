using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.EntityClasses
{
    class Material
    {
        public int materialId { get; set; }
        public int courseId { get; set; }
        public string materialCatagory { get; set; }
        public string materialDiscription { get; set; }
        public string materialDueDate { get; set; }
        public string materialPostedDate { get; set; }
        public string materialEditDate { get; set; }
        public string materialFilePath { get; set; }
        public int materialPoints { get; set; }
        public string materialTitle { get; set; }
        public Material()
        {

        }
        public Material(int materialId, int courseId, string materialCatagory, string materialDiscription, string materialDueDate, string materialPostedDate, string materialEditDate, string materialFilePath, int materialPoints, string materialTitle)
        {

        }
    }
}
