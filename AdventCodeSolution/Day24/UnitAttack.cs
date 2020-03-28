namespace AdventCodeSolution.Day24
{
    public class UnitAttack
    {
        public UnitAttack(UnitsGroup attacker, UnitsGroup defender)
        {
            Attacker = attacker;
            Defender = defender;
        }

        public UnitsGroup Attacker { get; }

        public UnitsGroup Defender { get; }

        public void ExecuteAttack()
        {
            Attacker.Attack(Defender);
        }
    }
}
