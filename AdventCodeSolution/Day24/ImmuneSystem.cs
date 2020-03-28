using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day24
{
    public class ImmuneSystem
    {
        private readonly IReadOnlyCollection<UnitsGroup> immunities;
        private readonly IReadOnlyCollection<UnitsGroup> infections;

        public ImmuneSystem(IEnumerable<UnitsGroup> immunities, IEnumerable<UnitsGroup> infections)
        {
            this.immunities = immunities.ToArray();
            this.infections = infections.ToArray();
        }

        public IEnumerable<UnitsGroup> AliveImmunities => FilterAlive(immunities);

        public IEnumerable<UnitsGroup> AliveInfections => FilterAlive(infections);

        public bool BothGroupsRemaining()
        {
            return AliveImmunities.Count() > 0 &&
                   AliveInfections.Count() > 0;
        }

        public int GetTotalUnitsRemaining()
        {
            return immunities
                .Concat(infections)
                .Select(g => g.UnitsCount)
                .Sum();
        }

        private IEnumerable<UnitsGroup> FilterAlive(IEnumerable<UnitsGroup> unitsGroups)
        {
            return unitsGroups.Where(i => i.IsAlive);
        }

        
    }
}
