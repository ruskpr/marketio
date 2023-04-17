using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common.Models;

namespace marketioWebAPI.Data
{
    public class marketioContext : DbContext
    {
        public marketioContext (DbContextOptions<marketioContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Transaction> Transactions { get; set; } = default!;
        public DbSet<Message> Messages { get; set; } = default!;
        public DbSet<UserRating> UserRatings { get; set; } = default!;
        public DbSet<Listing> Listings { get; set; } = default!;
        public DbSet<ListingCategory> ListingCategories { get; set; } = default!;
        public DbSet<ListingImage> ListingImages { get; set; } = default!;
    }
}
