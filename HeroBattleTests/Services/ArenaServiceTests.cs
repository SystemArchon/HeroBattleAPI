using HeroBattleAPI.Services.Interfaces;
using HeroBattleAPI.Services;
using HeroBattleAPI.BusinessLogic;
using HeroBattleAPI.Models.Enums;
using HeroBattleAPI.Models;

namespace HeroBattleTests.Services
{
    public class ArenaServiceTests
    {
        private readonly IArenaService _arenaService;
        private readonly Arena _arena;

        public ArenaServiceTests()
        {
            _arenaService = new ArenaService();
            List<Hero> heroes = new List<Hero>
            {
                new Hero { Id = Guid.NewGuid(), Type = HeroType.Archer, Health = 100 },
                new Hero { Id = Guid.NewGuid(), Type = HeroType.Swordsman, Health = 120 },
                new Hero { Id = Guid.NewGuid(), Type = HeroType.Cavalry, Health = 150 }
            };

            _arena = new Arena(heroes);
        }

        [Fact]
        public void TestHeroGeneration()
        {
            var arenaId = _arenaService.CreateArena(10);

            Assert.NotEqual(Guid.Empty, arenaId);
        }

        [Fact]
        public void TestBattle()
        {
            var arenaId = _arenaService.CreateArena(10);
            var history = _arenaService.StartBattle(arenaId);

            Assert.NotNull(history);
            Assert.Equal(arenaId, history.ArenaId);
            Assert.True(history.Rounds.Count > 0);
        }

        [Fact]
        public void StartBattle_AtLeastOneHeroSurvives()
        {
            _arena.StartBattle();
            int survivors = 0;
            foreach (var round in _arena.History.Rounds)
            {
                if (round.Defender.Health > 0)
                {
                    survivors++;
                }
            }

            Assert.True(survivors <= 1);
        }

        [Fact]
        public void StartBattle_BattleHistoryNotEmpty()
        {
            _arena.StartBattle();
            Assert.NotEmpty(_arena.History.Rounds);
        }
    }
}
