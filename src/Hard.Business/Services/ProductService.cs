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
        IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
        }

        public async Task Create(Product product)
        {
            if (!ExecuteValidation(new ProductValidator(), product) || !ExecuteValidation(new ProductValidator(), product)) return;

            await _productRepository.Create(product);
        }

        public async Task Delete(Guid id)
        {
            await _productRepository.Delete(id);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidator(), product) || !ExecuteValidation(new ProductValidator(), product)) ;
            
            await _productRepository.Update(product);
        }
    }
}
