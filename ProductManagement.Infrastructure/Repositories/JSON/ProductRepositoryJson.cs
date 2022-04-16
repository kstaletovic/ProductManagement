using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductRepositoryJson : IProductRepository
    {
        private readonly string productsJsonPath = "JSON files/products.json";
        private readonly string categoriesJsonPath = "JSON files/categories.json";
        private readonly string manufacturersJsonPath = "JSON files/manufacturers.json";
        private readonly string suppliersJsonPath = "JSON files/suppliers.json";

        public IEnumerable<Product> GetProducts()
        {
            string json = File.ReadAllText(productsJsonPath);
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
            return products;
        }

        public Product GetProductById(int productId)
        {
            string json = File.ReadAllText("JSON files/products.json");
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
            return products.SingleOrDefault(p => p.ProductId == productId);
        }

        public void InsertProduct(Product product)
        {
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(productsJsonPath));
            Category category = JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(categoriesJsonPath)).SingleOrDefault(c => c.CategoryId == product.CategoryId);
            Manufacturer manufacturer = JsonConvert.DeserializeObject<List<Manufacturer>>(File.ReadAllText(manufacturersJsonPath)).SingleOrDefault(m => m.ManufacturerId == product.ManufacturerId);
            Supplier supplier = JsonConvert.DeserializeObject<List<Supplier>>(File.ReadAllText(suppliersJsonPath)).SingleOrDefault(s => s.SupplierId == product.SupplierId);
            products.Add(new Product
            {
                ProductId = products.Max(p => p.ProductId) + 1,
                ProductName = product.ProductName.Trim(),
                Description = string.IsNullOrWhiteSpace(product.Description) ? string.Empty : product.Description.Trim(),
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = category,
                ManufacturerId = product.ManufacturerId,
                Manufacturer = manufacturer,
                SupplierId = product.SupplierId,
                Supplier = supplier
            });
            File.WriteAllText(productsJsonPath, JsonConvert.SerializeObject(products));
        }

        public void UpdateProduct(Product product)
        {
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(productsJsonPath));
            Product existingProduct = products.SingleOrDefault(p => p.ProductId == product.ProductId);
            Category category = JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(categoriesJsonPath)).SingleOrDefault(c => c.CategoryId == product.CategoryId);
            Manufacturer manufacturer = JsonConvert.DeserializeObject<List<Manufacturer>>(File.ReadAllText(manufacturersJsonPath)).SingleOrDefault(m => m.ManufacturerId == product.ManufacturerId);
            Supplier supplier = JsonConvert.DeserializeObject<List<Supplier>>(File.ReadAllText(suppliersJsonPath)).SingleOrDefault(s => s.SupplierId == product.SupplierId);
            existingProduct.ProductName = product.ProductName.Trim();
            existingProduct.Description = string.IsNullOrWhiteSpace(product.Description) ? string.Empty : product.Description.Trim();
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Category = category;
            existingProduct.ManufacturerId = product.ManufacturerId;
            existingProduct.Manufacturer = manufacturer;
            existingProduct.SupplierId = product.SupplierId;
            existingProduct.Supplier = supplier;
            File.WriteAllText(productsJsonPath, JsonConvert.SerializeObject(products));
        }

        public void DeleteProduct(Product product)
        {
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(productsJsonPath));
            Product existingProduct = products.SingleOrDefault(p => p.ProductId == product.ProductId);
            products.Remove(existingProduct);
            File.WriteAllText(productsJsonPath, JsonConvert.SerializeObject(products));
        }
    }
}
