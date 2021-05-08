using AutoMapper;
using Hard.App.Extensions;
using Hard.App.ViewModels;
using Hard.Business.Interfaces;
using Hard.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hard.App.Controllers
{
    [Authorize]
    public class SuppliersController : BaseController
    {
        private readonly ISupplierRepository _supplierRepository;

        private readonly ISupplierService _supplierService;

        private readonly IMapper _mapper; 

        public SuppliersController(ISupplierRepository supplierRepository, ISupplierService supplierService, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _supplierRepository = supplierRepository;
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [ClaimsAuthorize("supplier", "recover")]
        public async Task<IActionResult> Index()
        {
            return View(await recoverViewList());
        }

        [ClaimsAuthorize("supplier", "recover")]
        public async Task<IActionResult> Details(Guid id)
        {            
            var supplierViewModel =  await recoverViewModel(id);

            if (supplierViewModel == null) return NotFound();

            return View(supplierViewModel);
        }

        [ClaimsAuthorize("supplier", "create")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("supplier", "create")]
        [HttpPost]        
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid) return View(supplierViewModel);
            
            await _supplierService.Create(viewToModel(supplierViewModel));

            if (!ValidOperation()) return View(supplierViewModel);

            TempData["Success"] = "Supplier succesfully created";

            return RedirectToAction(nameof(Index));
        }

        [ClaimsAuthorize("supplier", "update")]
        public async Task<IActionResult> Edit(Guid id)
        {           
            var supplierViewModel = await recoverViewModel(id);

            if (supplierViewModel == null) return NotFound();
            
            return View(supplierViewModel);
        }

        [ClaimsAuthorize("supplier", "update")]
        [HttpPost]        
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id) return NotFound();            

            if (!ModelState.IsValid) return View(supplierViewModel);

            var model = await _supplierRepository.RecoverWithAddress(id);
            model.Name = supplierViewModel.Name;
            model.DocumentType = (DocumentType)supplierViewModel.DocumentType;
            model.DocumentId = supplierViewModel.DocumentId;
            model.Active = supplierViewModel.Active;            
            await _supplierService.Update(model);

            if (!ValidOperation()) return View(supplierViewModel);

            TempData["Success"] = "Supplier succesfully edited";

            return RedirectToAction(nameof(Index));                        
        }

        [ClaimsAuthorize("supplier", "delete")]
        public async Task<IActionResult> Delete(Guid id)
        {            
            var supplierViewModel = await recoverViewModel(id);
            
            if (supplierViewModel == null) return NotFound();

            return View(supplierViewModel);
        }

        [ClaimsAuthorize("supplier", "delete")]
        [HttpPost, ActionName("Delete")]        
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplier = await _supplierRepository.RecoverWithAddress(id);                       

            await _supplierService.Delete(id);

            if (!ValidOperation()) return View(modelToView(supplier));

            TempData["Success"] = "Supplier succesfully deleted";

            return RedirectToAction(nameof(Index));
        }

        [ClaimsAuthorize("supplier", "update")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var supplier = await _supplierRepository.RecoverWithAddress(id);

            if (supplier == null) return NotFound();

            return PartialView("_UpdateAddress", new SupplierViewModel() { Address = _mapper.Map<AddressViewModel>(supplier.Address) });
        }

        [ClaimsAuthorize("supplier", "update")]
        [HttpPost]        
        public async Task<IActionResult> UpdateAddress(SupplierViewModel supplierViewModel)
        {
            ModelState.Remove("Name");

            ModelState.Remove("DocumentId");

            if (!ModelState.IsValid) return PartialView("_UpdateAddress", supplierViewModel);

            await _supplierService.UpdateAddress(_mapper.Map<Address>(supplierViewModel.Address));

            var url = Url.Action("GetAddress", "Suppliers", new { id = supplierViewModel.Id });

            return Json(new { Success = true, url = url});
        }

        [ClaimsAuthorize("supplier", "recover")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var supplier = await _supplierRepository.RecoverWithAddress(id);

            if (supplier == null) return NotFound();

            return PartialView("_DetailsAddress", new SupplierViewModel() { Address = _mapper.Map<AddressViewModel>(supplier.Address) });
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
            return modelToView(await _supplierRepository.RecoverWithAddressAndProducts(id));
        }
    }
}
