using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("Api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : Controller
    {
        private readonly ILogger<VillaAPIController> _logger;
        private readonly ApplicationDbContext _db;


        public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        [HttpGet]
        [Route("GetAllVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Getting all Villa information");
            return Ok(_db.Villas);
        }


        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            _logger.LogInformation("Getting information about the Villa : " + id);
            if (id == 0)
            {
                _logger.LogError("Villa Error");
                return BadRequest();
            }

            var villa = _db.Villas.FirstOrDefault(usb => usb.Id == id);

            if (villa == null)
            {
                return NotFound("No villa is found for the ID you have provided.");
            }

            return Ok(villa);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null) { return BadRequest(villaDTO); }
  
            if (_db.Villas.FirstOrDefault(usa => usa.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Customer Error :", "Please enter Unique Villa Name..");
                return BadRequest(ModelState);
            }

            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                CreatedDate = DateTime.Now,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.sqft
            };

            _db.Villas.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }


        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0) return BadRequest();
            var villa = _db.Villas.FirstOrDefault(au => au.Id == id);

            if (villa != null)
            {
                _db.Villas.Remove(villa);
                _db.SaveChanges();
                return Ok("Villa has been deleted");
            }
            return Ok("Villa could not be located");
        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateVilla(int id, VillaDTO villaDTO)
        {
            if (id != villaDTO.Id) return BadRequest("you ID does not match, please try again with correct ID");

            if (villaDTO == null) { return BadRequest(villaDTO); }
            if (villaDTO.Id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }

            if (_db.Villas.FirstOrDefault(usa => usa.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Customer Error :", "Please enter Unique Villa Name..");
                return BadRequest(ModelState);
            }

            Villa model = new()
            {
                Id = villaDTO.Id,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                CreatedDate = DateTime.Now,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.sqft
            };

            _db.Villas.Add(model);
            _db.Update(model);
            _db.SaveChanges();

            return Ok(villaDTO);
        }
    }
}
