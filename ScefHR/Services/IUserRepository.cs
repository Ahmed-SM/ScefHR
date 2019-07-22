using System.Threading.Tasks;
using ScefHR.Models;

namespace ScefHR.Services
{
    public interface IUserRepository
    {
        Task<string> Create(AppUser employee);
    }
}