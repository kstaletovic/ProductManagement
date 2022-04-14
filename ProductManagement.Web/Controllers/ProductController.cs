using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetDataTable()
        {
            IEnumerable<ProductDataTableVM> dataTable = _productService.GetProductsForDataTable();
            return Json(new { data = dataTable });
        }

        public ActionResult Create()
        {
            ProductCreateVM model = _productService.GetProductCreateVM();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreateVM model)
        {
            if (ModelState.IsValid)
            {
                _productService.InsertProduct(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ProductEditVM model = _productService.GetProductEditVM(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductEditVM model)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
