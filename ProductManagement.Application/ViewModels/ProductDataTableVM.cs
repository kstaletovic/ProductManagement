using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.ViewModels
{
    public class ProductDataTableVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public string ProductCategory { get; set; }
        public string ProductManufacturer { get; set; }
        public string ProductSupplier { get; set; }
    }
}
