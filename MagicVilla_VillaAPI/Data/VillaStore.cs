using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Data
{
    public class VillaStore
    {

        public static List<VillaDTO> villaList = new List<VillaDTO> {
                new VillaDTO { Id=1, Name= "Pool View", Direction= "North" },
                new VillaDTO { Id=2,Name= "Beach View", Direction = "South"},
                new VillaDTO { Id=5,Name= "Sandy", Direction = "South"},
            };

    }

}

