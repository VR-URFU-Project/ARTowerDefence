using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpdateController
{
    private static Dictionary<TowerType, List<string>> LevelData = CSVReader.ReadUpdateData();

    public static string GetUpdateString(TowerType towerType, int currentLevel)
    {
        if (currentLevel > LevelData[towerType].Count)
            return null;
        return LevelData[towerType][currentLevel - 1];
    }
}
