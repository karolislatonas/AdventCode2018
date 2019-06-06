namespace AdventCodeSolution.Day5
{
    public class UnitFilter : IUnitFilter
    {
        private readonly char upperUnit;
        private readonly char lowerUnit;

        public UnitFilter(char unit)
        {
            lowerUnit = char.ToLower(unit);
            upperUnit = char.ToUpper(unit);
        }

        public bool CanBeAdded(char unit)
        {
            return unit != upperUnit && unit != lowerUnit;
        }
    }
}
