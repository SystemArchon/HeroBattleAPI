using HeroBattleAPI.Models;
using HeroBattleAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HeroBattleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArenaController : ControllerBase
    {
        private readonly IArenaService _arenaService;

        public ArenaController(IArenaService arenaService)
        {
            _arenaService = arenaService;
        }

        [HttpPost("create")]
        public ActionResult<Guid> CreateArena([FromQuery] int heroCount)
        {
            Guid arenaId = _arenaService.CreateArena(heroCount);
            return new OkObjectResult(arenaId);
        }

        [HttpPost("battle")]
        public ActionResult<BattleHistory> StartBattle([FromQuery] Guid arenaId)
        {
            try
            {
                BattleHistory history = _arenaService.StartBattle(arenaId);
                return new OkObjectResult(history);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Arena not found");
            }
            catch (ApplicationException e)
            {
                return NotFound("The arena has already been played");
            }
        }
    }
}
