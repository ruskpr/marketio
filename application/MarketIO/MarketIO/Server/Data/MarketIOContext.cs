using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MarketIO.Shared.Models;

namespace MarketIO.Server.Data
{
    public class MarketIOContext : DbContext
    {
        public MarketIOContext (DbContextOptions<MarketIOContext> options)
            : base(options)
        {
        }

        public DbSet<MarketIO.Shared.Models.User> Users { get; set; } = default!;

        public DbSet<MarketIO.Shared.Models.Transaction> Transactions { get; set; }
    }
}
