using System.ComponentModel.DataAnnotations;

namespace ResultManagmentSystem.Models.Data
{
    public class ReportCardMo
    {

        public string RollNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DateOfBirth { get; set; }
        public string MobileNo { get; set; }   
        public string Address { get; set; } 
        public int CSharp { get; set; }
        public int AspDotNet { get; set; }
        public int Mvc { get; set; }
        public int Angular { get; set; }
        public int Sqln { get; set; }
        public int TotalSum { get; set; }
        public int Percentaged { get; set; }
        public string Decision { get; set; }
     
      
     
    }
}
