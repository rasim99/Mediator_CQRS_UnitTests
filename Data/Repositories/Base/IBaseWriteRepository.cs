using Core.Entities;
namespace Data.Repositories.Base
{
    public interface IBaseWriteRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T data);
        void Update(T data);
        void Delete(T data);
    }
}
