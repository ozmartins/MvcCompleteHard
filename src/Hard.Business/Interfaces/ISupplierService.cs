using Hard.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hard.Business.Interfaces
{
    public interface ISupplierService : IDisposable
    {
        public Task Create(Supplier supplier);

        public Task Update(Supplier supplier);

        public Task UpdateAddress(Address address);

        public Task Delete(Guid id);
    }
}
