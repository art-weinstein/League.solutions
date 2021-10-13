namespace Soccer.Models
{
  public class PlayerTeam
  {
    public int PlayerTeamId { get; set; }
    public int PlayerId { get; set; }
    public int TeamId { get; set; }
    public virtual Player Player { get; set; }
    public virtual Team Team { get; set; }
  }
}