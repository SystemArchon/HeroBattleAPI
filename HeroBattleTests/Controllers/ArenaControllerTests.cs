using HeroBattleAPI.Controllers;
using HeroBattleAPI.Models;
using HeroBattleAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HeroBattleTests.Controllers
{
    public class ArenaControllerTests
    {
        private readonly Mock<IArenaService> _arenaServiceMock;
        private readonly ArenaController _controller;

        public ArenaControllerTests()
        {
            _arenaServiceMock = new Mock<IArenaService>();
            _controller = new ArenaController(_arenaServiceMock.Object);
        }

        [Fact]
        public void RandomHeroGenerator_ReturnsOkWithArenaId()
        {
            int numberOfHeroes = 3;
            Guid arenaId = Guid.NewGuid();
            _arenaServiceMock.Setup(service => service.CreateArena(numberOfHeroes)).Returns(arenaId);

            ActionResult<Guid> result = _controller.CreateArena(numberOfHeroes);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(arenaId, okResult.Value);
        }

        [Fact]
        public void StartBattle_ReturnsOkWithBattleHistory()
        {
            Guid arenaId = Guid.NewGuid();
            var battleHistory = new BattleHistory
            {
                ArenaId = arenaId,
                Rounds = new List<BattleRound>
                {
                    new BattleRound { RoundNumber = 1, Attacker = new Hero(), Defender = new Hero(), Result = "Round 1 result" },
                    new BattleRound { RoundNumber = 2, Attacker = new Hero(), Defender = new Hero(), Result = "Round 2 result" }
                }
            };
            _arenaServiceMock.Setup(service => service.StartBattle(arenaId)).Returns(battleHistory);

            ActionResult<BattleHistory> result = _controller.StartBattle(arenaId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(battleHistory, okResult.Value);
        }
    }
}
