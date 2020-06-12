using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PmsWebApi.Models
{
      public class Rent
    {
        [Key]
        public string InvoiceNumber { get; set; }
        public string LeaseNumber { get; set; }
        public int AmountDue { get; set; }
        public string DueDate { get; set; }
        public string PaymentId { get; set; }


    }

    public class RentContext : DbContext
    {
        public RentContext(DbContextOptions<RentContext> options) : base(options)
        {
        }

        public DbSet<Rent> Rent { get; set; }
    }
}

