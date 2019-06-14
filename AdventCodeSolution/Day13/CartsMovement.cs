using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day13
{
    public class CartsMovement
    {
        public CartsMovement(IEnumerable<Cart> carts, IEnumerable<Collision> collisions, int happenedOnTicktick)
        {
            Carts = carts.ToArray();
            Collisions = collisions.ToArray();
            HappenedOnTick = happenedOnTicktick;
        }

        public IReadOnlyList<Cart> Carts { get; }

        public IReadOnlyList<Collision> Collisions { get; }

        public int HappenedOnTick { get; }
    }
}
