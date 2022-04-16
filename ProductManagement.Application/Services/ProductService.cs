using ProductManagement.Application.Interfaces;
using ProductManagement.Application.ViewModels;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ISupplierRepository _supplierRepository;
        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IManufacturerRepository manufacturerRepository, ISupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _manufacturerRepository = manufacturerRepository;
            _supplierRepository = supplierRepository;
        }

        public IEnumerable<ProductDataTableVM> GetProductsForDataTable()
        {
            return _productRepository.GetProducts()
                .Select(p => new ProductDataTableVM
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.Description,
                    ProductPrice = p.Price,
                    ProductCategory = p.Category.CategoryName,
                    ProductManufacturer = p.Manufacturer.ManufacturerName,
                    ProductSupplier = p.Supplier.SupplierName
                });
        }

        public ProductCreateVM GetProductCreateVM()
        {
            IEnumerable<Category> categories = _categoryRepository.GetCategories();
            IEnumerable<Manufacturer> manufacturers = _manufacturerRepository.GetManufacturers();
            IEnumerable<Supplier> suppliers = _supplierRepository.GetSuppliers();
            return ProductCreateVM.ToViewModel(categories, manufacturers, suppliers);
        }

        public ProductEditVM GetProductEditVM(int productId)
        {
            IEnumerable<Category> categories = _categoryRepository.GetCategories();
            IEnumerable<Manufacturer> manufacturers = _manufacturerRepository.GetManufacturers();
            IEnumerable<Supplier> suppliers = _supplierRepository.GetSuppliers();
            Product product = _productRepository.GetProductById(productId);
            return ProductEditVM.ToViewModel(product, categories, manufacturers, suppliers);
        }

        public void InsertProduct(ProductCreateVM model)
        {
            Product product = new Product
            {
                ProductName = model.ProductName.Trim(),
                Description = string.IsNullOrWhiteSpace(model.Description) ? string.Empty : model.Description.Trim(),
                Price = model.Price,
                CategoryId = model.CategoryId,
                ManufacturerId = model.ManufacturerId,
                SupplierId = model.SupplierId
            };
            _productRepository.InsertProduct(product);
        }

        public void UpdateProduct(ProductEditVM model)
        {
            Product product = _productRepository.GetProductById(model.ProductId);
            product.ProductName = model.ProductName.Trim();
            product.Description = string.IsNullOrWhiteSpace(model.Description) ? string.Empty : model.Description.Trim();
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            product.ManufacturerId = model.ManufacturerId;
            product.SupplierId = model.SupplierId;
            _productRepository.UpdateProduct(product);
        }

        public void DeleteProduct(int productId)
        {
            Product product = _productRepository.GetProductById(productId);
            _productRepository.DeleteProduct(product);
        }
    }
}
