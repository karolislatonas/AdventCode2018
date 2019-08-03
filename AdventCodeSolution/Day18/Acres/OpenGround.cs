using AdventCodeSolution.Day03;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day18.Acres
{
    public class OpenGround : Acre
    {
        public OpenGround(XY location) : base(location)
        {
        }

        public override char Symbol => '.';

        public override Acre Transform(IEnumerable<Acre> neigbourAcres)
        {
            var trees = neigbourAcres.OfType<Tree>();

            return trees.Count() >= 3 ?
                new Tree(Location) :
                (Acre)this;
        }
    }
}
