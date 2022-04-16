using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Infrastructure.Repositories
{
    public class SupplierRepositoryDb : ISupplierRepository
    {
        private readonly ProductManagementContext _context;
        public SupplierRepositoryDb(ProductManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _context.Suppliers;
        }
    }
}
