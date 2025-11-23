namespace HealthShoper.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[SwaggerTag("API для работы с предметами ")]
public class ItemController(IItemService itemService) : ControllerBase
{
    /// <summary>
    /// Запрос для получения всех товаров.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(IEnumerable<ItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll([FromQuery] QueryFilter queryFilter)
    {
        return Ok(await itemService.GetItemsAsync(queryFilter));
    }

    /// <summary>
    /// Запрос для получения товара по ID.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetBy/{id:int}")]
    [ProducesResponseType(typeof(ItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await itemService.GetItemAsync(id));
    }

    /// <summary>
    /// Запрос для получения товаров по скидке.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAllDiscounts")]
    [ProducesResponseType(typeof(IEnumerable<ItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllItemsDiscounts()
    {
        return Ok(await itemService.GetDiscountItemsAsync());
    }
}