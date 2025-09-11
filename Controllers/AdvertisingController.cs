using AdvertisingService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertisingController : ControllerBase
    {
        private readonly IAdvertisingServiceJson _advertisingService;

        public AdvertisingController(IAdvertisingServiceJson advertisingService)
        {
            _advertisingService = advertisingService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл обязательно!!!");

            try
            {
                using var stream = file.OpenReadStream();
                var success = await _advertisingService.UploadDataAsync(stream);

                if (!success)
                    return BadRequest("Недопустимый формат JSON");

                return Ok("Данные успешно загружены");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string location)
        {
            try
            {
                var platforms = _advertisingService.SearchPlatforms(location);
                return Ok(platforms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
