namespace HealthShoper.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[SwaggerTag("API с клиентом. ")]
public class ClientController(IClientService clientService) : ControllerBase
{
    [HttpGet]
    [Route("Me")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AboutMe()
    {
        var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await clientService.GetClient(userId));
    }
}