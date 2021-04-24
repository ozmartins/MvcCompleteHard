using Hard.Business.Interfaces;
using Hard.Business.Models;
using Hard.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hard.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(HardDbContext Context) : base(Context)
        {

        }
        public async Task<Product> RecoverWithSupplier(Guid productId)
        {
            return await Context
                .Products
                .AsNoTracking()
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<Product>> RecoverAllWithSupplier()
        {
            return await Context
                .Products
                .AsNoTracking()
                .Include(p => p.Supplier)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> RecoverBySupplier(Guid supplierId)
        {
            return await Search(p => p.SupplierId == supplierId);
        }
    }
}
