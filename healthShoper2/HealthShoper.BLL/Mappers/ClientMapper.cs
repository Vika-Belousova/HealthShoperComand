using HealthShoper.BLL.Models.Dtos;
using HealthShoper.BLL.Models.ViewModels;
using HealthShoper.DAL.Models;
using Riok.Mapperly.Abstractions;

namespace HealthShoper.BLL.Mappers;

[Mapper]
public static partial class ClientMapper
{
    public static IEnumerable<ClientDto> MapToClientDtos(this IEnumerable<Client> clients)
    {
        return clients.Select(MapToClientDto);
    }

    public static ClientDto MapToClientDto(this Client client)
    {
        return ToDto(client);
    }

    private static partial ClientDto ToDto(Client client);
}