using ScefHR.Helpers;
using ScefHR.Models;
using ScefHR.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ScefHR.Repositories
{
    public class ServiceFormRepository : IServiceFormRepository
    {
        protected DataContext _context { get; set; }
        public ServiceFormRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(ServiceForm serviceForm)
        {
            await _context.ServiceForms.AddAsync(serviceForm);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ServiceForm> Read()
        {
            return _context.Set<ServiceForm>();
        }

        public IQueryable<ServiceForm> ReadByCondition(Expression<Func<ServiceForm, bool>> expression)
        {
            return _context.Set<ServiceForm>().Where(expression);
        }

        public Task Update()
        {
            throw new NotImplementedException();
        }
    }
}
