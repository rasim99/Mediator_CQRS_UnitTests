using Core.Entities;
namespace Data.Repositories.Base
{
    public interface IBaseReadRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
    }
}
