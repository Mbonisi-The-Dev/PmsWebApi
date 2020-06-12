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
    public class TenantLeaseController : ControllerBase
    {
        private readonly TenantLeaseContext _context;
        public TenantLeaseController(TenantLeaseContext context)
        {
            _context = context;
        }

        // GET: api/TenantLease
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantLease>>> GetTenantLease()
        {
            return await _context.TenantLease.ToListAsync();
        }

        // GET: api/Tenant/5
        [HttpGet("{IdNumber}")]
        public async Task<ActionResult<TenantLease>> GetTenantLease(string IdNumber)
        {
            var tenantLease = await _context.TenantLease.FindAsync(IdNumber);

            if (tenantLease == null)
            {
                return NotFound();
            }

            return tenantLease;
        }

        // POST: api/Tenant
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TenantLease>> PostTenantLease(TenantLease tenantLease, string IdNumber)
        {
            _context.TenantLease.Add(tenantLease);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TenantLeaseExists(IdNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetTenantLease", new { IdNumber = tenantLease.IdNumber }, tenantLease);
            return CreatedAtAction("GetTenantLease", new { tenantLease.IdNumber }, tenantLease);
        }


        // PUT: api/Tenant/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD
        [HttpPut("{IdNumber}")]
        public async Task<IActionResult> PutTenantLease(string IdNumber, TenantLease tenantLease)
        {
            if (IdNumber != tenantLease.IdNumber)
            {
                return BadRequest();
            }
            _context.Entry(tenantLease).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenantLeaseExists(IdNumber))
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
        [HttpDelete("{IdNumber}")]
        public async Task<ActionResult<TenantLease>> DeletetTenantLease(string IdNumber)
        {
            var tenantLease = await _context.TenantLease.FindAsync(IdNumber);
            if (tenantLease == null)
            {
                return NotFound();
            }

            _context.TenantLease.Remove(tenantLease);
            await _context.SaveChangesAsync();

            return tenantLease;
        }

        private bool TenantLeaseExists(string IdNumber)
        {
            return _context.TenantLease.Any(e => e.IdNumber == IdNumber);
        }
    }
}
