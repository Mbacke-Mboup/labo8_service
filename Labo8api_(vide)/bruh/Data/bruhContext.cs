using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using bruh.Models;

namespace bruh.Data
{
    public class bruhContext : DbContext
    {
        public bruhContext (DbContextOptions<bruhContext> options)
            : base(options)
        {
        }

        public DbSet<bruh.Models.Animal> Animal { get; set; } = default!;
    }
}
