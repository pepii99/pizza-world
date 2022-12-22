namespace pizza_hub.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pizza_hub.Data.Models.Identity;
using pizza_hub.Data.Models.Pizza;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }

    public DbSet<Pizza> Pizzas { get; set; }

    public DbSet<ApplicationUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<Pizza>()
           .HasOne<ApplicationUser>(s => s.ApplicationUser)
           .WithMany(g => g.Pizzas)
           .HasForeignKey(s => s.ApplicationUserId);

        base.OnModelCreating(builder);
    }
}
