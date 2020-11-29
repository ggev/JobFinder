using System;
using System.Threading;
using System.Threading.Tasks;
using JobFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace JobFinder.Domain.Interfaces
{
    public interface IDbContext : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken token = default);
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        DatabaseFacade Database { get; }
        EntityEntry Entry(object entity);

    }
}