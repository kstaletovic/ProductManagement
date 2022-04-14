using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly ProductManagementContext _context;
        public ManufacturerRepository(ProductManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            return _context.Manufacturers;
        }
    }
}
