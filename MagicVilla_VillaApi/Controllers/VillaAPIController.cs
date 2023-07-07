using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Logging;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaApi.Controllers
{
    //[Route("/api/[controller]")]
    [Route("api/VillaAPI")]
    //[ApiController]
    //[ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogging _logging;
        public VillaAPIController(ILogging logging)
        {
            _logging = logging;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVills()
        {
            _logging.Log("All Vills were got!!!!", "");
            return Ok(VillaStore.villaList);
        }

        //[HttpGet("id")]
        [HttpGet("{id:int}", Name = "GetVillWithId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type = typeof(VillaDto))]
        public ActionResult<VillaDto> GetId(int id)
        {
            if (id == 0)
            {
                _logging.Log("Vills CAN NOT BET ZERO 0", "error");
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] 
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto) 
        {

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(villaDto);
            //}

            if (VillaStore.villaList.FirstOrDefault(x=> x.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ExixtsError", "Such item exists");          
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }

            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDto.Id = VillaStore.villaList.OrderByDescending(u=> u.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDto);

            return CreatedAtRoute("GetVillWithId", new { id = villaDto.Id}, villaDto);
        }


        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteVillaById (int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id:int}", Name ="UpdateVillaWith")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateValue(int id, [FromBody] VillaDto villaDto)
        {
            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(x=> x.Id == id);
            villa.Name = villaDto.Name;
            villa.Sqft = villaDto.Sqft;
            villa.Occupancy = villaDto.Occupancy;

            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);

            if (villa == null)
            {
                return BadRequest();
            }

            patchDto.ApplyTo(villa);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
