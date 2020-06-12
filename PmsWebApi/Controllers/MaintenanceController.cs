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
    public class MaintenanceController : ControllerBase
    {

        private readonly MaintenanceContext _context;

        public MaintenanceController(MaintenanceContext context)
        {
            _context = context;
        }

        // GET: api/Tenant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maintenance>>> GetMaintenance()
        {
            //return await _context.TenantProfile.ToListAsync();

            return await _context.Maintenance.ToListAsync();
        }

        // GET: api/Tenant/5
        [HttpGet("{LogId}")]
        public async Task<ActionResult<Maintenance>> GetMaintenance(string LogId)
        {
            var maintenance = await _context.Maintenance.FindAsync(LogId);

            if (maintenance == null)
            {
                return NotFound();
            }

            return maintenance;
        }

        [HttpPost]
        public async Task<ActionResult<Maintenance>> PostMaintenance(Maintenance maintenance, string LogId)
        {
            _context.Maintenance.Add(maintenance);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MaintenanceExists(LogId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMaintenance", new { IdNumber = maintenance.IdNumber }, maintenance);
        }

        // PUT: api/Tenant/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD
        [HttpPut("{LogId}")]
        public async Task<IActionResult> PutTenantProfile(string LogId, Maintenance maintenance)
        {
            if (LogId != maintenance.LogId)
            {
                return BadRequest();
            }

            _context.Entry(maintenance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaintenanceExists(LogId))
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
        [HttpDelete("{LogId}")]
        public async Task<ActionResult<Maintenance>> DeleteMaintenance(string LogId)
        {
            var maintenance = await _context.Maintenance.FindAsync(LogId);
            if (maintenance == null)
            {
                return NotFound();
            }

            _context.Maintenance.Remove(maintenance);
            await _context.SaveChangesAsync();

            return maintenance;
        }


        private bool MaintenanceExists(string LogId)
        {
            return _context.Maintenance.Any(e => e.LogId == LogId);
        }
    }

}
