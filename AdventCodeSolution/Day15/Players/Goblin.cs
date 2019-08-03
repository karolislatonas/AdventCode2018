using AdventCodeSolution.Day03;

namespace AdventCodeSolution.Day15.Players
{
    public class Goblin : Player
    {
        public Goblin(XY location) : base(location)
        {
        }

        public override char Symbol => 'G';

        public override int AttackPower => 3;
    }
}
