using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Методы для работы с монстрами
/// </summary>
public static class MonsterController
{
    static List<MonsterData> Monsters = CSVReader.ReadMonsterData();

    /// <summary>
    /// Создаёт данные гоблина
    /// </summary>
    public static MonsterData GetGoblin()
    {
        var monster = Monsters.Where(x => x.Name == "Goblin").ToArray();
        return monster[0];
    }

    /// <summary>
    /// Создаёт данные волка
    /// </summary>
    public static MonsterData GetWolf()
    {
        var monster = Monsters.Where(x => x.Name == "Wolf").ToArray();
        return monster[0];
    }

    /// <summary>
    /// Создаёт данные орка
    /// </summary>
    public static MonsterData GetOrc()
    {
        var monster = Monsters.Where(x => x.Name == "Orc").ToArray();
        return monster[0];
    }

    /// <summary>
    /// Создаёт данные гарпии
    /// </summary>
    public static MonsterData GetHarpy()
    {
        var monster = Monsters.Where(x => x.Name == "Harpy").ToArray();
        return monster[0];
    }
}
