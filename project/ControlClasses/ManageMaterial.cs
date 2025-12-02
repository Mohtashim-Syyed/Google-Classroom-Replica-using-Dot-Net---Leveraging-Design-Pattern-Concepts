using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classroom.EntityClasses;
using Guna.UI.WinForms;

namespace Classroom.ControlClasses
{
    class ManageMaterial
    {
        CourseMaterial cm, cm1;
        public static CourseMaterial reSubmittied;
        public static CourseMaterial forSubmittionobj;
        public static List<materialBar> mbar { get; set; }
        public static List<GradeBarUC> gradeBar { get; set; }
        public static string openFile = "";
        public static List<DetailedMaterialBar> detailBar { get; set; }
        public static List<CourseMaterial> submittedMaterial { get; set; }
        public static List<submittedFileUC> studentFiles { get; set; }

        public ManageMaterial()
        {

        }
        public void setMaterial(int courseId, string materialCatagory, string materialDiscription, DateTime materialDueDate, DateTime materialPostedDate, DateTime materialEditDate, string materialFilePath, int materialPoints, string materialTitle)
        {
            cm = new CourseMaterial(courseId, materialCatagory, materialDiscription, materialDueDate, materialPostedDate, materialEditDate, materialFilePath, materialPoints, materialTitle);
            assignMaterial();

        }
        public void assignMaterial()
        {
            using (SqlCommand cmd = new SqlCommand("AssignMaterial", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@courseId", cm.courseId);
                cmd.Parameters.AddWithValue("@materialCatagory", cm.materialCatagory);
                cmd.Parameters.AddWithValue("@materialDiscription", cm.materialDiscription);
                cmd.Parameters.AddWithValue("@materialDueDate", cm.materialDueDate);
                cmd.Parameters.AddWithValue("@materialPostedDate", cm.materialPostedDate);
                cmd.Parameters.AddWithValue("@materialEditDate", cm.materialEditDate);
                cmd.Parameters.AddWithValue("@materialFilePath", System.IO.Path.GetFileName(cm.materialFilePath));
                cmd.Parameters.AddWithValue("@materialPoints", cm.materialPoints);
                cmd.Parameters.AddWithValue("@materialTitle", cm.materialTitle);
                cmd.ExecuteNonQuery();
                copyMaterial(cm.materialFilePath);
                MessageBox.Show("Material Created SuccessFully...");


            }
        }
        public void copyMaterial(string fullPath)
        {

            //send copy of file in folder
            if (!string.IsNullOrEmpty(fullPath) && fullPath != "No File Choosen")
            {
                string realDir = System.IO.Path.GetDirectoryName(fullPath);
                string fileName = System.IO.Path.GetFileName(fullPath);
                string projetDir = Environment.CurrentDirectory + @"\Attachments\";
                File.Copy(Path.Combine(realDir, fileName), Path.Combine(projetDir, fileName), true);
            }

        }
        ClassworkUC csuc = new ClassworkUC();
        public void getMaterial(int courseId)
        {

            mbar = new List<materialBar>();
            gradeBar = new List<GradeBarUC>();
            detailBar = new List<DetailedMaterialBar>();
            //  material = new List<CourseMaterial>();
            using (SqlCommand cmd = new SqlCommand("GetMaterialById", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@courseId", courseId);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    materialBar m = new materialBar();
                    m.gunaLabel1.Text = sdr.GetString(9);
                    m.gunaLabel2.Text = sdr.GetDateTime(5).ToString();
                    m.gunaLabel3.Text = sdr.GetString(7);
                    m.gunaLabel5.Text = sdr.GetString(2);
                    m.Margin = new Padding(4, 4, 4, 4);
                    m.gunaLabel4.Text = sdr.GetInt32(0).ToString();
                    mbar.Add(m);
                    GradeBarUC gbar = new GradeBarUC();
                    gbar.gunaLabel1.Text = sdr.GetString(9);
                    gbar.gunaLabel2.Text = sdr.GetDateTime(5).ToString();
                    //  gbar.gunaLabel3.Text = sdr.GetString(7);
                    gbar.gunaLabel5.Text = sdr.GetString(2);
                    gbar.Margin = new Padding(4, 4, 4, 4);
                    gbar.gunaLabel4.Text = sdr.GetInt32(0).ToString();
                    gradeBar.Add(gbar);
                    DetailedMaterialBar dmb = new DetailedMaterialBar();
                    dmb.gunaLabel1.Text = sdr.GetString(9);
                    dmb.gunaLabel2.Text = "Due " + sdr.GetDateTime(4).ToString();
                    dmb.gunaLabel13.Text = "Posted " + sdr.GetDateTime(5).ToString();
                    dmb.gunaLabel3.Text = sdr.GetString(3);
                    dmb.gunaLabel14.Text = sdr.GetInt32(0).ToString();
                    dmb.gunaLabel10.Text = sdr.GetInt32(0).ToString();

                    if (sdr.GetString(7) != "No File Choosen")
                    {
                        dmb.gunaLabel11.Text = sdr.GetString(7);
                    }
                    else
                    {
                        dmb.panel5.Hide();
                    }
                    if (User.isStudy == 1)
                    {
                        dmb.panel6.Hide();

                    }

                    dmb.Click += new System.EventHandler(csuc.pClick);
                    detailBar.Add(dmb);


                }
                sdr.Close();


            }
        }
        public void loadMaterial(FlowLayoutPanel flp)
        {
            for (int i = 0; i < mbar.Count; i++)
            {
                flp.Controls.Add(mbar[i]);
            }
        }
        public void loadGradeBar(FlowLayoutPanel flp)
        {
            for (int i = 0; i < gradeBar.Count; i++)
            {
                flp.Controls.Add(gradeBar[i]);
            }
        }
        public void loadDetailedMaterial(FlowLayoutPanel flp)
        {
            for (int i = 0; i < mbar.Count; i++)
            {
                flp.Controls.Add(detailBar[i]);
            }
        }
        public void studentSubmittion(int materialId)
        {
            using (SqlCommand cmd = new SqlCommand("GetMaterialByMaterialId", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@materialId", materialId);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cm = new CourseMaterial();
                    forSubmittionobj = new CourseMaterial();
                    cm.materialId = sdr.GetInt32(0);
                    cm.materialTitle = sdr.GetString(9);
                    cm.materialCatagory = sdr.GetString(2);
                    cm.materialDiscription = sdr.GetString(3);
                    cm.materialFilePath = sdr.GetString(7);
                    cm.materialPoints = int.Parse(sdr.GetString(8));
                    cm.materialDueDate = sdr.GetDateTime(4);
                    cm.materialPostedDate = sdr.GetDateTime(5);

                    forSubmittionobj = cm;
                }

                sdr.Close();


            }
        }

        public void fillSubmittionDetails(GunaLabel catagory, GunaLabel fileName, Panel p, Bunifu.Framework.UI.BunifuCustomLabel cl, GunaLabel materialId, GunaGroupBox ggb, Bunifu.Framework.UI.BunifuCustomLabel duedate)
        {
            catagory.Text = forSubmittionobj.materialTitle;
            cl.Text = forSubmittionobj.materialDiscription;
            duedate.Text = forSubmittionobj.materialDueDate.ToString();
            materialId.Text = forSubmittionobj.materialId.ToString();
            if (forSubmittionobj.materialFilePath != "No File Choosen")
            {
                p.Show();
                fileName.Text = forSubmittionobj.materialFilePath;

            }
            else
            {
                p.Hide();
            }
            if (forSubmittionobj.materialCatagory == "Lecturer")
            {
                ggb.Hide();
            }
            else
            {
                ggb.Show();
            }

        }
        public void fillResubmittionDetails(GunaLabel catagory, GunaLabel fileName, Panel p, Bunifu.Framework.UI.BunifuCustomLabel cl, GunaLabel materialId, GunaGroupBox ggb, GunaLabel file2, Bunifu.Framework.UI.BunifuCustomLabel duedate)
        {
            catagory.Text = forSubmittionobj.materialTitle;
            cl.Text = forSubmittionobj.materialDiscription;
            materialId.Text = forSubmittionobj.materialId.ToString();
            duedate.Text = forSubmittionobj.materialDueDate.ToString();
            if (forSubmittionobj.materialFilePath != "No File Choosen")
            {
                p.Show();
                fileName.Text = forSubmittionobj.materialFilePath;

            }
            else
            {
                p.Hide();
            }
            if (forSubmittionobj.materialCatagory == "Lecturer")
            {
                ggb.Hide();
            }
            else
            {
                ggb.Show();
            }
            file2.Text = reSubmittied.materialFilePath;
        }
        public void studentSubmits(int materialId, int studentId, int courseId, string filePath, string status, DateTime date, int marks, GunaLabel gnl)
        {
            cm = new CourseMaterial(materialId, studentId, courseId, filePath, status, date, marks);
            using (SqlCommand cmd = new SqlCommand("StudentSubmits", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@materialId", cm.materialId);
                cmd.Parameters.AddWithValue("@studentId", cm.studentId);
                cmd.Parameters.AddWithValue("@courseId", cm.courseId);
                cmd.Parameters.AddWithValue("@materialFilePath", Path.GetFileName(cm.materialFilePath));
                cmd.Parameters.AddWithValue("@materialStatus", cm.materialStatus);
                cmd.Parameters.AddWithValue("@materialPostedDate", cm.materialPostedDate);
                cmd.Parameters.AddWithValue("@materialPoints", cm.materialPoints);
                cmd.ExecuteNonQuery();
                copyMaterial(cm.materialFilePath);
                gnl.Show();
            }
        }

        public bool studentResubmitCheak(int materialId, int studentId)
        {
            reSubmittied = new CourseMaterial();
            bool a = false;
            cm1 = new CourseMaterial(materialId, studentId);
            using (SqlCommand cmd = new SqlCommand("StudentResubmitCheak", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@materialId", cm1.materialId);
                cmd.Parameters.AddWithValue("@studentId", cm1.studentId);
                cmd.ExecuteNonQuery();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    a = true;
                    reSubmittied.submittionId = sdr.GetInt32(0);
                    reSubmittied.materialFilePath = sdr.GetString(4);
                }
                sdr.Close();

                return a;

            }

        }
        public void resubmit(DateTime date, string file, int submittionId, string status)
        {
            cm = new CourseMaterial(file, date, status);
            using (SqlCommand cmd = new SqlCommand("Resubmit", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@materialFilePath", Path.GetFileName(cm.materialFilePath));
                cmd.Parameters.AddWithValue("@submittionId", submittionId);
                cmd.Parameters.AddWithValue("@postedDate", cm.materialPostedDate);
                cmd.Parameters.AddWithValue("@status", cm.materialStatus);
                cmd.ExecuteNonQuery();
                copyMaterial(cm.materialFilePath);
            }

        }
        /// <summary>
        /// manage people k andar list main student a chuka ha with id amd name 
        /// below  mathod saray  submittions select kar layaga by material id 
        /// </summary>
        /// <param name="materialId"></param>
        public void getSubmittedMaterialById(int materialId)
        {
            submittedMaterial = new List<CourseMaterial>();
            using (SqlCommand cmd = new SqlCommand("GetSubmittedMaterialById", Connection.Connect()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@materialId", materialId);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cm = new CourseMaterial();
                    cm.submittionId = sdr.GetInt32(0);
                    cm.materialId = sdr.GetInt32(1);
                    cm.studentId = sdr.GetInt32(2);
                    cm.courseId = sdr.GetInt32(3);
                    cm.materialFilePath = sdr.GetString(4);
                    cm.materialStatus = sdr.GetString(5);
                    cm.materialPoints = sdr.GetInt32(7);
                    submittedMaterial.Add(cm);
                }

                sdr.Close();


            }
        }
        public void designSubmittions()
        {
            studentFiles = new List<submittedFileUC>();
            for (int i = 0; i < ManagePeople.studentFullObject.Count; i++)
            {
                for (int j = 0; j < submittedMaterial.Count; j++)
                {
                    if (ManagePeople.studentFullObject[i].studentId == submittedMaterial[j].studentId)
                    {

                        submittedFileUC files = new submittedFileUC();
                        files.stdName.Text = ManagePeople.studentFullObject[i].studentName;
                        files.subId.Text = submittedMaterial[j].submittionId.ToString();
                        files.fileName.Text = submittedMaterial[j].materialFilePath;
                        files.status.Text = submittedMaterial[j].materialStatus;
                        files.tPoints.Text = "/ " + forSubmittionobj.materialPoints;
                        files.textBox1.Text = submittedMaterial[j].materialPoints.ToString();
                        files.Margin = new Padding(4, 4, 4, 4);
                        files.BackColor = Color.FromArgb(240, 240, 240);
                        studentFiles.Add(files);
                    }
                   
                }
            }
        }
    }
}
