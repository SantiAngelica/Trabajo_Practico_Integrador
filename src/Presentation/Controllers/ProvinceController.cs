using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/cities")]
public class ProvinceController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    public HttpClient provinceClient { get; set; }

    public ProvinceController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        string? httpClientName = "ProvinceHttpCLient";
        provinceClient = _httpClientFactory.CreateClient(httpClientName);
    }

    [HttpGet("provinces")]
    public async Task<string> GetProvinces()
    {
        return await provinceClient.GetStringAsync("provincias");
    }

    [HttpGet("municipies")]
    public async Task<string> GetMunicipies()
    {
        return await provinceClient.GetStringAsync("municipios");
    }
}
