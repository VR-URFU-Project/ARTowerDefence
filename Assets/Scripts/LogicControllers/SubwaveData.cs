using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SubwaveData
{
    public string SpawnType;
    public int Duration;
    public List<MonsterData> Monsters = new List<MonsterData>();

    /// <summary>
    /// Парсит строку с врагами и записывает их в список
    /// </summary>
    /// <param name="wave"></param>
    public void GenerateEnemies(string wave)
    {
        var parts = wave.Split('+').Select(x => x.Trim()).ToList();
        foreach (var part in parts)
        {
            switch (part[part.Length - 1])
            {
                case 'g':
                    for(int i=0; i< int.Parse(part.Substring(0, part.Length - 1)); ++i)
                        Monsters.Add(MonsterController.GetGoblin());
                    break;
                case 'w':
                    for (int i = 0; i < int.Parse(part.Substring(0, part.Length - 1)); ++i)
                        Monsters.Add(MonsterController.GetWolf());
                    break;
                case 'o':
                    for (int i = 0; i < int.Parse(part.Substring(0, part.Length - 1)); ++i)
                        Monsters.Add(MonsterController.GetOrc());
                    break;
                case 'h':
                    for (int i = 0; i < int.Parse(part.Substring(0, part.Length - 1)); ++i)
                        Monsters.Add(MonsterController.GetHarpy());
                    break;
            }
        }
    }
}
