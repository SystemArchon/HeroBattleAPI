using HeroBattleAPI.Models;

namespace HeroBattleAPI.Services.Interfaces
{
    public interface IArenaService
    {
        Guid CreateArena(int heroCount);
        BattleHistory StartBattle(Guid arenaId);
    }
}
