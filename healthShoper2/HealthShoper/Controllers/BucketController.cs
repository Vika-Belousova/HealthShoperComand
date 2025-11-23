namespace HealthShoper.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[SwaggerTag("API для работы с корзиной ")]
public class BucketController(IBucketService bucketService) : ControllerBase
{
    [HttpGet]
    [Route("GetItemFromBucket")]
    public async Task<IActionResult> GetFromBucket()
    {
        var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await bucketService.GetFromBucket(userId));
    }

    [HttpPost]
    [Route("AddInBucket")]
    public async Task<IActionResult> AddInBucket([FromBody] int itemId)
    {
        var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value);
        await bucketService.AddBucket(itemId, userId);
        return Ok();
    }

    [HttpDelete]
    [Route("DeleteFromBucket/{itemId}")]
    public async Task<IActionResult> DeleteFromBucket(int itemId)
    {
        var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value);
        await bucketService.DeleteBucket(itemId, userId);
        return Ok();
    }
}