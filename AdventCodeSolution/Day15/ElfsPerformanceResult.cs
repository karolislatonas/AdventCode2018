namespace AdventCodeSolution.Day15
{
    public class ElfsPerformanceResult
    {
        public ElfsPerformanceResult(int initialElfsCount, int totalElfDeaths)
        {
            InitialElfsCount = initialElfsCount;
            TotalElfDeaths = totalElfDeaths;
        }

        public int InitialElfsCount { get; }

        public int TotalElfDeaths { get; }

        public bool ElfWons => InitialElfsCount != TotalElfDeaths;
    }
}
