using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BreadCore.Models;

namespace BreadCore.Models
{
    public class BroodType
    {
        [Key]
        public int BroodTypeID { get; set; } 
        public string Type { get; set; }
        public int Code { get; set; }
        public int BakProgramma { get; set; }
    }
}

