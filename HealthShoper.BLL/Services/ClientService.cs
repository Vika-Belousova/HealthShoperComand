using HealthShoper.BLL.Exceptions.Models.Enums;
using HealthShoper.BLL.Extensions;
using HealthShoper.BLL.Interfaces;
using HealthShoper.BLL.Mappers;
using HealthShoper.BLL.Models.Dtos;
using HealthShoper.BLL.Models.ViewModels;
using HealthShoper.DAL.Interfaces;
using HealthShoper.DAL.Models;
using HealthShoper.DAL.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthShoper.BLL.Services;

public class ClientService(
    IApplicationDbContext dbContext,
    IAuthService authService,
    IPasswordHasher<Client> passwordHasher) : IClientService
{
    public async Task<ClientDto> GetClient(int id)
    {
        var client = await dbContext.Set<Client>().FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        if (client is null)
        {
            throw new UnauthorizedAccessException();
        }

        return client.MapToClientDto();
    }


    public async Task<TokenDto> CreateClient(ClientViewModel dto)
    {
        try
        {
            var client = new Client()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Role = Role.Client,
            };
            await dbContext.Set<Client>().AddAsync(client);
            await dbContext.SaveChangesAsync();

            client.PasswordHash = passwordHasher.HashPassword(client, dto.Password);
            dbContext.Set<Client>().Update(client);
            await dbContext.SaveChangesAsync();
            return await authService.GenerateToken(client.Id);
        }
        catch (Exception e)
        {
            throw ApiResultExtensions.Unauthorized(ErrorCode.Unauthorized, "Sorry current errors");
        }
    }

    public async Task<IEnumerable<ClientDto>> GetClients()
    {
        var clients = dbContext.Set<Client>().Where(p => !p.IsDeleted).AsEnumerable();
        return clients.MapToClientDtos();
    }

    public Task UpdateClient(int id, ClientViewModel dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteClient(int id)
    {
        throw new NotImplementedException();
    }
}