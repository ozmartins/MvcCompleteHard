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
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(HardDbContext Context) : base(Context)
        {
        }

        public async Task<Address> RecoverBySupplier(Guid supplierId)
        {
            return await Context
                .Addresses
                .AsNoTracking()                
                .FirstOrDefaultAsync(a => a.SupplierId == supplierId);
        }
    }
}
