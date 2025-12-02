using HealthShoper.BLL.Models.Dtos;
using HealthShoper.BLL.Models.ViewModels;
using HealthShoper.DAL.Models.Enums;

namespace HealthShoper.BLL.Interfaces;

public interface IAuthService
{
    Task<TokenDto> GenerateToken(int userId);

    Task<TokenDto> LogIn(LogInViewModel  viewModel);
    
    Task<TokenDto> RefreshToken(string refreshToken);
}