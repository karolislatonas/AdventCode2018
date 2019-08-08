namespace AdventCodeSolution.Day22.Regions
{
    public class WetRegion : CaveRegion
    {
        public WetRegion(int erosionLevel) : base(erosionLevel)
        {
        }

        public override int RiskLevel => 1;
    }
}
