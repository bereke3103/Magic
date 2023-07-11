using MagicVillaWeb.Models;

namespace MagicVillaWeb.Services.IServices
{
    public interface IBaseServices
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest request);
    }
}
