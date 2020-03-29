using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCodeSolution.Day24
{
    public class ImmuneSystemInfoPrinter
    {
        public void PrintInfo(ImmuneSystem immuneSystem, int damageIncrease)
        {
            AppendContent(
                $"Damage increase: {damageIncrease}" +
                Environment.NewLine +
                $"{CreateGroupsInfo("Immunes: ", immuneSystem.AliveImmunities)}" +
                Environment.NewLine +
                $"{CreateGroupsInfo("Infections: ", immuneSystem.AliveInfections)}" +
                Environment.NewLine +
                Environment.NewLine);
        }

        private string CreateGroupsInfo(string header, IEnumerable<UnitsGroup> unitsGroups)
        {
            var aliveUnitsGroups = unitsGroups.Where(u => u.IsAlive).ToArray();

            var aliveCount = aliveUnitsGroups.Length;
            var totalUnits = unitsGroups.Sum(u => u.UnitsCount);

            return $"{header} Groups Count: {aliveCount}, Total Units: {totalUnits}";
        }

        private void AppendContent(string content)
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = Path.Combine(desktopPath, "day24data.txt");

            File.AppendAllText(filePath, content);
        }
    }
}
