﻿using Core.Entities;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories.Base
{
    public class BaseReadRepository<T>: IBaseReadRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _table;

        public BaseReadRepository(AppDbContext context)
        {
            _table = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _table.FindAsync(id);
        }
    }
}
