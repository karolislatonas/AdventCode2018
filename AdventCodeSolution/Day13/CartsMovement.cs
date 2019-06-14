using AdventCodeSolution.Day10;
using AdventCodeSolution.Day3;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day13
{
    public class CartsMovement
    {
        private readonly SortedDictionary<XY, Cart> sortedCartsByMovement;

        public CartsMovement(IEnumerable<Cart> carts, IEnumerable<Collision> collisions, int tick)
        {
            sortedCartsByMovement = carts.ToSortedDictionary(c => c.Location, c => c, FromLeftHighestToRightLowestComparer.Value);
            Collisions = collisions.ToArray();
            Tick = tick;
        }

        public IReadOnlyCollection<Cart> Carts => sortedCartsByMovement.Values;

        public IReadOnlyList<Collision> Collisions { get; }

        public int Tick { get; }

        
    }
}
