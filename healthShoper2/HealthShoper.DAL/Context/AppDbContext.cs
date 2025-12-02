using System.Reflection;
using HealthShoper.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthShoper.DAL.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    async Task IApplicationDbContext.SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken);
    }

    public async Task InvokeTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction != null)
        {
            await action();
            return;
        }

        await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await action();
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<T> InvokeTransactionAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction != null)
        {
            return await action();
        }

        await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await action();
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}