using STIVE.API.Models;
using Microsoft.EntityFrameworkCore;

namespace STIVE.API.Database
{
    public class stive_potion_seller_context : DbContext
    {
        public DbSet<User> user { get; set;}
        public DbSet<Role> role { get; set; }
        public DbSet<UserHasRole> userHasRole { get; set; }
        public DbSet<Bonus> bonus { get; set; }
        public DbSet<Customer> customer { get; set; }
        public DbSet<Potion> potion { get; set; }
        public DbSet<Save> save { get; set; }
        public DbSet<SaveHasBonus> saveHasBonus { get; set; }
        public DbSet<SaveHasPotion> saveHasPotion { get; set; }
        public DbSet<Supplier> supplier { get; set; }
        public DbSet<SaveHasSupplier> saveHasSupplier { get; set; }

        public stive_potion_seller_context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
