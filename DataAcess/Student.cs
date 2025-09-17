using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ResultMamangementSystem.Models;
using System.Data;

namespace ResultManagmentSystem.DataAcess
{
    public class Student
    {
        SqlConnection Con;
        public Student()
        {
            Con = new SqlConnection("Server=WINDOWS-SNAEAQL; Initial Catalog=ResultManagement ; Persist Security Info = True; Integrated Security=True; TrustServerCertificate=True; ");
        }

        public List<StudentMo> GetStudentList()
        {
            List<StudentMo> Ilist = new List<StudentMo>();
            string str = "Select * from StudentData";
            SqlCommand sqlCommand = new SqlCommand(str, Con);
            SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            Con.Open();
            sda.Fill(dt);
            Con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Ilist.Add(new StudentMo
                {
                    Id = Convert.ToInt32(dr["id"]),
                    RollNo = Convert.ToString(dr["Rollno"]),
                    Name = Convert.ToString(dr["Name"]),
                    FatherName = Convert.ToString(dr["fatherName"]),
                    Email = Convert.ToString(dr["Email"]),
                    DateOfBirth =Convert.ToDateTime(dr["DateOfBirth"]),
                    MobileNo = Convert.ToString(dr["MobileNo"]),
                    Address = Convert.ToString(dr["Address"]),
                });

            }
            return Ilist;
        }

       public bool CreateSudent(StudentMo studentMo)
        {
            string query = "Insert Into StudentData (Rollno,Name,FatherName,Email,DateOfBirth,MobileNo,Address) values ('" + studentMo.RollNo+ "','"+studentMo.Name+ "','"+studentMo.FatherName + "','"+studentMo.Email+"','"+studentMo.DateOfBirth+"','"+studentMo.MobileNo+"','"+studentMo.Address+"')";
           SqlCommand cmd= new SqlCommand(query, Con);
            Con.Open();
            int i = cmd.ExecuteNonQuery();
            Con.Close();
            if(i>0) return true;    
            return false;   
        }
        public bool UpdateStudent(StudentMo studentMo)
        {
            
            string quary= "UPDATE  StudentData SET  Name='" + studentMo.Name+ "',FatherName='" + studentMo.FatherName + "',Email='" + studentMo.Email+ "',DateOfBirth='"+studentMo.DateOfBirth+"',MobileNo='" + studentMo.MobileNo+ "',Address='" + studentMo.Address+ "' WHERE Id=" + studentMo.Id;
           SqlCommand cmd = new SqlCommand(quary, Con); 
            Con.Open();
            int i = cmd.ExecuteNonQuery();
            Con.Close();
            if(i>0) return true;
            return false;
        }
        public bool DeleteStudent(int id)
        {
            string query = "Delete from StudentData Where Id =" + id;
            SqlCommand cmd = new SqlCommand(query,Con);
            Con.Open();
              int i = cmd.ExecuteNonQuery();
            Con.Close();
            if(i>0) return true;
            return false;
        }

    }
}
