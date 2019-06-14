using AdventCodeSolution.Day10;
using AdventCodeSolution.Day13.Tracks;
using AdventCodeSolution.Day3;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day13
{
    public class TrackSystem
    {
        private readonly IReadOnlyDictionary<XY, Track> tracks;
        private readonly IReadOnlyList<Cart> carts;

        public TrackSystem(IEnumerable<Track> tracks, IEnumerable<Cart> carts)
        {
            this.tracks = tracks.ToDictionary(t => t.Location, t => t);
            this.carts = carts.ToArray();
        }

        public int GetSystemHeight() => tracks.Keys.MaxBy(xy => xy.Y).Y;

        public Collision FindFirstCollision()
        {
            return EnumerateCartMovement().First(m => m.Collisions.Count > 0).Collisions.First();
        }

        public Cart FindLastCart()
        {
            return EnumerateCartMovement().First(m => m.Carts.Count <= 1).Carts.Single();
        }

        public IEnumerable<CartsMovement> EnumerateCartMovement()
        {
            var orderedCartsByLocation = carts.ToSortedDictionary(c => c.Location, c => c, FromLeftHighestToRightLowestComparer.Value);

            var tick = 1;
            do
            {
                yield return MoveCartsAndUpdateCurrentCarts(orderedCartsByLocation, tick);

                tick++;

            } while (true);
        }

        public CartsMovement MoveCartsAndUpdateCurrentCarts(SortedDictionary<XY, Cart> sortedCartsByLocation, int tick)
        {
            var movedCarts = sortedCartsByLocation.Values.Select(c => (beforeMove: c, afterMove: MoveCart(c))).ToArray();

            var collisions = new List<Collision>();

            foreach (var (cartBeforeMove, cartAfterMove) in movedCarts)
            {
                var alreadyRemoved = !sortedCartsByLocation.Remove(cartBeforeMove.Location);
                if (alreadyRemoved)
                    continue;

                var foundCollision = sortedCartsByLocation.TryGetValue(cartAfterMove.Location, out var collidingCart);
                if (foundCollision)
                {
                    sortedCartsByLocation.Remove(collidingCart.Location);
                    collisions.Add(new Collision(cartAfterMove, collidingCart, tick));
                }
                else
                {
                    sortedCartsByLocation.Add(cartAfterMove.Location, cartAfterMove);
                }
            }

            return new CartsMovement(sortedCartsByLocation.Values, collisions, tick);
        }

        private Cart MoveCart(Cart cart)
        {
            var track = tracks[cart.NextLocation];

            return track.TurnCart(cart.MoveCart());
        }
    }
}
