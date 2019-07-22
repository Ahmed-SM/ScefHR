using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScefHR.Helpers;
using ScefHR.Models;

namespace ScefHR.Services
{
    public class ServiceFormService : IServiceFormService
    {
        private DataContext _context;

        public ServiceFormService(DataContext context)
        {
            _context = context;
            _context.SaveChanges();
        }
        public async void Create(ServiceForm serviceForm)
        {
            _context.ServiceForms.Add(serviceForm);
            await _context.SaveChangesAsync();
        }

        public async Task<EntityEntry<ServiceForm>> Delete(int id)
        {
            var serviceForm =  _context.ServiceForms.Remove(await _context.ServiceForms.FindAsync(id));
            if (serviceForm == null)
            {
                return null;
            }
            await _context.SaveChangesAsync();
            return serviceForm;
        }

        public IQueryable<ServiceForm> Read()
        {
            return _context.Set<ServiceForm>();
        }

        private bool ServiceFormExists(int id)
        {
            return _context.ServiceForms.Any(e => e.Id == id);
        }

        public async Task<bool> Update(int id, ServiceForm serviceForm)
        {
            _context.Entry(serviceForm).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceFormExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }
    }
}
