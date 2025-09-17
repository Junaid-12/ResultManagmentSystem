using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Protocol;
using ResultMamangementSystem.Models;
using ResultManagmentSystem.Models.Data;
using ServiceStack;
using System.Collections.Generic;
using System.Data;

namespace ResultManagmentSystem.DataAcess
{
    public class Result
    {
        SqlConnection Con;
        public Result()
        {
            Con = new SqlConnection("Server=WINDOWS-SNAEAQL; Initial Catalog=ResultManagement ; Persist Security Info = True; Integrated Security=True; TrustServerCertificate=True; ");
        }

        public List<ResultMo> GetResultList()
        {
            List<ResultMo> Ilist = new List<ResultMo>();
            string str = "Select * from ResultData";
            SqlCommand sqlCommand = new SqlCommand(str, Con);
            SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            Con.Open();
            sda.Fill(dt);
            Con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Ilist.Add(new ResultMo
                {
                    Id=Convert.ToInt32(dr["Id"]),
                    RollNo = Convert.ToString(dr["Rollno"]),
                    CSharp = Convert.ToInt32(dr["CSharp"]),
                    AspDotNet = Convert.ToInt32(dr["AspDotNet"]),
                    Mvc = Convert.ToInt32(dr["Mvc"]),
                    Angular = Convert.ToInt32(dr["Angular"]),
                    Sqln = Convert.ToInt32(dr["Sqln"]),
                    TotalSum = Convert.ToInt32(dr["TotalSum"]),
                    Percentaged = Convert.ToInt32(dr["Percentaged"]),
                    Decision = Convert.ToString(dr["Decision"])

                });

            }
            return Ilist;
        }
        public List<ResultMo> GetRollfromDatabase() 
        {
            List<ResultMo> Ilist = new List<ResultMo>();
            string str = "select RollNo from StudentData "; 
            SqlCommand sqlCommand = new SqlCommand(str, Con);
            Con.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
           
            while (reader.Read())
            {
                Ilist.Add(new ResultMo
                {

                    RollNo = reader["RollNo"].ToString()
                });
            }
            return Ilist;
        }
        public bool CreateResult(ResultMo result)   
        {
            result.TotalSum = result.CSharp + result.AspDotNet + result.Mvc + result.Angular + result.Sqln;
            result.Percentaged = (100*result.TotalSum)/500;
            if (result.Percentaged > 35) {
                result.Decision = "pass";
            }
            else {
                result.Decision = "Fail";
             }
           
            string query = "Insert Into ResultData (RollNo,CSharp,AspDotNet,Mvc,Angular,Sqln,TotalSum ,Percentaged,Decision) values ('" + result.RollNo + "','" + result.CSharp + "','" + result.AspDotNet + "','" + result.Mvc + "','" + result.Angular + "','" + result.Sqln + "','"+result.TotalSum+"','"+result
                .Percentaged+"','"+result.Decision+"')";
            
            SqlCommand cmd = new SqlCommand(query, Con);
            Con.Open();
            int i = cmd.ExecuteNonQuery();
            Con.Close();
            if (i > 0) return true;
            return false;
        }   
        public bool UpdateResult(ResultMo result)
        {
            result.TotalSum=result.CSharp+result.AspDotNet+result.Mvc + result.Angular + result.Sqln;
            result.Percentaged=(result.TotalSum*100)/500;
            if (result.Percentaged > 35) result.Decision = "Pass";
            else result.Decision = "Fail";
            string quary = "Update  ResultData set  CSharp='" + result.CSharp + "',AspDotNet='" + result.AspDotNet + "',Mvc='" + result.Mvc + "',Angular='" + result.Angular + "',sqln='" + result.Sqln + "',TotalSum='"+result.TotalSum+ "',Percentaged='" + result.Percentaged+"',Decision='"+result.Decision+"' where Id=" + result.Id;
            SqlCommand cmd = new SqlCommand(quary, Con);
            Con.Open();
            int i = cmd.ExecuteNonQuery();
            Con.Close();    
            if (i > 0) return true;
            return false;
        }
        public bool DeleteResult(int id)
        {
            string query = "Delete from ResultData Where Id =" + id;
            SqlCommand cmd = new SqlCommand(query, Con);
            Con.Open();
            int i = cmd.ExecuteNonQuery();
            Con.Close();
            if (i > 0) return true;
            return false;
        }

        public List<ReportCardMo> GetReportCards(string rollno)
        {
            var Ilist = new List<ReportCardMo>();
             string quary = "Select *  from StudentData AS c Full OUTER JOIN ResultData AS P ON c.RollNo=p.RollNo where c.RollNo=@Rollno OR p.RollNo=@Rollno ";
            SqlCommand cmd = new SqlCommand(quary, Con);
            cmd.Parameters.AddWithValue("@Rollno", rollno);
         
     
            Con.Open(); 
            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                
                var data = new ReportCardMo
                {
                    RollNo = reader["RollNo"].ToString(),
                    Name = reader["Name"].ToString(),
                    FatherName = reader["FatherName"].ToString(),
                    Email = reader["Email"].ToString(),
                     DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                     MobileNo = reader["MobileNo"].ToString(),
                    Address = reader["Address"].ToString(),
                    CSharp = Convert.ToInt32(reader["CSharp"]),
                    AspDotNet = Convert.ToInt32(reader["AspDotNet"]),
                    Mvc = Convert.ToInt32(reader["Mvc"]),
                    Angular = Convert.ToInt32(reader["Angular"]),
                    Sqln = Convert.ToInt32(reader["Sqln"]),
                    TotalSum = Convert.ToInt32(reader["TotalSum"]),
                    Percentaged = Convert.ToInt32(reader["Percentaged"]),
                    Decision = reader["Decision"].ToString(),
                 
                };
                Ilist.Add(data);
            }
            reader.Close(); 
            
           return Ilist;    
        }

        public(string studentname, string email) SendEmailUser(string rollno )
        {
            string Studentname = null;
            string email = null;  
          
            string quary = "Select Name, Email  from StudentData where RollNo=@Rollno AND EXISTS  (Select 1 from ResultData where RollNo=@Rollno) ";
          

                SqlCommand cmd = new SqlCommand(quary, Con);
                cmd.Parameters.AddWithValue("@Rollno",rollno);
                Con.Open();
                SqlDataReader  reader = cmd.ExecuteReader();
             if (reader.Read()) {
                Studentname = reader["Name"].ToString();
                email = reader["Email"].ToString();
               

            };
            reader.Close();
            return (Studentname ,email) ;
           

        }

    }
}

