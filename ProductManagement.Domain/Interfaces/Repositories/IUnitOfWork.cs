using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IManufacturerRepository ManufacturerRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        int Complete();
    }
}
