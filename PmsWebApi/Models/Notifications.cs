using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PmsWebApi.Models
{
    public class Notifications
    {
        [Key]
        public string NoteId { get; set; }

        public string NoteDescription { get; set; }


    }

    public class NotificationsContext : DbContext
    {
        public NotificationsContext(DbContextOptions<NotificationsContext> options) : base(options)
        {
        }

        public DbSet<Notifications> Notifications { get; set; }
    }
}

