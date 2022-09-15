using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BreadCore.Models
{
    public class Medewerker
    {
        [Key]
        public int Id { get; set; } 
        public int BedienerNr { get; set; }
        public string Wachtwoord { get; set; }
    }
}
