using AdventCodeSolution.Day3;
using System;
using System.Collections.Generic;

namespace AdventCodeSolution.Day15.Players
{
    public abstract class Player
    {
        private const int InitialHitPoints = 200;

        public event Action<Player> PlayerDied;
        public event Action<XY, Player> PlayerMoved;

        protected Player(XY location)
        {
            Location = location;
            HitPoints = InitialHitPoints;
        }

        public bool IsDead => HitPoints <= 0;

        public int HitPoints { get; private set; }

        public XY Location { get; private set; }

        public abstract int AttackPower { get; }

        public abstract char Symbol { get; }

        public void Attack(Player target)
        {
            VerifyIsNotDead();

            target.TakeDamageFrom(this);
        }
        
        public void MoveTo(XY newLocation)
        {
            VerifyIsNotDead();

            var previousLocation = Location;

            Location = newLocation;

            PlayerMoved?.Invoke(previousLocation, this);
        }

        public bool IsEnemy(Player otherPlayer) => GetType() != otherPlayer.GetType();

        private void TakeDamageFrom(Player player)
        {
            VerifyIsNotDead();

            HitPoints -= player.AttackPower;

            if (IsDead)
            {
                PlayerDied?.Invoke(this);
            }
        }

        private void VerifyIsNotDead()
        {
            if (IsDead) throw new Exception("Player is already dead");
        }
    }
}
