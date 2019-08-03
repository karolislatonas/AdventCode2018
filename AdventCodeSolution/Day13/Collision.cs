using AdventCodeSolution.Day03;
using System;

namespace AdventCodeSolution.Day13
{
    public class Collision
    {
        public Collision(Cart firstCart, Cart secondCart, int tick)
        {
            if (firstCart.Location != secondCart.Location)
                throw new ArgumentException("Both carts location should match");

            FirstCart = firstCart;
            SecondCart = secondCart;
            Location = firstCart.Location;
            Tick = tick;
        }

        public Cart FirstCart { get; }

        public Cart SecondCart { get; }

        public XY Location { get; }

        public int Tick { get; }
    }
}
