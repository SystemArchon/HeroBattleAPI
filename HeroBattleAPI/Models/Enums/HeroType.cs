namespace HeroBattleAPI.Models.Enums
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HeroType
    {
        Archer,
        Swordsman,
        Cavalry
    }
}
