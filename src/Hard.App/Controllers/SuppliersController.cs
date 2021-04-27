using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hard.App.Data;
using Hard.App.ViewModels;
using Hard.Business.Interfaces;
using AutoMapper;
using Hard.Business.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hard.App.Controllers
{
    [Authorize]
    public class SuppliersController : BaseControler
    {
        private readonly ISupplierRepository _supplierRepository;

        private readonly IAddressRepository _addressRepository;

        private readonly IMapper _mapper; 

        public SuppliersController(ISupplierRepository supplierRepository, IAddressRepository addressRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await recoverViewList());
        }
        
        public async Task<IActionResult> Details(Guid id)
        {            
            var supplierViewModel =  await recoverViewModel(id);

            if (supplierViewModel == null) return NotFound();

            return View(supplierViewModel);
        }        

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid) return View(supplierViewModel);
            
            await _supplierRepository.Create(viewToModel(supplierViewModel));
            
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Edit(Guid id)
        {           
            var supplierViewModel = await recoverViewModel(id);

            if (supplierViewModel == null) return NotFound();
            
            return View(supplierViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id) return NotFound();            

            if (!ModelState.IsValid) return View(supplierViewModel);
            
            await _supplierRepository.Update(viewToModel(supplierViewModel));
                
            return RedirectToAction(nameof(Index));                        
        }
        
        public async Task<IActionResult> Delete(Guid id)
        {            
            var supplierViewModel = await recoverViewModel(id);
            
            if (supplierViewModel == null) return NotFound();

            return View(supplierViewModel);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplier = await _supplierRepository.RecoverWithAddress(id);
            
            await _addressRepository.Delete(supplier.Address.Id);

            await _supplierRepository.Delete(id);
            
            return RedirectToAction(nameof(Index));
        }

        private SupplierViewModel modelToView(Supplier model)
        {
            return _mapper.Map<SupplierViewModel>(model);
        }

        private Supplier viewToModel(SupplierViewModel view)
        {
            return _mapper.Map<Supplier>(view);
        }

        private async Task<IEnumerable<SupplierViewModel>> recoverViewList()
        {
            return _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.RecoverAll());
        }

        private async Task<SupplierViewModel> recoverViewModel(Guid id)
        {
            return modelToView(await _supplierRepository.RecoverWithAddress(id));
        }
    }
}
