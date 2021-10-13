using System.Collections.Generic;

namespace Soccer.Models
{
  public class Player
  {
    public Player()
    {
        this.JoinEntities = new HashSet<PlayerTeam>();
        
    }

    public int PlayerId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<PlayerTeam> JoinEntities { get; }
  }
}