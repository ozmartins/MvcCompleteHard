using Hard.Business.Interfaces;
using Hard.Business.Models;
using Hard.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hard.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        public Task Create(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidator(), supplier) || !ExecuteValidation(new AddressValidator(), supplier.Address))
                return Task.CompletedTask;
            else
                return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidator(), supplier) || !ExecuteValidation(new AddressValidator(), supplier.Address))
                return Task.CompletedTask;
            else
                return Task.CompletedTask;
        }

        public Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidator(), address))
                return Task.CompletedTask;
            else
                return Task.CompletedTask;
        }
    }
}
