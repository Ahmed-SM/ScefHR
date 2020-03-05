using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ScefHR.Models;

namespace ScefHR.Services
{
    public interface IUserRepository
    {
        Task Create(AppUser entity);
        IQueryable<AppUser> Read();
        AppUser Read(string id);
        IQueryable<AppUser> ReadByCondition(Expression<Func<AppUser, bool>> expression);
        Task Update();
        Task Delete(int id);
    }
}