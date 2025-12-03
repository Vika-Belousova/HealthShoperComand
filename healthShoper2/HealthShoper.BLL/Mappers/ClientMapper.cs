using HealthShoper.BLL.Models.Dtos;
using HealthShoper.BLL.Models.ViewModels;
using HealthShoper.DAL.Models;
using Riok.Mapperly.Abstractions;

namespace HealthShoper.BLL.Mappers;

[Mapper]
public static partial class ClientMapper
{
    // Для коллекции: List<Client> → List<ClientDto>
    public static IEnumerable<ClientDto> MapToClientDtos(this IEnumerable<Client> clients)
    {
        return clients.Select(MapToClientDto);
    }
    // Для одного объекта: Client → ClientDto
    public static ClientDto MapToClientDto(this Client client)
    {
        return ToDto(client);
    }
    // Этот метод будет автоматически сгенерирован Mapperly
    // Он сам найдёт совпадающие свойства и скопирует их
    private static partial ClientDto ToDto(Client client);
}