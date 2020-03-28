using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day24
{
    public class AttackExecuter
    {
        public void ExecuteAttacks(IEnumerable<UnitAttack> unitAttacks)
        {
            var orderedAttacks = unitAttacks.OrderByDescending(a => a.Attacker.Initiative);

            foreach (var attack in orderedAttacks)
            {
                if(attack.Attacker.IsAlive)
                    attack.ExecuteAttack();
            } 
        }
    }
}
