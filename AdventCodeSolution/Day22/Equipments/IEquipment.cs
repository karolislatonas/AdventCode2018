using AdventCodeSolution.Day22.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCodeSolution.Day22.Equipments
{
    public interface IEquipment
    {
        bool CanEnter(CaveRegion region);
    }
}
