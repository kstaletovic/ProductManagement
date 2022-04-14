using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductRepositoryDb : IProductRepository
    {
        private readonly ProductManagementContext _context;
        public ProductRepositoryDb(ProductManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public void InsertProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}
