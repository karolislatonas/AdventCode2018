using AdventCodeSolution.Day03;

namespace AdventCodeSolution.Day10
{
    public class Light
    {
        public Light(XY initialPositon, XY velocity)
        {
            InitialPositon = initialPositon;
            Velocity = velocity;
        }

        public XY InitialPositon { get; }

        public XY Velocity { get; }

        public XY GetPositionAfterSeconds(int seconds) => InitialPositon + Velocity * seconds;
    }
}
