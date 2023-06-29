using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollmentModels.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseModel
    {
        Task<TEntity> GetAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity); 
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(int Id);
        Task<bool> Exist (TEntity entity);
       
    }

}
