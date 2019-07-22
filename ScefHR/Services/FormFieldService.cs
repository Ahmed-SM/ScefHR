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
            _context.SaveChanges();
        }
        public void Create(FormField formField)
        {
            _context.FormFields.Add(formField);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.FormFields.Remove(_context.FormFields.Find(id));
            _context.SaveChanges();
        }

        public IQueryable<FormField> Read()
        {
            return _context.Set<FormField>();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
