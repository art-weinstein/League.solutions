using Microsoft.EntityFrameworkCore;

namespace Soccer.Models
{
  public class SoccerContext : DbContext
  {
    public virtual DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamTournament> TeamTournament { get; set; }

    public SoccerContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}