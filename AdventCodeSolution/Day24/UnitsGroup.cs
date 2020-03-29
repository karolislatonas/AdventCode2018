using System;
using static System.Math;

namespace AdventCodeSolution.Day24
{
    public class UnitsGroup
    {
        private UnitAttackDetails unitAttackDetails;
        private readonly ImmunityDetails immunityDetails;

        public UnitsGroup(int unitsCount, int unitHitPoints, UnitAttackDetails unitAttackDetails, ImmunityDetails immunityDetails, int initiative)
        {
            UnitsCount = unitsCount;
            UnitHitPoints = unitHitPoints;
            this.unitAttackDetails = unitAttackDetails;
            this.immunityDetails = immunityDetails;
            Initiative = initiative;
        }

        public int Initiative { get; }
        
        public int UnitsCount { get; private set; }

        public int UnitHitPoints { get; }
        
        public bool IsAlive => UnitsCount > 0;

        public int EffectivePower => unitAttackDetails.UnitDamage * UnitsCount;

        public void ChangeUnitDamageBy(int unitDamageChange)
        {
            unitAttackDetails = unitAttackDetails.ChangeDamageBy(unitDamageChange);
        }

        public int CalculateAttackDamageOnTarget(UnitsGroup target)
        {
            return target.CalculateDamageToBeDone(GetAttackDamage()).TotalDamage;
        }

        public void Attack(UnitsGroup groupToAttack)
        {
            var attackDamage = GetAttackDamage();
            groupToAttack.TakeDamage(attackDamage);
        }

        private void TakeDamage(Damage damage)
        {
            var damageToBeDone = CalculateDamageToBeDone(damage);

            var unitsKilled = damageToBeDone.TotalDamage / UnitHitPoints;

            UnitsCount -= Min(UnitsCount, unitsKilled);
        }

        private Damage CalculateDamageToBeDone(Damage attackDamage)
        {
            return attackDamage
                .ApplyImmunities(immunityDetails.ImmuneToAttacks)
                .ApplyWeaknesses(immunityDetails.WeakToAttacks);
        }

        private Damage GetAttackDamage() => new Damage(EffectivePower, unitAttackDetails.AttackType);
    }
}
