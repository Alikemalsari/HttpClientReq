using MicroService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MicroService.Context
{
    public class CustomerDbContext : DbContext
    {

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
