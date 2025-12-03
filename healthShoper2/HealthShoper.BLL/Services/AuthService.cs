using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HealthShoper.BLL.Contstants;
using HealthShoper.BLL.Exceptions.Models.Enums;
using HealthShoper.BLL.Extensions;
using HealthShoper.BLL.Interfaces;
using HealthShoper.BLL.Models;
using HealthShoper.BLL.Models.Dtos;
using HealthShoper.BLL.Models.ViewModels;
using HealthShoper.DAL.Extensions;
using HealthShoper.DAL.Interfaces;
using HealthShoper.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace HealthShoper.BLL.Services;

/// <summary>
/// Сервис для генерации токена доступа в систему.
/// </summary>
/// <param name="dbContext"></param>
/// <param name="configuration"></param>
public class AuthService(
    IApplicationDbContext dbContext,
    IPasswordHasher<Client> passwordHasher,
    IConfiguration configuration) : IAuthService
{
    /// <summary>
    /// Генерация токена.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public async Task<TokenDto> GenerateToken(int userId)
    {
        var client = await dbContext.Set<Client>().FirstOrDefaultAsync(p => p.Id == userId && !p.IsDeleted);

        if (client is null)
        {
            throw new UnauthorizedAccessException();
        }
        // Данные для токена 
        var claims = CreateClaims(userId, client);
        // Генерируем Access Token (секретный ключ из конфигурации)
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ApplicationSettings:Secret"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(configuration["ApplicationSettings:Issuer"],
            configuration["ApplicationSettings:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );
        // Генерируем Refresh Token(случайная строка)
        var tokenDto = new TokenDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = GenerateRefreshToken(),
            ValidTo = token.ValidTo
        };
        // Сохраняем Refresh Token в БД (для проверки при обновлении)
        client.RefreshToken = tokenDto.RefreshToken;
        dbContext.Set<Client>().Update(client);
        await dbContext.SaveChangesAsync();
        return tokenDto;
    }
    // Вход пользователя
    public async Task<TokenDto> LogIn(LogInViewModel viewModel)
    {
        var client = await dbContext.Set<Client>().FirstOrDefaultAsync(p => p.Email == viewModel.Email && !p.IsDeleted);

        if (client is null)
        {
            throw ApiResultExtensions.Unauthorized(ErrorCode.Unauthorized, "Invalid username or password FAILED");
        }

        if (passwordHasher.VerifyHashedPassword(client, client.PasswordHash, viewModel.Password) == PasswordVerificationResult.Failed)
        {
            throw ApiResultExtensions.Unauthorized(ErrorCode.Unauthorized, "Invalid username or password ");
        }


        return await GenerateToken(client.Id);
    }

    /// <summary>
    /// Обновление AccessToken в случае если истек срок годности AccessToken
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public async Task<TokenDto> RefreshToken(string refreshToken)
    {
        var client = await dbContext.Set<Client>()
            .FirstOrDefaultAsync(p => p.RefreshToken == refreshToken && !p.IsDeleted);

        if (client is null)
        {
            throw ApiResultExtensions.Unauthorized(ErrorCode.Unauthorized, "Invalid username or password");
        }

        return await GenerateToken(client.Id);
    }

    /// <summary>
    /// Генерация RefreshToken
    /// </summary>
    /// <returns></returns>
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    /// <summary>
    /// Создание Claims (данных для токена)
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="client"></param>
    /// <returns></returns>
    private static List<Claim> CreateClaims(int userId, Client client)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, client.Email),
            new(ClaimsConst.RoleId, client.Role.ToString()),
        };
        if (!string.IsNullOrEmpty(client.PhoneNumber))
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.PhoneNumber, client.PhoneNumber));
        }

        return claims;
    }
}