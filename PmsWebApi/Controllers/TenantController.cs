using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PmsWebApi.Models;

namespace PmsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {

        private readonly TenantContext _context;

        public TenantController(TenantContext context)
        {
            _context = context;
        }

        // GET: api/Tenant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tenant>>> GetTenant()
        {
            //return await _context.Tenant.ToListAsync();

            return await _context.Tenant.ToListAsync();
        }

        // GET: api/Tenant/5
        [HttpGet("{UserId}")]
        public async Task<ActionResult<Tenant>> GetTenant(int UserId)
        {
            var Tenant = await _context.Tenant.FindAsync(UserId);

            if (Tenant == null)
            {
                return NotFound();
            }

            return Tenant;
        }

        // POST: api/Tenant
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Tenant>> PostTenant(Tenant Tenant, int UserId)
        {
            _context.Tenant.Add(Tenant);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TenantExists(UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTenant", new { UserId = Tenant.UserId }, Tenant);
        }

        // PUT: api/Tenant/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD
        [HttpPut("{UserId}")]
        public async Task<IActionResult> PutTenant(int UserId, Tenant Tenant)
        {
            if (UserId != Tenant.UserId)
            {
                return BadRequest();
            }

            _context.Entry(Tenant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenantExists(UserId))
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



        // DELETE: api/ApiWithActions/5
        [HttpDelete("{UserId}")]
        public async Task<ActionResult<Tenant>> DeleteTenant(int UserId)
        {
            var Tenant = await _context.Tenant.FindAsync(UserId);
            if (Tenant == null)
            {
                return NotFound();
            }

            _context.Tenant.Remove(Tenant);
            await _context.SaveChangesAsync();

            return Tenant;
        }

        private bool TenantExists(int UserId)
        {
            return _context.Tenant.Any(e => e.UserId == UserId);
        }
    }
}

