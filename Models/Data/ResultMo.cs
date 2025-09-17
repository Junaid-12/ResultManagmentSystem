using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ResultMamangementSystem.Models
{
    public class ResultMo
    {
        public int Id { get; set; }
        [ForeignKey(nameof(StudentMo.RollNo))]
        public string RollNo { get; set; }
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
