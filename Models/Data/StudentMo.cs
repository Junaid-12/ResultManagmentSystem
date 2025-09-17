using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ResultMamangementSystem.Models
{
    public class StudentMo
    {
        public int Id { get; set; }
        public string RollNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public  DateTime DateOfBirth { get; set; } 
        public string MobileNo { get; set; }
        public string Address { get; set; }

    }
}
