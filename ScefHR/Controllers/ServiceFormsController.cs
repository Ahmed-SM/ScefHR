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
    public class ServiceFormsController : ControllerBase
    {
        private readonly DataContext _context;

        public ServiceFormsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ServiceForms
        [HttpGet]
        public IEnumerable<ServiceForm> GetServiceForms()
        {
            return _context.ServiceForms;
        }

        // GET: api/ServiceForms/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceForm([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceForm = await _context.ServiceForms.FindAsync(id);

            if (serviceForm == null)
            {
                return NotFound();
            }

            return Ok(serviceForm);
        }

        // PUT: api/ServiceForms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceForm([FromRoute] int id, [FromBody] ServiceForm serviceForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceForm.Id)
            {
                return BadRequest();
            }

            _context.Entry(serviceForm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceFormExists(id))
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

        // POST: api/ServiceForms
        [HttpPost]
        public async Task<IActionResult> PostServiceForm([FromBody] ServiceForm serviceForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _context.ServiceForms.Add(serviceForm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiceForm", new { id = serviceForm.Id }, serviceForm);
        }

        // DELETE: api/ServiceForms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceForm([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceForm = await _context.ServiceForms.FindAsync(id);
            if (serviceForm == null)
            {
                return NotFound();
            }

            _context.ServiceForms.Remove(serviceForm);
            await _context.SaveChangesAsync();

            return Ok(serviceForm);
        }

        private bool ServiceFormExists(int id)
        {
            return _context.ServiceForms.Any(e => e.Id == id);
        }
    }
}