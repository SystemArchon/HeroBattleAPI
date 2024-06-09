using HeroBattleAPI.Models.Enums;
using HeroBattleAPI.Models;
using System.Text.Json.Serialization;

namespace HeroBattleAPI
{
    [JsonSerializable(typeof(Hero))]
    [JsonSerializable(typeof(BattleRound))]
    [JsonSerializable(typeof(BattleHistory))]
    [JsonSerializable(typeof(HeroType))]
    public partial class JsonContext : JsonSerializerContext
    {
    }
}
