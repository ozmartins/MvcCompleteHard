using Hard.Business.Interfaces;
using Hard.Business.Models;
using Hard.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hard.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        public Task Create(Product product)
        {
            if (!ExecuteValidation(new ProductValidator(), product) || !ExecuteValidation(new ProductValidator(), product))
                return Task.CompletedTask;
            else
                return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidator(), product) || !ExecuteValidation(new ProductValidator(), product))
                return Task.CompletedTask;
            else
                return Task.CompletedTask;
        }
    }
}
