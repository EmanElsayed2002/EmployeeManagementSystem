using Data.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Infrastructure.Repo
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepo(AppDbContext context)
        {
            _dbContext = context;
        }
        public virtual async Task<Employee> GetByIdAsync(int id)
        {

            return await _dbContext.Set<Employee>().FindAsync(id);
        }


        public IQueryable<Employee> GetTableNoTracking()
        {
            return _dbContext.Set<Employee>().AsNoTracking().AsQueryable();
        }
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Set<Employee>().AsNoTracking().ToListAsync();
        }

        public virtual async Task AddRangeAsync(ICollection<Employee> entities)
        {
            await _dbContext.Set<Employee>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();

        }
        public virtual async Task<Employee> AddAsync(Employee entity)
        {
            await _dbContext.Set<Employee>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(Employee entity)
        {
            _dbContext.Set<Employee>().Update(entity);
            await _dbContext.SaveChangesAsync();

        }

        public virtual async Task<bool> DeleteAsync(int entity)
        {
            var emp = await _dbContext.Set<Employee>().FindAsync(entity);

            if (emp != null)
            {
                _dbContext.Set<Employee>().Remove(emp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public virtual async Task DeleteRangeAsync(ICollection<Employee> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }



        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext.Database.CommitTransaction();

        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public IQueryable<Employee> GetTableAsTracking()
        {
            return _dbContext.Set<Employee>().AsQueryable();

        }

        public virtual async Task UpdateRangeAsync(ICollection<Employee> entities)
        {
            _dbContext.Set<Employee>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollBackAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
        public async Task<IQueryable<Employee>> FindAllInclude(string[] Include = null)
        {
            IQueryable<Employee> obj = _dbContext.Set<Employee>();
            if (Include != null)
            {
                foreach (var item in Include)
                {
                    obj = obj.Include(item);
                }
            }
            return obj;
        }
        public async Task<IQueryable<Employee>> FindAllByInclude(Expression<Func<Employee, bool>> match, string[] Include = null)
        {
            IQueryable<Employee> obj = _dbContext.Set<Employee>();
            if (Include != null)
            {
                foreach (var item in Include)
                {
                    obj = obj.Include(item);
                }
            }
            return obj.Where(match);
        }
        public async Task<Employee> FindByInclude(Expression<Func<Employee, bool>> match, string[] Include = null)
        {
            IQueryable<Employee> obj = _dbContext.Set<Employee>();
            if (Include != null)
            {
                foreach (var item in Include)
                {
                    obj = obj.Include(item);
                }
            }
            return await obj.FirstOrDefaultAsync(match);
        }
    }
}
