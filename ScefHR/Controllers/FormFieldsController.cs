using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScefHR.Helpers;
using ScefHR.Models;

namespace ScefHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormFieldsController : ControllerBase
    {
        private readonly DataContext _context;

        public FormFieldsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/FormFields
        [HttpGet]
        public IEnumerable<FormField> GetFormFields()
        {
            return _context.FormFields;
        }

        // GET: api/FormFields/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormField([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var formField = await _context.FormFields.FindAsync(id);

            if (formField == null)
            {
                return NotFound();
            }

            return Ok(formField);
        }

        // PUT: api/FormFields/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormField([FromRoute] int id, [FromBody] FormField formField)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != formField.Id)
            {
                return BadRequest();
            }

            _context.Entry(formField).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormFieldExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FormFields
        [HttpPost]
        public async Task<IActionResult> PostFormField([FromBody] FormField formField)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FormFields.Add(formField);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormField", new { id = formField.Id }, formField);
        }

        // DELETE: api/FormFields/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormField([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var formField = await _context.FormFields.FindAsync(id);
            if (formField == null)
            {
                return NotFound();
            }

            _context.FormFields.Remove(formField);
            await _context.SaveChangesAsync();

            return Ok(formField);
        }

        private bool FormFieldExists(int id)
        {
            return _context.FormFields.Any(e => e.Id == id);
        }
    }
}