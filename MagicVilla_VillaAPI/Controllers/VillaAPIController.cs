using MagicVilla_VillaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("Api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : Controller
    {
        [HttpGet]
        public IEnumerable<Villa> GetVillas()
        {
            return new List<Villa> { 
                new Villa { Id=1, name= "Pool View"},
                new Villa { Id=2,name= "Beach View"}
            };
        }





        
    }
}
