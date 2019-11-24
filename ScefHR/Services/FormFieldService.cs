using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScefHR.Helpers;
using ScefHR.Models;

namespace ScefHR.Services
{
    public class FormFieldService : IFormFieldService
    {
        private DataContext _context;
        public FormFieldService(DataContext context)
        {
            _context = context;
        }
        public async Task Create(FormField formField)
        {
            _context.FormFields.Add(formField);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.FormFields.Remove(_context.FormFields.Find(id));
            await _context.SaveChangesAsync();
        }

        public IQueryable<FormField> Read()
        {
            return _context.Set<FormField>();
        }

        public Task Update()
        {
            throw new NotImplementedException();
        }
    }
}
