namespace AdventCodeSolution.Day15.Players.Creation
{
    public class PlayerConfiguration
    {
        private PlayerConfiguration(int attackPower)
        {
            AttackPower = attackPower;
        }

        public int AttackPower { get; }

        public static PlayerConfiguration DefaultConfig { get; } = new PlayerConfiguration(3);

        public static PlayerConfiguration CreateConfig(int attackPower) => new PlayerConfiguration(attackPower);
    }
}
