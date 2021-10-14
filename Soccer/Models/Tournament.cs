using System.Collections.Generic;

namespace Soccer.Models
{
  public class Tournament
  {
    public Tournament()
    {
      this.JoinEntities = new HashSet<TeamTournament>();
    }

    public int TournamentId { get; set; }
    public string TournamentName { get; set; }
    public virtual ICollection<TeamTournament> JoinEntities { get; set; }
  }
}