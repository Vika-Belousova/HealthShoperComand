using HealthShoper.BLL.Models.Dtos;
using HealthShoper.BLL.Models.ViewModels;

namespace HealthShoper.BLL.Interfaces;

public interface IClientService
{
    Task<ClientDto> GetClient(int id);
    Task<TokenDto> CreateClient(ClientViewModel dto);
    Task<IEnumerable<ClientDto>> GetClients();
    Task UpdateClient(int id, ClientViewModel dto);
    Task DeleteClient(int id);
}