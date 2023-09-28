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

        public VillaAPIController(ILogger<VillaAPIController> logger)
        {
            _logger = logger;
        }





        [HttpGet]
        [Route("GetAllVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<VillaDTO> GetVillas()
        {
            _logger.LogInformation("Getting all Villa information");
            return VillaStore.villaList;
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

            var villa = VillaStore.villaList.FirstOrDefault(usb => usb.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null) { return BadRequest(villaDTO); }
            if (villaDTO.Id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }

            if (VillaStore.villaList.FirstOrDefault(usa => usa.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Customer Error :", "Please enter Unique Villa Name..");
                return BadRequest(ModelState);
            }

            villaDTO.Id = VillaStore.villaList.OrderByDescending(Asutralia => Asutralia.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDTO);
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }


        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0) return BadRequest();
            var villa = VillaStore.villaList.FirstOrDefault(Aus => Aus.Id == id);
            if (villa != null) { VillaStore.villaList.Remove(villa); }
            return Ok("Villa has been deleted");
        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateVilla(int id, VillaDTO villaDTO)
        {
            if (id != villaDTO.Id) return BadRequest("you ID does not match, please try again with correct ID");

            var villa = VillaStore.villaList.FirstOrDefault(Aus => Aus.Id == villaDTO.Id);
            if (villa == null) { return BadRequest("Villa not found"); }

            villa.Name = villaDTO.Name;
            

            return Ok(villaDTO);
        }




    }
}
