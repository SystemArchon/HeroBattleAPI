using HeroBattleAPI.Models.Enums;

namespace HeroBattleAPI.Models
{
    public class Hero
    {
        public Guid Id { get; set; }
        public HeroType Type { get; set; }
        public int Health { get; set; }
    }
}
