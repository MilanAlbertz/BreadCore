using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BreadCore.Models;

namespace BreadCore.Models
{
    public class Brood
    {
        [Key]
        public int Id { get; set; }
        public int? BroodTypeID { get; set; }
        public BroodType? BroodType { get; set; }
        [Required]
        public int? GebakkenFiliaalId { get; set; }
        public Filiaal? GebakkenFiliaal { get; set; }
        [DataType(DataType.DateTime)]   
        public DateTime? TijdGebakken { get; set; }
        public int? HoeveelheidGebakken { get; set; }
        public int? HoeveelheidDerving { get; set; }
        public int? MedewerkerId { get; set; }
        public Medewerker? Medewerker { get; set; }
        public int? Bakprogramma { get; set; }

    }
}

