using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TicketVibe.Entities;

namespace TicketVibe.Repositories
{
    public interface IRepositoryBase<TEntiy> where TEntiy : EntityBase
    {
        Task<ICollection<TEntiy>> GetAllAsync();

        Task<ICollection<TEntiy>> GetAllAsync(Expression<Func<TEntiy, bool>> predicate);

        Task<ICollection<TEntiy>> GetAllAsync<TKey>(Expression<Func<TEntiy, bool>> predicate, Expression<Func<TEntiy, TKey>> orderBy);


        Task<TEntiy?> GetByIdAsync(int id);

        Task<int> AddAsync(TEntiy entity);

        Task UpdateAsync();
        
        Task DeleteAsync(int id);


    }
}
