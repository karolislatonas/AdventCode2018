using AdventCodeSolution.Day03;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day18.Acres
{
    public class Tree : Acre
    {
        public Tree(XY location) : base(location)
        {
        }

        public override char Symbol => '|';

        public override Acre Transform(IEnumerable<Acre> neigbourAcres)
        {
            var lumberyards = neigbourAcres.OfType<Lumberyard>();

            return lumberyards.Count() >= 3 ?
                new Lumberyard(Location) :
                (Acre)this;
        }
    }
}
