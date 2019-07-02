using AdventCodeSolution.Day3;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day18.Acres
{
    public class Lumberyard : Acre
    {
        public Lumberyard(XY location) : base(location)
        {
        }

        public override char Symbol => '#';

        public override Acre Transform(IEnumerable<Acre> neigbourAcres)
        {
            var trees = neigbourAcres.OfType<Tree>();
            var lumberyards = neigbourAcres.OfType<Lumberyard>();

            return trees.Any() && lumberyards.Any() ?
                (Acre)this :
                new OpenGround(Location);
        }
    }
}
