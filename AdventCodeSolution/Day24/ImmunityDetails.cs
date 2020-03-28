using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day24
{
    public class ImmunityDetails
    {
        private ImmunityDetails() :
            this(Enumerable.Empty<AttackType>(), Enumerable.Empty<AttackType>())
        {

        }

        public ImmunityDetails(IEnumerable<AttackType> weakToAttacks, IEnumerable<AttackType> immuneToAttacks)
        {
            WeakToAttacks = weakToAttacks.ToArray();
            ImmuneToAttacks = immuneToAttacks.ToArray();
        }

        public IReadOnlyList<AttackType> WeakToAttacks { get; }

        public IReadOnlyList<AttackType> ImmuneToAttacks { get; }

        public static ImmunityDetails Empty { get; } = new ImmunityDetails();
    }
}
