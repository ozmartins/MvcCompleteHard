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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Hard.App.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        private readonly ISupplierRepository _supplierRepository;

        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository,
                                  ISupplierRepository supplierRepository,
                                  IMapper mapper)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

       
        public async Task<IActionResult> Index()
        {            
            return View(await recoverViewList());
        }
        
        public async Task<IActionResult> Details(Guid id)
        {            
            var productViewModel = await recoverViewModel(id);

            if (productViewModel == null) return NotFound();

            return View(productViewModel);
        }
        
        public async Task<IActionResult> Create()
        {
            var product = new ProductViewModel() { Suppliers = await recoverSuppliersList() };

            return View(product);
        }        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel.Suppliers = await recoverSuppliersList();

            if (!ModelState.IsValid) return View(productViewModel);

            await UploadFile(productViewModel.UploadImage, productViewModel.Image);

            await _productRepository.Create(viewToModel(productViewModel));

            return RedirectToAction(nameof(Index));                                   
        }        

        public async Task<IActionResult> Edit(Guid id)
        {            
            var productViewModel = await recoverViewModel(id);            

            if (productViewModel == null) return NotFound();
            
            return View(productViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            productViewModel.Suppliers = await recoverSuppliersList();

            if (id != productViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(productViewModel);

            await UploadFile(productViewModel.UploadImage, productViewModel.Image);

            await _productRepository.Update(viewToModel(productViewModel));

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(Guid id)
        {            
            var productViewModel = await recoverViewModel(id);

            if (productViewModel == null) return NotFound();

            return View(productViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _productRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }        

        private ProductViewModel modelToView(Product model)
        {
            return _mapper.Map<ProductViewModel>(model);
        }

        private Product viewToModel(ProductViewModel view)
        {
            return _mapper.Map<Product>(view);
        }

        private async Task<IEnumerable<ProductViewModel>> recoverViewList()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.RecoverAllWithSupplier());
        }

        private async Task<ProductViewModel> recoverViewModel(Guid id)
        {
            var view = modelToView(await _productRepository.RecoverWithSupplier(id));

            view.Suppliers = await recoverSuppliersList();

            return view;
        }

        private async Task<IEnumerable<SupplierViewModel>> recoverSuppliersList()
        {
            return _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.RecoverAll());
        }

        private async Task<bool> UploadFile(IFormFile image, string name)
        {
            if (image.Length == 0) return false;

            var directoryName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", name);

            using (var stream = new FileStream(directoryName, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return true;
        }
    }
}
