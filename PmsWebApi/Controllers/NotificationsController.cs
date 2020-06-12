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
    public class NotificationsController : ControllerBase
    {

        private readonly NotificationsContext _context;

        public NotificationsController(NotificationsContext context)
        {
            _context = context;
        }

        // GET: api/Tenant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notifications>>> GetNotifications()
        {
            //return await _context.TenantProfile.ToListAsync();

            return await _context.Notifications.ToListAsync();
        }

        [HttpGet("{NoteId}")]
        public async Task<ActionResult<Notifications>> GetNotifications(string NoteId)
        {
            var notifications = await _context.Notifications.FindAsync(NoteId);

            if (notifications == null)
            {
                return NotFound();
            }

            return notifications;
        }

        [HttpPost]
        public async Task<ActionResult<Notifications>> PostNotifications(Notifications notifications, string NoteId)
        {
            _context.Notifications.Add(notifications);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NotificationsExists(NoteId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNotifications", new { NoteId = notifications.NoteId }, notifications);
        }


        // PUT: api/Tenant/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD
        [HttpPut("{NoteId}")]
        public async Task<IActionResult> PutNotifications(string NoteId, Notifications notifications)
        {
            if (NoteId != notifications.NoteId)
            {
                return BadRequest();
            }

            _context.Entry(notifications).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationsExists(NoteId))
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

        [HttpDelete("{LogId}")]
        public async Task<ActionResult<Notifications>> DeleteNotifications(string NoteId)
        {
            var notifications = await _context.Notifications.FindAsync(NoteId);
            if (notifications == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notifications);
            await _context.SaveChangesAsync();

            return notifications;
        }

        private bool NotificationsExists(string NoteId)
        {
            return _context.Notifications.Any(e => e.NoteId == NoteId);
        }
    }
}

