using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace PmsWebApi.Models
{
    public class TenantLease
    {
        [Key]
        public string IdNumber { get; set; }

        public string LeaseNumber { get; set; }

        public string UnitNumber { get; set; }


        public int Rental { get; set; }

        public string VehicleReg { get; set; }

        public string VehicleReg2 { get; set; }

        public string StatusApproval { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

    }

    public class TenantLeaseContext : DbContext
    {
        public TenantLeaseContext(DbContextOptions<TenantLeaseContext> options) : base(options)
        {
        }

        public DbSet<TenantLease> TenantLease { get; set; }
    }
}

