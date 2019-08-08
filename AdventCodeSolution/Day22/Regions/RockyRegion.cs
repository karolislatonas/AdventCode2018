namespace AdventCodeSolution.Day22.Regions
{
    public class RockyRegion : CaveRegion
    {
        public RockyRegion(int erosionLevel) : base(erosionLevel)
        {
        }

        public override int RiskLevel => 0;
    }
}
