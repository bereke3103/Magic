using MagicVilla_Utility;
using MagicVillaWeb.Models;
using MagicVillaWeb.Models.Dto;
using MagicVillaWeb.Services.IServices;
using static System.Net.WebRequestMethods;

namespace MagicVillaWeb.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;

        public VillaService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrl:VillaApi");
        }

        public Task<T> CreateAsync<T>(VillaCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{villaUrl}/api/VillaAPI" 
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = $"{villaUrl}/api/VillaAPI/{id}" 
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            var request = new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = "https://localhost:7150/api/VillaAPI" 
            };
            var result =  SendAsync<T>(request);
            return result;
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{villaUrl}/api/VillaAPI/{id}" 
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{villaUrl}/api/VillaAPI/{dto.Id}" 
            });
        }
    }
}
