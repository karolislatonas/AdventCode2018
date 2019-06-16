using AdventCodeSolution.Day3;

namespace AdventCodeSolution.Day15.Players
{
    public class Elf : Player
    {
        public Elf(XY location) : this(location, 3)
        {
        }

        public Elf(XY location, int attackPower) : base(location)
        {
            AttackPower = attackPower;
        }

        public override char Symbol => 'E';

        public override int AttackPower { get; }
    }
}
