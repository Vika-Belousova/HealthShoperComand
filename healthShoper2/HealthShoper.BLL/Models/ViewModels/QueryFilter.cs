using HealthShoper.DAL.Models.Enums;

namespace HealthShoper.BLL.Models.ViewModels;

public class QueryFilter
{
    public List<CategoryType> Category { get; set; } = [CategoryType.All];
    public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    public SortBy SortBy { get; set; } = SortBy.All;
    public DateTime? CreatedAt { get; set; }
    public string? Search { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}