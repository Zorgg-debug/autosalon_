using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace autosalon_.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Salon> salon { get; set; }
        public DbSet<Car> cars { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
