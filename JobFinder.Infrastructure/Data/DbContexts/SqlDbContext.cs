using JobFinder.Domain.Interfaces;
using JobFinder.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using JobFinder.Domain.Entities;

namespace JobFinder.Infrastructure.Data.DbContexts
{
    public class SqlDbContext : DbContext, IDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SqlDbContext(DbContextOptions<SqlDbContext> options, IConfiguration configuration) : base(options)
        {
            _httpContextAccessor = new HttpContextAccessor();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var decimals = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal));
            foreach (var property in decimals)
                property.SetColumnType("decimal(18, 6)");

            foreach (var entityType in DbContextHelper.GetEntityTypes())
            {
                var method = DbContextHelper.SetGlobalQueryMethod.MakeGenericMethod(entityType);
                method.Invoke(this, new object[] { modelBuilder });
            }

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(ConstValues.ConnectionString);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            AddTimestamps(_httpContextAccessor);
            return await base.SaveChangesAsync(token);
        }

        public override int SaveChanges()
        {
            AddTimestamps(_httpContextAccessor);
            return base.SaveChanges();
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        #region Private
        private void AddTimestamps(IHttpContextAccessor httpContextAccessor)
        {
            var currentUserId = 0;
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            if (_httpContextAccessor.HttpContext != null)
                int.TryParse(httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == "personId")
                    .Select(c => c.Value).FirstOrDefault(), out currentUserId);

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDt = DateTime.UtcNow;
                    ((BaseEntity)entity.Entity).CreatedBy = currentUserId;
                }
                ((BaseEntity)entity.Entity).UpdatedDt = DateTime.UtcNow;
                ((BaseEntity)entity.Entity).UpdatedBy = currentUserId;
            }
        }

        private void SetGlobalQuery<T>(ModelBuilder builder) where T : BaseEntity
        {
            builder.Entity<T>().HasKey(e => e.Id);
            builder.Entity<T>().HasQueryFilter(x => EF.Property<bool>(x, nameof(x.IsDeleted)) == false);
        }
        #endregion
    }
}