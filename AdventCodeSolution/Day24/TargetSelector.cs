using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day24
{
    public class TargetSelector
    {
        private readonly ImmuneSystem immuneSystem;

        public TargetSelector(ImmuneSystem immuneSystem)
        {
            this.immuneSystem = immuneSystem;
        }

        public UnitAttack[] SelectTargets()
        {
            var immunitiesAttacks = SelectTargets(immuneSystem.AliveImmunities, immuneSystem.AliveInfections);
            var infectionsAttacks = SelectTargets(immuneSystem.AliveInfections, immuneSystem.AliveImmunities);

            return immunitiesAttacks
                .Concat(infectionsAttacks)
                .ToArray();
        }

        public IEnumerable<UnitAttack> SelectTargets(IEnumerable<UnitsGroup> attackers, IEnumerable<UnitsGroup> possibleTargets)
        {
            var orderedAttackers = attackers.OrderByDescending(a => a.EffectivePower);

            var selectedTargets = new HashSet<UnitsGroup>();
            var remainingTargets = new HashSet<UnitsGroup>(possibleTargets);

            foreach (var attacker in orderedAttackers)
            {
                var target = SelectTargetForUnit(attacker, remainingTargets);

                if (target != null)
                {
                    selectedTargets.Add(target);
                    remainingTargets.Remove(target);
                    yield return new UnitAttack(attacker, target);
                }
            }
        }

        private UnitsGroup SelectTargetForUnit(UnitsGroup attacker, IEnumerable<UnitsGroup> possibleTargets)
        {
            return possibleTargets
                    .MultipleMaxBy(attacker.CalculateAttackDamageOnTarget)
                    .MultipleMaxBy(t => t.EffectivePower)
                    .MultipleMaxBy(t => t.Initiative)
                    .SingleOrDefault();
        }
    }
}
