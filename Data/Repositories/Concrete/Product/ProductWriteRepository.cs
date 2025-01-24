using Data.Contexts;
using Data.Repositories.Abstract.Product;
using Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete.Product
{
    public class ProductWriteRepository : BaseWriteRepository<Core.Entities.Product>, IProductWriteRepository
    {
        public ProductWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
