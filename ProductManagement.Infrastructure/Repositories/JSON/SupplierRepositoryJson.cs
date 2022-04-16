using Newtonsoft.Json;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductManagement.Infrastructure.Repositories
{
    public class SupplierRepositoryJson : ISupplierRepository
    {
        private string jsonPath = "JSON files/suppliers.json";
        public IEnumerable<Supplier> GetSuppliers()
        {
            var json = File.ReadAllText(jsonPath);
            List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(json);
            return suppliers;
        }
    }
}
