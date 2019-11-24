using ScefHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScefHR.Services
{
    public interface IFormFieldService
    {
        Task Create(FormField formField);
        IQueryable<FormField> Read();
        Task Update();
        Task Delete(int id);
    }
}
