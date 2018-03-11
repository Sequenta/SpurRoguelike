using System.Linq;
using SpurRoguelike.Core;
using SpurRoguelike.Core.Primitives;
using SpurRoguelike.Core.Views;

namespace SpurRoguelike.PlayerBot
{
    public class PlayerBot : IPlayerController
    {
        public Turn MakeTurn(LevelView levelView, IMessageReporter messageReporter)
        {
            messageReporter.ReportMessage("Hey ho! I'm still breathing");

           

            return Turn.Step((StepDirection)levelView.Random.Next(4));
        }

        private Turn Attack(LevelView levelView, PawnView monster)
        {
            if (monster.HasValue)
                return Turn.Attack(monster.Location - levelView.Player.Location);

            var nearbyMonster = GetNearbyMonster(levelView, monster.Location);
            if (nearbyMonster.HasValue)
            {
                return Turn.Attack(nearbyMonster.Location - levelView.Player.Location);
            }

            return null;
        }

        private PawnView GetNearbyMonster(LevelView levelView)
        {
            return GetNearbyMonster(levelView, levelView.Player.Location);
        }

        private PawnView GetNearbyMonster(LevelView levelView, Location location)
        {
            return levelView.Monsters.FirstOrDefault(m => IsInAttackRange(location, m.Location));
        }

        private static bool IsInAttackRange(Location a, Location b)
        {
            return a.IsInRange(b, 1);
        }
    }
}
