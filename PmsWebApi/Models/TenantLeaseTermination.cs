using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PmsWebApi.Models
{
    public class TenantLeaseTermination
    {
        [Key]
        public string TermNumber { get; set; }
        public string LeaseNumber { get; set; }
        public string Reason { get; set; }

    }

    public class TenantLeaseTerminationContext : DbContext
    {
        public TenantLeaseTerminationContext(DbContextOptions<TenantLeaseTerminationContext> options) : base(options)
        {
        }

        public DbSet<TenantLeaseTermination> TenantLeaseTermination { get; set; }
    }


}

