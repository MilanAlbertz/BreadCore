using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BreadCore.Models
{
    public class Medewerker
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BedienerNr { get; set; }
        [Required]
        public int Wachtwoord { get; set; }
        [Required]
        public string Rol { get; set; }
        public int WerkendFiliaal { get; set; }
    }
}
