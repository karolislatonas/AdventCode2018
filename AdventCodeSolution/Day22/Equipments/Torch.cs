using System;
using AdventCodeSolution.Day22.Regions;

namespace AdventCodeSolution.Day22.Equipments
{
    public class Torch : IEquipment
    {
        private Torch()
        {

        }

        public bool CanEnter(CaveRegion region)
        {
            return region is RockyRegion ||
                region is NarrowRegion;
        }

        public static Torch Value { get; } = new Torch();

        public override string ToString() => GetType().Name;
    }
}
