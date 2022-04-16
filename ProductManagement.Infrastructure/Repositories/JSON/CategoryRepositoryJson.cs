using Newtonsoft.Json;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductManagement.Infrastructure.Repositories
{
    public class CategoryRepositoryJson : ICategoryRepository
    {
        private string jsonPath = "JSON files/categories.json";

        public IEnumerable<Category> GetCategories()
        {
            var json = File.ReadAllText(jsonPath);
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(json);
            return categories;
        }
    }
}
