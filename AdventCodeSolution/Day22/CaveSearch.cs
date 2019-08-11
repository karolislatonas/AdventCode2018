using AdventCodeSolution.Day03;
using AdventCodeSolution.Day22.Equipments;

namespace AdventCodeSolution.Day22
{
    public class CaveSearch
    {
        public CaveSearch(XY location, IEquipment equipment) :
            this(location, equipment, 0)
            
        {

        }

        private CaveSearch(XY location, IEquipment equipment, int cost)
        {
            Location = location;
            Equipment = equipment;
            Cost = cost;
        }

        public XY Location { get; }

        public IEquipment Equipment { get; }

        public int Cost { get; }

        public CaveSearch Move(XY direction)
        {
            return new CaveSearch(Location + direction, Equipment, Cost + 1);
        }

        public CaveSearch SwitchEquipmentTo(IEquipment equipment)
        {
            return new CaveSearch(Location, equipment, Cost + 7);
        }

        public override string ToString()
        {
            return $"Loc: {Location}, Eq: {Equipment}";
        }
    }
}
