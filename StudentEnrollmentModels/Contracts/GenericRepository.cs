using StudentEnrollmentModels.Repositories;

namespace StudentEnrollmentModels.Contracts
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel
    {

        public Task<TEntity> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exist(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }

}
