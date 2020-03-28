using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day24
{
    public class Damage
    {
        private readonly AttackType attackType;

        public Damage(int totalDamage, AttackType type)
        {
            TotalDamage = totalDamage;
            this.attackType = type;
        }

        public int TotalDamage { get; }

        public Damage ApplyWeaknesses(IEnumerable<AttackType> weaknesses)
        {
            return weaknesses.Contains(attackType) ?
                Double() :
                this;
        }

        public Damage ApplyImmunities(IEnumerable<AttackType> immunities)
        {
            return immunities.Contains(attackType) ?
               NoDamage :
               this;
        }

        private Damage Double() => new Damage(TotalDamage * 2, attackType);

        public static Damage NoDamage { get; } = new Damage(0, AttackType.None);
    }
}
