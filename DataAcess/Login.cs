using Azure;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Data.SqlClient;
using ResultMamangementSystem.Models;
using System.Data;

namespace ResultManagmentSystem.DataAcess
{
    public class Login
    {
        SqlConnection Con;
        public Login()
        {
                    Con = new SqlConnection("Server=WINDOWS-SNAEAQL; Initial Catalog=ResultManagement ; Persist Security Info = True; Integrated Security=True; TrustServerCertificate=True; ");
        }
       /* public bool AddAdmin(AdminMo adminMo)
        {
            string str = " Insert into AdminData (UserId,PassWord) values ('" + adminMo.UserId + "','" + adminMo.Password + "')";
            SqlCommand sqlCommand = new SqlCommand(str, Con);
            Con.Open();
            int i = sqlCommand.ExecuteNonQuery();
            Con.Close();
            if (i > 0)
            {
                return true;
            }
            return false;


        }*/

        public bool LoginUser(AdminMo adminMo)
        {
         
            string quary = "select Count(*) from AdminData where UserId ='" + adminMo.UserId + "' and Password='" + adminMo.Password + "' ";
           
            SqlCommand cmd = new SqlCommand(quary,Con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            Con.Open();
          sqlDataAdapter.Fill(dt);
          
            cmd.ExecuteNonQuery();
            Con.Close();
            if (dt.Rows[0][0].ToString() == "1")
            {
                return true;
            }
            return false;   

            
        }

    }
}
