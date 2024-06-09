using HeroBattleAPI.Models.Enums;
using HeroBattleAPI.Models;
using System;

namespace HeroBattleAPI.BusinessLogic
{
    public class Arena
    {
        private readonly List<Hero> _heroes;
        public Guid Id { get; } = Guid.NewGuid();
        public BattleHistory History { get; } = new BattleHistory();
        public Boolean ArenaCompleted { get; set; } = false;

        public Arena(List<Hero> heroes)
        {
            _heroes = heroes;
            History.ArenaId = Id;
        }

        public void StartBattle()
        {
            int roundNumber = 1;

            while (_heroes.Count(h => h.Health > 0) > 1)
            {
                Hero attacker = _heroes.Where(h => h.Health > 0).OrderBy(_ => Guid.NewGuid()).First();
                Hero defender = _heroes.Where(h => h.Health > 0 && h.Id != attacker.Id).OrderBy(_ => Guid.NewGuid()).First();

                string result = ExecuteAttack(attacker, defender);

                History.Rounds.Add(new BattleRound
                {
                    RoundNumber = roundNumber++,
                    Attacker = new Hero() { Id = attacker.Id, Type = attacker.Type, Health = attacker.Health },
                    Defender = new Hero() { Id = defender.Id, Type = defender.Type, Health = defender.Health },
                    Result = result
                });

                foreach (var hero in _heroes.Where(h => h.Health > 0 && h.Id != attacker.Id && h.Id != defender.Id))
                {
                    hero.Health = Math.Min(GetInitialHealth(hero.Type), hero.Health + 10);
                }
            }

            ArenaCompleted = true;
        }

        private string ExecuteAttack(Hero attacker, Hero defender)
        {
            bool defenderDies = false;
            switch (attacker.Type)
            {
                case HeroType.Archer:
                    if (defender.Type == HeroType.Cavalry)
                        defenderDies = new Random().NextDouble() < 0.4;
                    else if (defender.Type == HeroType.Swordsman || defender.Type == HeroType.Archer)
                        defenderDies = true;
                    break;
                case HeroType.Swordsman:
                    if (defender.Type == HeroType.Swordsman || defender.Type == HeroType.Archer)
                        defenderDies = true;
                    break;
                case HeroType.Cavalry:
                    if (defender.Type == HeroType.Cavalry || defender.Type == HeroType.Swordsman || defender.Type == HeroType.Archer)
                        defenderDies = true;
                    break;
            }

            if (defenderDies)
            {
                defender.Health = 0;
                attacker.Health /= 2;
                HandleHeroesHealth(attacker, defender);
                return GetAttackOutcome(attacker, defender);
            }
            else
            {
                attacker.Health /= 2;
                defender.Health /= 2;
                HandleHeroesHealth(attacker, defender);
                return GetAttackOutcome(attacker, defender);
            }
        }

        private void HandleHeroesHealth(Hero attacker, Hero defender)
        {
            if (attacker.Health < GetInitialHealth(attacker.Type) / 4)
                attacker.Health = 0;

            if (defender.Health < GetInitialHealth(defender.Type) / 4)
                defender.Health = 0;
        }

        private string GetAttackOutcome(Hero attacker, Hero defender)
        {
            string action = $"{attacker.Type} attacked {defender.Type}";
            string outcome;

            if (attacker.Health == 0 && defender.Health == 0)
            {
                outcome = "both of them died because their health reached lower than the quarter of their original health";
            }
            else if (attacker.Health == 0)
            {
                outcome = "the attacker died because his health reached lower than the quarter of his original health";
            }
            else if (defender.Health == 0)
            {
                outcome = "the defender died because his health reached lower than the quarter of his original health";
            }
            else
            {
                outcome = "both survived with reduced health";
            }

            return $"{action} and {outcome}.";
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
