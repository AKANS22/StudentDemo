using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollmentModels.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseModel
    {
        Task<TEntity> GetAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity); 
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task<bool> Exists (int id);
       
    }

}
