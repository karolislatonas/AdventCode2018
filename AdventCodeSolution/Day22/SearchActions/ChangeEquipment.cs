using AdventCodeSolution.Day22.Equipments;

namespace AdventCodeSolution.Day22.SearchActions
{
    public class ChangeEquipment : IAction
    {
        private readonly IEquipment newEquipment;

        public ChangeEquipment(IEquipment newEquipment)
        {
            this.newEquipment = newEquipment;
        }

        public CaveSearch ApplyAction(CaveSearch caveSearch)
        {
            if (caveSearch.Equipment == newEquipment)
                return caveSearch;

            return caveSearch.SwitchEquipmentTo(newEquipment);
        }
    }
}
