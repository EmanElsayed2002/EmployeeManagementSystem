using Data.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Infrastructure.Repo
{
    public interface IEmployeeRepo
    {
        Task DeleteRangeAsync(ICollection<Employee> entities);
        Task<Employee> GetByIdAsync(int id);
        Task SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
        IQueryable<Employee> GetTableNoTracking();
        IQueryable<Employee> GetTableAsTracking();
        Task<Employee> AddAsync(Employee entity);
        Task AddRangeAsync(ICollection<Employee> entities);
        Task UpdateAsync(Employee entity);
        Task UpdateRangeAsync(ICollection<Employee> entities);
        Task<bool> DeleteAsync(int id);

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();
        Task<IQueryable<Employee>> FindAllInclude(string[] Include = null);
        Task<IQueryable<Employee>> FindAllByInclude(Expression<Func<Employee, bool>> match, string[] Include = null);
        Task<Employee> FindByInclude(Expression<Func<Employee, bool>> match, string[] Include = null);
    }
}
