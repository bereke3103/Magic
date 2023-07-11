using AutoMapper;
using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dto;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_VillaApi.Controllers
{

    [Route("api/VillaNumberAPI")]
    public class VillaNumberAPIController : ControllerBase
    {

        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;
        public VillaNumberAPIController(IMapper mapper, IVillaNumberRepository dbVillaNumber, IVillaRepository dbVilla)
        {
            _mapper = mapper;
            _dbVillaNumber = dbVillaNumber;
            _response = new APIResponse();
            _dbVilla = dbVilla;

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumberList = await _dbVillaNumber.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaNumberDto>>(villaNumberList);
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
        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumberById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villaNumber = await _dbVillaNumber.GetAsync(v => v.VillaNo == id);
                if (villaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
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
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDto createDto)
        {

            try
            {
               

                if (await _dbVillaNumber.GetAsync(x => x.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("ExixtsError", "Villa number already exists");
                    return BadRequest(ModelState);
                }

                if (await _dbVilla.GetAsync(x=> x.Id == createDto.VilldID) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa ID is invalid");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                
                VillaNumber villaNumber = _mapper.Map<VillaNumber>(createDto);

          

                await _dbVillaNumber.CreateAsync(villaNumber);
                _response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillWithId", new { id = villaNumber.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

        }


        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> DeleteVillaNumberById(int id)
        {

            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);
                if (villaNumber == null)
                {
                    return NotFound();
                }

                _dbVillaNumber.RemoveAsync(villaNumber);
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

        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumberById(int id, [FromBody] VillaNumberUpdateDto updateDto)
        {

            try
            {
                if (updateDto == null || id != updateDto.VillaNo)
                {
                    return BadRequest();
                }


                if (await _dbVilla.GetAsync(x=> x.Id == updateDto.VilldID) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa ID is invalid");
                    return BadRequest(ModelState);
                }


                VillaNumber model = _mapper.Map<VillaNumber>(updateDto);


                await _dbVillaNumber.UpdateAsync(model);
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
    }
}
