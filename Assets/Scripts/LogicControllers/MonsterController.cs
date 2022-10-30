using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class MonsterController
{
    static List<MonsterData> Monsters = CSVReader.ReadMonsterData();

    public static MonsterData GetGoblin()
    {
        var monster = Monsters.Where(x => x.Name == "Goblin").ToArray();
        return monster[0];
    }

    public static MonsterData GetWolf()
    {
        var monster = Monsters.Where(x => x.Name == "Wolf").ToArray();
        return monster[0];
    }

    public static MonsterData GetOrc()
    {
        var monster = Monsters.Where(x => x.Name == "Orc").ToArray();
        return monster[0];
    }

    public static MonsterData GetHarpy()
    {
        var monster = Monsters.Where(x => x.Name == "Harpy").ToArray();
        return monster[0];
    }
}
