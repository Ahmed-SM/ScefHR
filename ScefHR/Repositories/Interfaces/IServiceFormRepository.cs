using ScefHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ScefHR.Repositories.Interfaces
{
    public interface IServiceFormRepository
    {
        Task Create(ServiceForm serviceForm);
        IQueryable<ServiceForm> Read();
        IQueryable<ServiceForm> ReadByCondition(Expression<Func<ServiceForm, bool>> expression);
        Task Update();
        Task Delete(int id);
    }
}
