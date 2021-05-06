using AutoMapper;
using Hard.App.ViewModels;
using Hard.Business.Interfaces;
using Hard.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hard.App.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        private readonly IProductRepository _productRepository;        

        private readonly ISupplierRepository _supplierRepository;

        private readonly IMapper _mapper;

        public ProductsController(IProductService productService,
                                  IProductRepository productRepository,
                                  ISupplierRepository supplierRepository,
                                  IMapper mapper,
                                  INotifier notifier) : base(notifier)
        {
            _productService = productService;
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

            var model = viewToModel(productViewModel);

            await _productService.Create(model);            

            await UploadFile(model.Id, productViewModel);

            if (!ValidOperation()) return View(productViewModel);

            TempData["Success"] = "Product succesfully created";

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
            if (id != productViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(productViewModel);

            var model = await _productRepository.Recover(id);

            model.Active = productViewModel.Active;
            model.Description = productViewModel.Description;
            model.Image = productViewModel.Image;
            model.Name = productViewModel.Name;
            model.Price = productViewModel.Price;            

            await _productService.Update(model);

            await UploadFile(id, productViewModel);

            if (!ValidOperation()) return View(productViewModel);

            TempData["Success"] = "Product succesfully updated";

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
            await _productService.Delete(id);

            if (!ValidOperation()) return View(_productRepository.Recover(id));

            TempData["Success"] = "Product succesfully removed";

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

            return view;
        }

        private async Task<IEnumerable<SupplierViewModel>> recoverSuppliersList()
        {
            return _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.RecoverAll());
        }

        private async Task UploadFile(Guid productId, ProductViewModel productViewModel)
        {
            if (productViewModel.UploadImage == null) return;

            if (productViewModel.UploadImage.Length == 0) return;
           
            var directoryName = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{productId}");

            if (!Directory.Exists(directoryName)) Directory.CreateDirectory(directoryName);                                    

            var fileName = Path.Combine(directoryName, productViewModel.Image);

            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                await productViewModel.UploadImage.CopyToAsync(stream);
            }            
        }

        
    }
}
