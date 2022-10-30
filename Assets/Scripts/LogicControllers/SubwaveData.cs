using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SubwaveData
{
    public string SpawnType;
    public int Duration;
    public List<MonsterData> Monsters = new List<MonsterData>();

    public void GenerateEnemies(string wave)
    {
        var parts = wave.Split('+').Select(x => x.Trim()).ToList();
        foreach (var part in parts)
        {
            switch (part[part.Length - 1])
            {
                case 'g':
                    Monsters.Add(MonsterController.GetGoblin());
                    break;
                case 'w':
                    Monsters.Add(MonsterController.GetWolf());
                    break;
                case 'o':
                    Monsters.Add(MonsterController.GetOrc());
                    break;
                case 'h':
                    Monsters.Add(MonsterController.GetHarpy());
                    break;
            }
        }
    }
}
