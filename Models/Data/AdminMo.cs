using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ResultMamangementSystem.Models
{
    public class AdminMo
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }
        public int Password { get; set; }
    }
}
