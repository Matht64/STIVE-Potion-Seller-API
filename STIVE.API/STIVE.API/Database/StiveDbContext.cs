using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using STIVE.API.Models;

namespace STIVE.API.Database;

public class StiveDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Bonus> Bonus { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Potion> Potion { get; set; }
    public DbSet<GameData> GameData { get; set; }
    public DbSet<Supplier> Supplier { get; set; }
    public DbSet<GameDataBonus> GameDataBonus { get; set; }
    public DbSet<GameDataPotion> GameDataPotion { get; set; }
    public DbSet<GameDataSupplier> GameDataSupplier { get; set; }
    public DbSet<Cart> Cart { get; set; }
    public DbSet<CartItem> CartItem { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    
    public StiveDbContext(DbContextOptions<StiveDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<GameData>()
            .HasOne(gd => gd.User)
            .WithMany()
            .HasForeignKey(gd => gd.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurer la relation GameData -> Bonus via GameDataBonus
        builder.Entity<GameDataBonus>()
            .HasKey(gdb => new { gdb.GameDataId, gdb.BonusId });

        builder.Entity<GameDataBonus>()
            .HasOne(gdb => gdb.GameData)
            .WithMany(gd => gd.GameDataBonuses)
            .HasForeignKey(gdb => gdb.GameDataId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<GameDataBonus>()
            .HasOne(gdb => gdb.Bonus)
            .WithMany(b => b.GameDatasBonus)
            .HasForeignKey(gdb => gdb.BonusId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurer la relation GameData -> Potion via GameDataPotion
        builder.Entity<GameDataPotion>()
            .HasKey(gdp => new { gdp.GameDataId, gdp.PotionId });

        builder.Entity<GameDataPotion>()
            .HasOne(gdp => gdp.GameData)
            .WithMany(gd => gd.GameDataPotions)
            .HasForeignKey(gdp => gdp.GameDataId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<GameDataPotion>()
            .HasOne(gdp => gdp.Potion)
            .WithMany(p => p.GameDatasPotion)
            .HasForeignKey(gdp => gdp.PotionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurer la relation GameData -> Supplier via GameDataSupplier
        builder.Entity<GameDataSupplier>()
            .HasKey(gds => new { gds.GameDataId, gds.SupplierId });

        builder.Entity<GameDataSupplier>()
            .HasOne(gds => gds.GameData)
            .WithMany(gd => gd.GameDataSuppliers)
            .HasForeignKey(gds => gds.GameDataId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<GameDataSupplier>()
            .HasOne(gds => gds.Supplier)
            .WithMany(s => s.GameDatasSupplier)
            .HasForeignKey(gds => gds.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}