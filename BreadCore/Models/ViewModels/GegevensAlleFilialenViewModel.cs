namespace BreadCore.Models.ViewModels
{
    public class GegevensAlleFilialenViewModel
    {
        public List<BroodType?> BroodTypes { get; set; }
        public List<Brood?> Brood { get; set; }
        public List<int?> HoeveelheidGebakken { get; set; }
        public List <int?> HoeveelheidDerving { get; set; }
        public DateTime? BeginDatum { get; set; }
        public DateTime? Einddatum { get; set; }
    }
}
 