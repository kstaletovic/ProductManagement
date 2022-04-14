using ProductManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductManagementContext _context;
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IManufacturerRepository ManufacturerRepository { get; }
        public ISupplierRepository SupplierRepository { get; }

        public UnitOfWork(ProductManagementContext context, IProductRepository productRepository, ICategoryRepository categoryRepository,
            IManufacturerRepository manufacturerRepository, ISupplierRepository supplierRepository)
        {
            _context = context;
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
            ManufacturerRepository = manufacturerRepository;
            SupplierRepository = supplierRepository;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
