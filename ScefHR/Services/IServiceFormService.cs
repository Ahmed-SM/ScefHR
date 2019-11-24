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
        Task Create(ServiceForm serviceForm, string userId);
        IQueryable Read(string userId);
        IQueryable Read(string userId, int it);
        Task<bool> Update(int id, ServiceForm serviceForm);
        Task<EntityEntry<ServiceForm>> Delete(int id);
        IQueryable AdminOnHoldRead(string userId);
        IQueryable AdminAcceptedRead(string userId);
        IQueryable OnHoldRead(string userId);
        IQueryable AcceptedRead(string userId);
        IQueryable AdminOnHoldRead(int id, string userId);
        IQueryable AdmiAcceptRead(int id, string userId);
        Task<bool> AdminOnHoldAccept(string userId, int formId);
        Task<bool> AdminOnHoldRefuse(string userId, int formId);
    }
}
