using Hard.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hard.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> RecoverWithSupplier(Guid productId);

        Task<IEnumerable<Product>> RecoverAllWithSupplier();

        Task<IEnumerable<Product>> RecoverBySupplier(Guid supplierId);
    }
}
