using AdventCodeSolution.Day22.Regions;

namespace AdventCodeSolution.Day22.Equipments
{
    public class ClimbingGear : IEquipment
    {
        private ClimbingGear()
        {

        }

        public bool CanEnter(CaveRegion region)
        {
            return region is RockyRegion ||
                region is WetRegion;
        }

        public static ClimbingGear Value { get; } = new ClimbingGear();

        public override string ToString() => GetType().Name;
    }
}
