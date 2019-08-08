namespace AdventCodeSolution.Day22.Regions
{
    public abstract class CaveRegion
    {
        protected CaveRegion(int erosionLevel)
        {
            ErosionLevel = erosionLevel;
        }

        public int ErosionLevel { get; }

        public abstract int RiskLevel { get; }
    }
}
