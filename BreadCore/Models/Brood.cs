using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BreadCore.Models;

namespace BreadCore.Models
{
    public class Brood
    {
        [Key]
        public int Id { get; set; }
        public BroodType GebakkenBrood { get; set; }
        public Filiaal GebakkenFiliaal { get; set; }
        [DataType(DataType.DateTime)]   
        public DateTime TijdGebakken { get; set; }
        public int HoeveelheidGebakken { get; set; }
        public int HoeveelheidDerving { get; set; }
        public Medewerker Bakker { get; set; }
    }
}

