using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineBakery.Models
{
  public class OnlineBakeryContext : IdentityDbContext<ApplicationUser>
  {
    // public DbSet<Name> Names { get; set; }
    public OnlineBakeryContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}