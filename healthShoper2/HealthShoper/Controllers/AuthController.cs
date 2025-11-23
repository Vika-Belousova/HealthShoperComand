namespace HealthShoper.Controllers;

[ApiController] // Контроллер выступает в качестве API
[AllowAnonymous] // Получить доступ к данному контроллеру может любой пользователь. Без проверки авторизации
[Route("api/[controller]")] // Базовый маршрут к контроллеру
[SwaggerTag("api для регистрации и авторизации пользователя")] // Для сваггера помечается для чего нужен данный контроллер
public class AuthController(IClientService clientService, IAuthService authService) : ControllerBase
{
    /// <summary>
    ///     Регистрация пользователя
    /// </summary>
    /// <param name="clientViewModel"></param>
    /// <returns name="tokenDto"></returns>
    [HttpPost]
    [Route("Registration")]
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Registration([FromBody] ClientViewModel clientViewModel)
    {
        var token = await clientService.CreateClient(clientViewModel);
        return Ok(token);
    }

    /// <summary>
    ///     Авторизация пользователя
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns name="tokenDto"></returns>
    [HttpPost]
    [Route("LogIn")]
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> LogIn([FromBody] LogInViewModel viewModel)
    {
        var tokenDto = await authService.LogIn(viewModel);
        return Ok(tokenDto);
    }
}