using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JobFinder.Domain.Entities;

namespace JobFinder.Domain.Interfaces
{
    public interface IRepository
    {
        Task<(IQueryable<T> query, int count, int pageCount)> GetListByPaging<T>(IQueryable<T> query, int pageNumber, int pageSize) where T : BaseEntity;
        Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        Task<IEnumerable<T>> GetAllAsNoTrackingAsync<T>(params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        Task<T> GetByIdAsync<T>(long id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        Task<T> GetByIdAsNoTrackingAsync<T>(long id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        Task<IEnumerable<T>> FilterAsNoTrackingAsync<T>(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        IQueryable<T> GetAll<T>(params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        IQueryable<T> GetAllAsNoTracking<T>(params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        T GetById<T>(long id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        T GetByIdAsNoTracking<T>(long id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;

        IQueryable<T> Filter<T>(Expression<Func<T, bool>> query,
            params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;

        IQueryable<T> FilterAsNoTracking<T>(Expression<Func<T, bool>> query,
            params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;

        T Create<T>(T entity) where T : BaseEntity;
        Task<T> CreateAsync<T>(T entity) where T : BaseEntity;
        IList<T> CreateRange<T>(IList<T> entities) where T : BaseEntity;
        Task<IList<T>> CreateRangeAsync<T>(IList<T> entities) where T : BaseEntity;
        Task<bool> Remove<T>(long id) where T : BaseEntity;
        Task<bool> HardRemove<T>(long id) where T : BaseEntity;
        Task<bool> RemoveRange<T>(IList<long> ids) where T : BaseEntity;
        Task<bool> HardRemoveRange<T>(IList<long> ids) where T : BaseEntity;
        Task<int> SaveChanges();
        void Dispose();
        IDbContext GetContext();
    }
}