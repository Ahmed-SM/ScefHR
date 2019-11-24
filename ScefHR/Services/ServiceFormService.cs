using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScefHR.Helpers;
using ScefHR.Models;

namespace ScefHR.Services
{
    public class ServiceFormService : IServiceFormService
    {
        private DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ServiceFormService(DataContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            //_context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();
        }
        public async Task Create(ServiceForm serviceForm, string userId)
        {
            var employee = _context.Employees.Where(t => t.IdentityId == userId).FirstOrDefault();
            serviceForm.IssueDate = DateTime.Now;
            employee.ServiceForms.Add(serviceForm);
            _context.Employees.Update(employee);
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

        public IQueryable Read(string userId)
        {
            return _context.ServiceForms.Where(UserId => UserId.Employee.IdentityId == userId).Select(s => new {
                s.Id,
                s.Name,
                s.IssueDate,
                s.FormFields,
                s.Status
            });
        }
        public IQueryable AdminOnHoldRead(string userId)
        {
            var admin = _context.Employees.Where(t => t.IdentityId == userId).Include(e => e.Entity).FirstOrDefault();
            if (admin.Entity != null)
            {
                return _context.ServiceForms.Where(e => e.Employee.Entity == admin.Entity && e.Status == 0).Select(s => new
                {
                    s.Employee.Identity.FirstName,
                    s.Employee.Identity.LastName,
                    s.Id,
                    s.Name,
                    s.IssueDate,
                    s.FormFields
                });
            }
            return null;
        }
        public IQueryable AdminAcceptedRead(string userId)
        {
            var admin = _context.Employees.Where(t => t.IdentityId == userId).Include(e => e.Entity).FirstOrDefault();
            if (admin.Entity != null)
            {
                return _context.ServiceForms.Where(e => e.Employee.Entity == admin.Entity && e.Status != 0).Select(s => new
                {
                    s.Employee.Identity.FirstName,
                    s.Employee.Identity.LastName,
                    s.Id,
                    s.Name,
                    s.IssueDate,
                    s.FormFields
                });
            }
            return null;
        }
        public IQueryable Read(string userId, int id)
        {
            return _context.ServiceForms.Where(s => s.Employee.IdentityId == userId && s.Id == id).Select(s => new {
                s.Employee.Identity.FirstName,
                s.Employee.Identity.LastName,
                s.Id,
                s.Name,
                s.IssueDate,
                s.FormFields,
                s.Status

            });
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

        public IQueryable AdminOnHoldRead(int id, string userId)
        {
            var admin = _context.Employees.Where(t => t.IdentityId == userId).Include(e => e.Entity).FirstOrDefault();
            if (admin.Entity != null)
            {
                return _context.ServiceForms.Where(e => e.Employee.Entity == admin.Entity && e.Status == 0 && e.Id == id).Select(s => new
                {
                    s.Name,
                    s.Employee.Identity.FirstName,
                    s.Employee.Identity.LastName,
                    s.IssueDate,
                    s.FormFields
                });
            }
            return null;
        }
        public IQueryable AdmiAcceptRead(int id, string userId)
        {
            var admin = _context.Employees.Where(t => t.IdentityId == userId).Include(e => e.Entity).FirstOrDefault();
            if (admin.Entity != null)
            {
                return _context.ServiceForms.Where(e => e.Employee.Entity == admin.Entity && e.Status != 0 && e.Id == id).Select(s => new
                {
                    s.Name,
                    s.Employee.Identity.FirstName,
                    s.Employee.Identity.LastName,
                    s.IssueDate,
                    s.FormFields
                });
            }
            return null;
        }


        public async Task<bool> AdminOnHoldAccept(string userId, int formId)
        {
            var form =_context.ServiceForms.Find(formId);
            form.Status = 1;
            _context.ServiceForms.Update(form);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AdminOnHoldRefuse(string userId, int formId)
        {
            var form = _context.ServiceForms.Find(formId);
            form.Status = 0;
            _context.ServiceForms.Update(form);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable OnHoldRead(string userId)
        {
            return _context.ServiceForms.Where(t => t.Employee.IdentityId == userId && t.Status == 0).Select(s => new
            {
                s.Name,
                s.Employee.Identity.FirstName,
                s.Employee.Identity.LastName,
                s.IssueDate,
                s.FormFields,
                s.Status
            });
        }

        public IQueryable AcceptedRead(string userId)
        {
                return _context.ServiceForms.Where(t => t.Employee.IdentityId == userId && t.Status != 0 ).Select(s => new
                {
                    s.Name,
                    s.Employee.Identity.FirstName,
                    s.Employee.Identity.LastName,
                    s.IssueDate,
                    s.FormFields,
                    s.Status
                });
        }

    }
}
