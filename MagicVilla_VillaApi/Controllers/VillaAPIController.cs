using AutoMapper;
using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dto;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Controllers
{
    //[Route("/api/[controller]")]
    [Route("api/VillaAPI")]
    //[ApiController]
    //[ApiController]
    public class VillaAPIController : ControllerBase
    {
        //private readonly ApplicationDbContext _db;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaAPIController(IMapper mapper, IVillaRepository dbVilla)
        {
            _mapper = mapper;
            _dbVilla = dbVilla;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillsAsync()
        {
            IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();
            return Ok(_mapper.Map<List<VillaDto>>(villaList));
        }

        //[HttpGet("id")]
        [HttpGet("{id:int}", Name = "GetVillWithId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type = typeof(VillaDto))]
        public async Task<ActionResult<VillaDto>> GetId(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await _dbVilla.GetAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<VillaDto>(villa);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] 
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto createDto) 
        {

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(villaDto);
            //}

            if (await _dbVilla.GetAsync(x=> x.Name.ToLower() == createDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ExixtsError", "Such item exists");          
                return BadRequest(ModelState);
            }

            if (createDto == null)
            {
                return BadRequest(createDto);
            }

            //if (villaDto.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            //villaDto.Id = VillaStore.villaList.OrderByDescending(u=> u.Id).FirstOrDefault().Id + 1;
            //VillaStore.villaList.Add(villaDto);
            var model = _mapper.Map<Villa>(createDto);

            //Villa model = new Villa()
            //{
            //    Amenity = createDto.Amenity,
            //    Details = createDto.Details,
            //    //Id = villaDto.Id,
            //    ImageUrl = createDto.ImageUrl,
            //    Name = createDto.Name,
            //    Occupancy = createDto.Occupancy,
            //    Rate = createDto.Rate,
            //    Sqft = createDto.Sqft,

            //};

            await _dbVilla.CreateAsync(model);

            return CreatedAtRoute("GetVillWithId", new { id = model.Id}, createDto);
        }


        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> DeleteVillaById (int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbVilla.GetAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            _dbVilla.RemoveAsync(villa);
            return NoContent();
        }

        [HttpPut("{id:int}", Name ="UpdateVillaWith")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateValue(int id, [FromBody] VillaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }

            //var villa = _db.Villas.FirstOrDefault(x=> x.Id == id);
            //villa.Name = villaDto.Name;
            //villa.Sqft = villaDto.Sqft;
            //villa.Occupancy = villaDto.Occupancy;

            var model = _mapper.Map<Villa>(updateDto);

            //Villa model = new Villa()
            //{
            //    Amenity = updateDto.Amenity,
            //    Details = updateDto.Details,
            //    Id = updateDto.Id,
            //    ImageUrl = updateDto.ImageUrl,
            //    Name = updateDto.Name,
            //    Occupancy = updateDto.Occupancy,
            //    Rate = updateDto.Rate,
            //    Sqft = updateDto.Sqft
            //};

            await _dbVilla.UpdateAsync(model);
            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _dbVilla.GetAsync(x => x.Id == id, tracked: false);

            var villaDto = _mapper.Map<VillaUpdateDto>(villa);
            //VillaUpdateDto villaDto = new()
            //{
            //    Amenity = villa.Amenity,
            //    Details = villa.Details,
            //    Id = villa.Id,
            //    ImageUrl = villa.ImageUrl,
            //    Name = villa.Name,
            //    Occupancy = villa.Occupancy,
            //    Rate = villa.Rate,
            //    Sqft = villa.Sqft
            //};

             if (villa == null)
             {
                return BadRequest();
             }

            patchDto.ApplyTo(villaDto, ModelState);

            var model = _mapper.Map<Villa>(villa);


            //_db.Villas.Update(model);
            //await _db.SaveChangesAsync();
            await _dbVilla.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
