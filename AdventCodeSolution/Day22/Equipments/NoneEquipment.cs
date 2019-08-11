using AdventCodeSolution.Day22.Regions;

namespace AdventCodeSolution.Day22.Equipments
{
    public class NoneEquipment : IEquipment
    {
        private NoneEquipment()
        {

        }

        public bool CanEnter(CaveRegion region)
        {
            return region is WetRegion ||
                region is NarrowRegion;
        }

        public static NoneEquipment Value { get; } = new NoneEquipment();

        public override string ToString() => GetType().Name;
    }
}
