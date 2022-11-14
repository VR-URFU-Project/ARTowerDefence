﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Методы для работы с башнями
/// </summary>
public static class TowerManager
{
    static List<TowerData> Towers = CSVReader.ReadTowerData();

    /// <summary>
    /// Получить данные о баллисте
    /// </summary>
    public static TowerData GetBallista()
    {
        var tower = Towers.Where(x => x.Type == TowerType.Ballista).ToArray();
        tower[0].PrefabName = Enum.GetName(typeof(TowerType), TowerType.Ballista);
        tower[0].ShapePrefabName = "shape_"+ Enum.GetName(typeof(TowerType), TowerType.Ballista);
        return tower[0];
    }

    /// <summary>
    /// Получить данные о башне с лучниками
    /// </summary>
    public static TowerData GetTreeHouse()
    {
        var tower = Towers.Where(x => x.Type == TowerType.TreeHouse).ToArray();
        return tower[0];
    }

    /// <summary>
    /// Получить данные о грибе
    /// </summary>
    public static TowerData GetMushroom()
    {
        var tower = Towers.Where(x => x.Type == TowerType.Mushroom).ToArray();
        tower[0].PrefabName = Enum.GetName(typeof(TowerType), TowerType.Mushroom);
        tower[0].ShapePrefabName = "shape_" + Enum.GetName(typeof(TowerType), TowerType.Mushroom);
        return tower[0];
    }

    /// <summary>
    /// Получить данные о лазерной башне
    /// </summary>
    public static TowerData GetLazerTower()
    {
        var tower = Towers.Where(x => x.Type == TowerType.LazerTower).ToArray();
        return tower[0];
    }
}
