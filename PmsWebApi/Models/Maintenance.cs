using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PmsWebApi.Models
{
    public class Maintenance
    {
        [Key]
        public string LogId { get; set; }
        public string IdNumber { get; set; }
        public string LogDate { get; set; }
        public string CallStatus { get; set; }
        public string CallDescription { get; set; }
    }

    public class MaintenanceContext : DbContext
    {
        public MaintenanceContext(DbContextOptions<MaintenanceContext> options) : base(options)
        {
        }

        public DbSet<Maintenance> Maintenance { get; set; }
    }
}
