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
    public class TenantLeaseTerminationController : ControllerBase
    {
        private readonly TenantLeaseTerminationContext _context;
        public TenantLeaseTerminationController(TenantLeaseTerminationContext context)
        {
            _context = context;

        }

        // GET: api/Tenant Lease Termination
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantLeaseTermination>>> GetTenantLeaseTermination()
        {
            //return await _context.TenantProfile.ToListAsync();

            return await _context.TenantLeaseTermination.ToListAsync();
        }

        [HttpGet("{LeaseNumber}")]
        public async Task<ActionResult<TenantLeaseTermination>> GetTenantLeaseTermination(string LeaseNumber)
        {
            var tenantLeaseTermination = await _context.TenantLeaseTermination.FindAsync(LeaseNumber);

            if (tenantLeaseTermination == null)
            {
                return NotFound();
            }

            return tenantLeaseTermination;
        }


        // POST: api/Tenant
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TenantLeaseTermination>> PostTenantLeaseTermination(TenantLeaseTermination tenantLeaseTermination, string LeaseNumber)
        {
            _context.TenantLeaseTermination.Add(tenantLeaseTermination);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TenantLeaseTerminationExists(LeaseNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetTenantLease", new { IdNumber = tenantLease.IdNumber }, tenantLease);
            return CreatedAtAction("GetTenantLease", new { LeaseNumber = tenantLeaseTermination.LeaseNumber }, tenantLeaseTermination);
        }


        // PUT: api/Tenant/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD
        [HttpPut]
        public async Task<IActionResult> PutTenantLeaseTermination(string LeaseNumber, TenantLeaseTermination tenantLeaseTermination)
        {
            if (LeaseNumber != tenantLeaseTermination.LeaseNumber)
            {
                return BadRequest();
            }
            _context.Entry(tenantLeaseTermination).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenantLeaseTerminationExists(LeaseNumber))
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
        // [HttpDelete("{LeaseNumber}")]
        [HttpDelete]
        public async Task<ActionResult<TenantLeaseTermination>> DeletetTenantLeaseTermination(string LeaseNumber)
        {
            var tenantLeaseTermination = await _context.TenantLeaseTermination.FindAsync(LeaseNumber);
            if (tenantLeaseTermination == null)
            {
                return NotFound();
            }

            _context.TenantLeaseTermination.Remove(tenantLeaseTermination);
            await _context.SaveChangesAsync();

            return tenantLeaseTermination;
        }
        private bool TenantLeaseTerminationExists(string LeaseNumber)
        {
            return _context.TenantLeaseTermination.Any(e => e.LeaseNumber == LeaseNumber);
        }
    }
}
