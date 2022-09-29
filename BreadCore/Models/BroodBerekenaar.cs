using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BreadCore.Models
{
    public class Broodberekenaar
    {
        public static List<int?> GemiddeldeGebakkenAlleFilialenBerekenen(List<Brood> broden, List<BroodType> broodTypes)
        {
            List<int?> lijstAantalGebakkenBroden = new List<int?>();
            int? aantalGebakkenBroden = 0;
            int? aantalTypesGebakken = 0;
            foreach (var broodType in broodTypes)
            {
                foreach (var brood in broden)
                {
                    if (broodType.BroodTypeID == brood.BroodTypeID)
                    {
                        aantalGebakkenBroden += brood.HoeveelheidGebakken;
                        aantalTypesGebakken += 1;
                    }
                }
                aantalGebakkenBroden /= aantalTypesGebakken;
                lijstAantalGebakkenBroden.Add(aantalGebakkenBroden);
                aantalGebakkenBroden = 0;
                aantalTypesGebakken = 0;


            }
            return lijstAantalGebakkenBroden;

        }
        public static List<int?> GemiddeldeDervingAlleFilialenBerekenen(List<Brood> broden, List<BroodType> broodTypes)
        {
            List<int?> lijstAantalBedorvenBroden = new List<int?>();
            int? aantalDervingBroden = 0;
            int? aantalTypesDerving = 0;
            foreach (var broodType in broodTypes)
            {
                foreach (var brood in broden)
                {
                    if (broodType.BroodTypeID == brood.BroodTypeID)
                    {
                        aantalDervingBroden += brood.HoeveelheidDerving;
                        aantalTypesDerving += 1;
                    }
                }
                aantalDervingBroden /= aantalTypesDerving;
                lijstAantalBedorvenBroden.Add(aantalDervingBroden);
                aantalDervingBroden = 0;
                aantalTypesDerving = 0;
            }
            return lijstAantalBedorvenBroden;
        }
    }
}
