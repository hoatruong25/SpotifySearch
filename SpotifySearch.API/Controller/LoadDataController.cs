using Microsoft.AspNetCore.Mvc;
using SpotifySearchAPI.BusinessService.LoadDataService;

namespace SpotifySearchAPI.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class LoadDataController : ControllerBase
{
    private readonly ILoadDataService _loadDataService;

    public LoadDataController(ILoadDataService loadDataService)
    {
        _loadDataService = loadDataService;
    }

    [HttpPost]
    public async Task<IActionResult> ImportData(CancellationToken cancellation)
    {
        try
        {
            await _loadDataService.IndexDataFromCsvAsync(cancellation);
            return Ok("Success");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}