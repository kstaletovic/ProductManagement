using ProductManagement.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDataTableVM> GetProductsForDataTable();
        ProductCreateVM GetProductCreateVM();
        ProductEditVM GetProductEditVM(int productId);
        void InsertProduct(ProductCreateVM model);
        void UpdateProduct(ProductEditVM model);
        void DeleteProduct(int productId);
    }
}
