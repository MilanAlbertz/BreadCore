using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BreadCore.Models
{
    public class Filiaal { 

        [Key]
        public int FiliaalId { get; set; } 
        public string FiliaalNaam { get; set; }
        public List<Medewerker>? Medewerkers { get; set; }
    }
}
