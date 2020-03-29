using System;
using System.Linq;

namespace AdventCodeSolution.Day24
{
    public class ImmuneSystemSimulator
    {
        private readonly TargetSelector targetSelect = new TargetSelector();
        private readonly AttackExecuter attackExecuter = new AttackExecuter();

        public ImmuneSystem FindMinumumBoostForImmunitiesToSurvive(Func<ImmuneSystem> createImmuneSystem)
        {
            var damageIncrease = 0;
            ImmuneSystem immuneSystem;
            do
            {
                damageIncrease+=1;
                immuneSystem = createImmuneSystem();
                BoostImmuneSystem(immuneSystem, damageIncrease);
                Simulate(immuneSystem);

            } while (!immuneSystem.ImmuneSystemSurvived());

            return immuneSystem;
        }

        public void Simulate(ImmuneSystem immuneSystem)
        {
            do
            {
                var unitAttacks = targetSelect.SelectTargets(immuneSystem);

                var isSimulationStuck = IsAttacksStuck(unitAttacks);
                
                if (isSimulationStuck)
                    return;

                attackExecuter.ExecuteAttacks(unitAttacks);

            } while (immuneSystem.IsBothGroupsAlive());
        }

        private bool IsAttacksStuck(UnitAttack[] unitAttacks)
        {
            return unitAttacks
                .All(a => a.DamageToBeDone() < a.Defender.UnitHitPoints);
        }

        private void BoostImmuneSystem(ImmuneSystem immuneSystem, int damageIncrease)
        {
            foreach (var immunityGroup in immuneSystem.AliveImmunities)
                immunityGroup.ChangeUnitDamageBy(damageIncrease);
        }
    }
}
