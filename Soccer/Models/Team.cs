using System.Collections.Generic;

namespace Soccer.Models
{
  public class Team
  {
    public Team()
    {
        this.JoinEntities = new HashSet<TeamTournament>();
        
    }

    public int TeamId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<TeamTournament> JoinEntities { get; }
  }
}