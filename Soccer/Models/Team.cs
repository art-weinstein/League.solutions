using System.Collections.Generic;

namespace Soccer.Models
{
  public class Team
  {
    public Team()
    {
      this.JoinEntities = new HashSet<PlayerTeam>();
    }

    public int TeamId { get; set; }
    public string TeamName { get; set; }
    public virtual ICollection<PlayerTeam> JoinEntities { get; set; }
  }
}