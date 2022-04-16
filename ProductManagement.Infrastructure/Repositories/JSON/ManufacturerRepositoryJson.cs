using Newtonsoft.Json;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ManufacturerRepositoryJson : IManufacturerRepository
    {
        private string jsonPath = "JSON files/manufacturers.json";

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            var json = File.ReadAllText(jsonPath);
            List<Manufacturer> manufacturers = JsonConvert.DeserializeObject<List<Manufacturer>>(json);
            return manufacturers;
        }
    }
}
