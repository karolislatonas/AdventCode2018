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

        public CaveSearch ApplyAction(CaveSearch caveSearch) => caveSearch.SwitchEquipmentTo(newEquipment);
    }
}
