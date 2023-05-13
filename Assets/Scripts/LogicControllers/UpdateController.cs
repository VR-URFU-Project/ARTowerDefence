using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpdateController
{
    private static Dictionary<TowerType, List<string>> LevelData = CSVReader.ReadUpdateData();

    public static string GetUpdateString(TowerType towerType, int currentLevel)
    {
        if (!CanUpgradeTower(towerType, currentLevel))
            return null;
        return LevelData[towerType][currentLevel - 1];
    }

    public static bool CanUpgradeTower(TowerType towerType, int currentLevel)
    {
        if (currentLevel > LevelData[towerType].Count)
            return false;
        return true;
    }
}
