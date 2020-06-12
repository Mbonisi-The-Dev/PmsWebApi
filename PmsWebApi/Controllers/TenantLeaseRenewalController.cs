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
    public class TenantLeaseRenewalController : ControllerBase
    {
        private readonly TenantLeaseRenewalContext _context;
        public TenantLeaseRenewalController(TenantLeaseRenewalContext context)
        {
            _context = context;
        }

        // GET: api/TenantLeaseRenewal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantLeaseRenewal>>> GetTenantLeaseRenewal()
        {
            return await _context.TenantLeaseRenewal.ToListAsync();
        }

        // GET: api/Tenant/5
        [HttpGet("{LeaseNumber}")]
        public async Task<ActionResult<TenantLeaseRenewal>> GetTenantLeaseRenewal(string LeaseNumber)
        {
            var tenantLeaseRenewal = await _context.TenantLeaseRenewal.FindAsync(LeaseNumber);

            if (tenantLeaseRenewal == null)
            {
                return NotFound();
            }

            return tenantLeaseRenewal;
        }

        // POST: api/Tenant
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TenantLeaseRenewal>> PostTenantLeaseRenewal(TenantLeaseRenewal tenantLeaseRenewal, string LeaseNumber)
        {
            _context.TenantLeaseRenewal.Add(tenantLeaseRenewal);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TenantLeaseRenewalExists(LeaseNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetTenantLease", new { IdNumber = tenantLease.IdNumber }, tenantLease);
            return CreatedAtAction("GetTenantLease", new { tenantLeaseRenewal.LeaseNumber }, tenantLeaseRenewal);
        }

        // PUT: api/Tenant/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD
        [HttpPut("{LeaseNumber}")]
        public async Task<IActionResult> PutTenantLeaseRenewal(string LeaseNumber, TenantLeaseRenewal tenantLeaseRenewal)
        {
            if (LeaseNumber != tenantLeaseRenewal.LeaseNumber)
            {
                return BadRequest();
            }
            _context.Entry(tenantLeaseRenewal).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenantLeaseRenewalExists(LeaseNumber))
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
        [HttpDelete("{LeaseNumber}")]
        public async Task<ActionResult<TenantLeaseRenewal>> DeletetTenantLeaseRenewal(string LeaseNumber)
        {
            var tenantLeaseRenewal = await _context.TenantLeaseRenewal.FindAsync(LeaseNumber);
            if (tenantLeaseRenewal == null)
            {
                return NotFound();
            }

            _context.TenantLeaseRenewal.Remove(tenantLeaseRenewal);
            await _context.SaveChangesAsync();

            return tenantLeaseRenewal;
        }

        private bool TenantLeaseRenewalExists(string LeaseNumber)
        {
            return _context.TenantLeaseRenewal.Any(e => e.LeaseNumber == LeaseNumber);
        }
    }
}
