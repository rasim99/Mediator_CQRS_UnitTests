using Data.Repositories.Base;
namespace Data.Repositories.Abstract.Product
{
    public interface IProductReadRepository : IBaseReadRepository<Core.Entities.Product>
    {
        Task<Core.Entities.Product> GetByNameAsync(string name);

    }
}
