using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScefHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScefHR.Services
{
    public interface IServiceFormService
    {
        void Create(ServiceForm serviceForm);
        IQueryable<ServiceForm> Read();
        Task<bool> Update(int id, ServiceForm serviceForm);
        Task<EntityEntry<ServiceForm>> Delete(int id);
    }
}
