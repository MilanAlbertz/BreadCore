using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BreadCore.Models;

namespace BreadCore.Models
{
    public class Bakprogramma
    {
        [Key]
        public int Id { get; set; }
        public string Naam { get; set; }
        public List<BroodType>? BroodTypes { get; set; }
    }
}

