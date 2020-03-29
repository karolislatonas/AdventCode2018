namespace AdventCodeSolution.Day24
{
    public class UnitAttackDetails
    {
        public UnitAttackDetails(int unitDamage, AttackType attackType)
        {
            UnitDamage = unitDamage;
            AttackType = attackType;
        }

        public int UnitDamage { get; }

        public AttackType AttackType { get; }

        public UnitAttackDetails ChangeDamageBy(int damageChange)
        {
            return new UnitAttackDetails(UnitDamage + damageChange, AttackType);
        }
    }
}
