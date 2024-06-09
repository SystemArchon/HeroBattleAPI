namespace HeroBattleAPI.Models
{
    public class BattleRound
    {
        public int RoundNumber { get; set; }
        public Hero Attacker { get; set; }
        public Hero Defender { get; set; }
        public string Result { get; set; }
    }
}
