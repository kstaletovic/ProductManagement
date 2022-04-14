using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Domain.Interfaces.Repositories
{
    public interface IManufacturerRepository
    {
        IEnumerable<Manufacturer> GetManufacturers();
    }
}
