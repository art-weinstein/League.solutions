using Microsoft.EntityFrameworkCore;

namespace Soccer.Models
{
  public class SoccerContext : DbContext
  {
    public virtual DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerTeam> PlayerTeam { get; set; }

    public SoccerContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}