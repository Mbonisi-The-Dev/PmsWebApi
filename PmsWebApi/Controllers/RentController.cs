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
    public class RentController : ControllerBase
    {

        private readonly RentContext _context;

        public RentController(RentContext context)
        {
            _context = context;
        }

        // GET: api/Rent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rent>>> GetRent()
        {
            //return await _context.TenantProfile.ToListAsync();

            return await _context.Rent.ToListAsync();
        }

        //GET: api/Tenant/5
        [HttpGet("{LeaseNumber}")]
        public async Task<ActionResult<Rent>> GetRent(string LeaseNumber)
        {
            var rent = await _context.Rent.FindAsync(LeaseNumber);

            if (rent == null)
            {
                return NotFound();
            }

            return rent;
        }

        private bool RentExists(string LeaseNumber)
        {
            return _context.Rent.Any(e => e.LeaseNumber == LeaseNumber);
        }
    }
}
