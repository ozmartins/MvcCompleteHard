using Hard.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hard.Business.Interfaces
{
    public interface IProductService : IDisposable
    {
        public Task Create(Product product);

        public Task Update(Product product);

        public Task Delete(Guid id);
    }
}
