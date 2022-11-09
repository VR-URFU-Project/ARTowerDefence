using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Методы для работы с башнями
/// </summary>
public static class TowerManager
{
    /// <remarks>
    /// TO DO: сделать List башен через CVSReader
    /// </remarks>
    static List<TowerData> Towers = new List<TowerData>();

    /// <summary>
    /// Получить данные о баллисте
    /// </summary>
    public static TowerData GetBallista()
    {
        var tower = Towers.Where(x => x.Type == TowerType.Ballista).ToArray();
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
