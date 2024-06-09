namespace HeroBattleAPI.Models
{
    public class BattleHistory
    {
        public Guid ArenaId { get; set; }
        public List<BattleRound> Rounds { get; set; } = new List<BattleRound>();
    }
}
