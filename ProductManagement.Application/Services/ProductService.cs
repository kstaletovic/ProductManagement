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
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ProductDataTableVM> GetProductsForDataTable()
        {
            return _unitOfWork.ProductRepository.GetProducts()
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
            IEnumerable<Category> categories = _unitOfWork.CategoryRepository.GetCategories();
            IEnumerable<Manufacturer> manufacturers = _unitOfWork.ManufacturerRepository.GetManufacturers();
            IEnumerable<Supplier> suppliers = _unitOfWork.SupplierRepository.GetSuppliers();
            return ProductCreateVM.ToViewModel(categories, manufacturers, suppliers);
        }

        public ProductEditVM GetProductEditVM(int productId)
        {
            IEnumerable<Category> categories = _unitOfWork.CategoryRepository.GetCategories();
            IEnumerable<Manufacturer> manufacturers = _unitOfWork.ManufacturerRepository.GetManufacturers();
            IEnumerable<Supplier> suppliers = _unitOfWork.SupplierRepository.GetSuppliers();
            Product product = _unitOfWork.ProductRepository.GetProductById(productId);
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
            _unitOfWork.ProductRepository.InsertProduct(product);
            _unitOfWork.Complete();
        }

        public void UpdateProduct(ProductEditVM model)
        {
            Product product = _unitOfWork.ProductRepository.GetProductById(model.ProductId);
            product.ProductName = model.ProductName.Trim();
            product.Description = string.IsNullOrWhiteSpace(model.Description) ? string.Empty : model.Description.Trim();
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            product.ManufacturerId = model.ManufacturerId;
            product.SupplierId = model.SupplierId;
            _unitOfWork.ProductRepository.UpdateProduct(product);
            _unitOfWork.Complete();
        }

        public void DeleteProduct(int productId)
        {
            Product product = _unitOfWork.ProductRepository.GetProductById(productId);
            _unitOfWork.ProductRepository.DeleteProduct(product);
            _unitOfWork.Complete();
        }
    }
}
