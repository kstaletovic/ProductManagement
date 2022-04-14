using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ProductManagement.Application.ViewModels
{
    public class ProductEditVM
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "This field can be maximum {1} characters long.")]
        public string ProductName { get; set; }
        [StringLength(500, ErrorMessage = "This field can be maximum {1} characters long.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public double Price { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int ManufacturerId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int SupplierId { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Manufacturers { get; set; }
        public List<SelectListItem> Suppliers { get; set; }

        public static ProductEditVM ToViewModel(Product product, IEnumerable<Category> categories, IEnumerable<Manufacturer> manufacturers, IEnumerable<Supplier> suppliers)
        {
            return new ProductEditVM()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ManufacturerId = product.ManufacturerId,
                SupplierId = product.SupplierId,
                Categories = categories.Select(c => new SelectListItem()
                {
                    Text = c.CategoryName,
                    Value = c.CategoryId.ToString()
                }).ToList(),
                Manufacturers = manufacturers.Select(m => new SelectListItem()
                {
                    Text = m.ManufacturerName,
                    Value = m.ManufacturerId.ToString()
                }).ToList(),
                Suppliers = suppliers.Select(s => new SelectListItem()
                {
                    Text = s.SupplierName,
                    Value = s.SupplierId.ToString()
                }).ToList()
            };
        }
    }
}
