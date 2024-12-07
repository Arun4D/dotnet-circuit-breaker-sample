using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CgCustomer.Models;

namespace CgCustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CgCustomerDetailsController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CgCustomerDetailsController(CustomerContext context)
        {
            _context = context;
        }

        // GET: api/CgCustomerDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CgCustomerDetails>>> GetCustomerDetailsList()
        {
            return await _context.CustomerDetailsList.ToListAsync();
        }

        // GET: api/CgCustomerDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CgCustomerDetails>> GetCgCustomerDetails(long id)
        {
            var cgCustomerDetails = await _context.CustomerDetailsList.FindAsync(id);

            if (cgCustomerDetails == null)
            {
                return NotFound();
            }

            return cgCustomerDetails;
        }

        // PUT: api/CgCustomerDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCgCustomerDetails(long id, CgCustomerDetails cgCustomerDetails)
        {
            if (id != cgCustomerDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(cgCustomerDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CgCustomerDetailsExists(id))
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

        // POST: api/CgCustomerDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CgCustomerDetails>> PostCgCustomerDetails(CgCustomerDetails cgCustomerDetails)
        {
            _context.CustomerDetailsList.Add(cgCustomerDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction( nameof(GetCgCustomerDetails), new { id = cgCustomerDetails.Id }, cgCustomerDetails);
        }

        // DELETE: api/CgCustomerDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCgCustomerDetails(long id)
        {
            var cgCustomerDetails = await _context.CustomerDetailsList.FindAsync(id);
            if (cgCustomerDetails == null)
            {
                return NotFound();
            }

            _context.CustomerDetailsList.Remove(cgCustomerDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CgCustomerDetailsExists(long id)
        {
            return _context.CustomerDetailsList.Any(e => e.Id == id);
        }
    }
}
