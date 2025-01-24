using Core.Entities;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
namespace Data.Repositories.Base
{
    public class BaseWriteRepository<T> : IBaseWriteRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _table;

        public BaseWriteRepository(AppDbContext context)
        {
            _table = context.Set<T>();
        }



        public async Task CreateAsync(T data)
        {
            _table.Add(data);
        }
        public void Update(T data)
        {
            _table.Update(data);
        }
        public void Delete(T data)
        {
            _table.Remove(data);
        }
    }
}
