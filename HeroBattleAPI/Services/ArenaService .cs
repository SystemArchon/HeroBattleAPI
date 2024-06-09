using HeroBattleAPI.Models.Enums;
using HeroBattleAPI.Models;
using HeroBattleAPI.Services.Interfaces;
using HeroBattleAPI.BusinessLogic;

namespace HeroBattleAPI.Services
{
    public class ArenaService : IArenaService
    {

        private readonly Dictionary<Guid, Arena> _arenas = new Dictionary<Guid, Arena>();

        public Guid CreateArena(int heroCount)
        {
            List<Hero> heroes = GenerateHeroes(heroCount);
            Arena arena = new Arena(heroes);
            _arenas[arena.Id] = arena;

            return arena.Id;
        }

        public BattleHistory StartBattle(Guid arenaId)
        {
            if (!_arenas.ContainsKey(arenaId))
                throw new KeyNotFoundException("Arena not found");

            Arena arena = _arenas[arenaId];
            if (arena.ArenaCompleted)
                throw new ApplicationException("The arena has already been played");

            arena.StartBattle();

            return arena.History;
        }

        private List<Hero> GenerateHeroes(int heroCount)
        {
            List<Hero> heroes = new List<Hero>();
            Random random = new Random();

            for (int i = 0; i < heroCount; i++)
            {
                HeroType heroType = (HeroType)random.Next(0, 3);
                heroes.Add(new Hero { Id = Guid.NewGuid(), Type = heroType, Health = GetInitialHealth(heroType) });
            }

            return heroes;
        }

        private int GetInitialHealth(HeroType heroType) => heroType switch
        {
            HeroType.Archer => 100,
            HeroType.Swordsman => 120,
            HeroType.Cavalry => 150,
            _ => 0
        };

    }
}
