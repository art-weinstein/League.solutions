namespace Soccer.Models
{
  public class TeamTournament
  {
    public int TeamTournamentId { get; set; }
    public int TeamId { get; set; }
    public int TournamentId { get; set; }
    public virtual Team Team { get; set; }
    public virtual Tournament Tournament { get; set; }
  }
}