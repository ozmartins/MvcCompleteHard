using Hard.Business.Interfaces;
using Hard.Business.Models;
using Hard.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hard.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        ISupplierRepository _supplierRepository;
        IAddressRepository _addressRepository;

        public SupplierService(ISupplierRepository supplierRepository, IAddressRepository addressRepository, INotifier notifier) : base(notifier)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
        }

        public async Task Create(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidator(), supplier) || !ExecuteValidation(new AddressValidator(), supplier.Address)) return;

            if (_supplierRepository.Search(s => s.DocumentId == supplier.DocumentId).Result.Any())
            {
                Notify($"Already exists a supplier with the document {supplier.DocumentId}");
                return;
            }

            await _supplierRepository.Create(supplier);
        }

        public async Task Delete(Guid id)
        {
            if (_supplierRepository.RecoverWithAddressAndProducts(id).Result.Products.Any())
            {
                Notify("This supplier has associated products.");
                return;
            }
          
            var addressId = _supplierRepository.RecoverWithAddress(id).Result.Address.Id;

            await _addressRepository.Delete(addressId);

            await _supplierRepository.Delete(id);
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
            _addressRepository?.Dispose();
        }

        public async Task Update(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidator(), supplier) || !ExecuteValidation(new AddressValidator(), supplier.Address)) return;

            if (_supplierRepository.Search(s => s.DocumentId == supplier.DocumentId && s.Id != supplier.Id).Result.Any())
            {
                Notify($"Already exists a supplier with the document {supplier.DocumentId}");
                return;
            }

            await _supplierRepository.Update(supplier);
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidator(), address)) return;

            await _addressRepository.Update(address);
        }
    }
}
