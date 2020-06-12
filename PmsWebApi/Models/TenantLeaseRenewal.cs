using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PmsWebApi.Models
{
    public class TenantLeaseRenewal
    {
        [Key]
        public string RenewNumber { get; set; }
        public string LeaseNumber { get; set; }
        public string AddNotes { get; set; }
    }

    public class TenantLeaseRenewalContext : DbContext
    {
        public TenantLeaseRenewalContext(DbContextOptions<TenantLeaseRenewalContext> options) : base(options)
        {
        }

        public DbSet<TenantLeaseRenewal> TenantLeaseRenewal { get; set; }
    }
}

