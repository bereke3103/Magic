using AutoMapper;
using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dto;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
        private readonly APIResponse _response;
        public VillaAPIController(IMapper mapper, IVillaRepository dbVilla)
        {
            _mapper = mapper;
            _dbVilla = dbVilla;
            _response = new APIResponse();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillsAsync()
        {
            try
            {
                IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaDto>>(villaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        //[HttpGet("id")]
        [HttpGet("{id:int}", Name = "GetVillWithId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type = typeof(VillaDto))]
        public async Task<ActionResult<APIResponse>> GetVillaById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villa = await _dbVilla.GetAsync(v => v.Id == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                //var result = _mapper.Map<VillaDto>(villa);
                _response.Result = _mapper.Map<VillaDto>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDto createDto)
        {

            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(villaDto);
                //}

                if (await _dbVilla.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower()) != null)
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
                var villa = _mapper.Map<Villa>(createDto);

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

                await _dbVilla.CreateAsync(villa);
                _response.Result = _mapper.Map<Villa>(villa);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillWithId", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

        }


        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<APIResponse>> DeleteVillaById(int id)
        {

            try
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
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.isSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

        }

        [HttpPut("{id:int}", Name = "UpdateVillaWith")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateValue(int id, [FromBody] VillaUpdateDto updateDto)
        {

            try
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
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.isSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.isSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;

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
