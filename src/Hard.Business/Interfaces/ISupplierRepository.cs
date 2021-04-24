using Hard.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hard.Business.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> RecoverWithAddresses(Guid supplierId);

        Task<Supplier> RecoverWithAddressesAndProducts(Guid supplierId);
    }
}
