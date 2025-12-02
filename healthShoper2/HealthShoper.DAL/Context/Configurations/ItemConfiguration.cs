using System.Text.Json;
using HealthShoper.DAL.Models;
using HealthShoper.DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthShoper.DAL.Context.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder
            .Property(e => e.Category)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<IEnumerable<CategoryType>>(v, (JsonSerializerOptions)null)
            )
            .HasColumnType("text"); // будет храниться как JSON-строка
    }
}