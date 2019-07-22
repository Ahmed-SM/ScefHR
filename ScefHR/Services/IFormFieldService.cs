using ScefHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScefHR.Services
{
    public interface IFormFieldService
    {
        void Create(FormField formField);
        IQueryable<FormField> Read();
        void Update();
        void Delete(int id);
    }
}
