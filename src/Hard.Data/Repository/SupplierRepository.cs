using Hard.Business.Interfaces;
using Hard.Business.Models;
using Hard.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hard.Data.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(HardDbContext Context) : base(Context)
        {

        }

        public async Task<Supplier> RecoverWithAddress(Guid supplierId)
        {
            return await Context
                .Suppliers
                .AsNoTracking()
                .Include(s => s.Address)
                .FirstOrDefaultAsync(s => s.Id == supplierId);
        }

        public async Task<Supplier> RecoverWithAddressAndProducts(Guid supplierId)
        {
            return await Context
                .Suppliers
                .AsNoTracking()
                .Include(s => s.Address)
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == supplierId);
        }
    }
}
