namespace AdventCodeSolution.Day22.Regions
{
    public class NarrowRegion : CaveRegion
    {
        public NarrowRegion(int erosionLevel) : base(erosionLevel)
        {
        }

        public override int RiskLevel => 2;
    }
}
