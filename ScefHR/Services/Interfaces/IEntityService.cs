using ScefHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScefHR.Services
{
    public interface IEntityService
    {
        Task Create(string name);
        IQueryable Read(string userId);
        IQueryable AdminRead();
        IQueryable SRead();
        Task Update(int? id);
        Task Delete(int id);
    }
}
