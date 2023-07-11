using AutoMapper;
using MagicVillaWeb.Models;
using MagicVillaWeb.Models.Dto;
using MagicVillaWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVillaWeb.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;


        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;

        }
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDto> list = new List<VillaDto>();

            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.isSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
