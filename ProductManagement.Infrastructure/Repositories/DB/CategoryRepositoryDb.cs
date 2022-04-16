using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Infrastructure.Repositories
{
    public class CategoryRepositoryDb : ICategoryRepository
    {
        private readonly ProductManagementContext _context;
        public CategoryRepositoryDb(ProductManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories;
        }
    }
}
