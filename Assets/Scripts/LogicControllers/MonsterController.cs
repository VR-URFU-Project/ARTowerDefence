using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Методы для работы с монстрами
/// </summary>
public static class MonsterController
{
    static List<MonsterData> Monsters = CSVReader.ReadMonsterData();
    static private float purpleChance = 0.1f;
    static private float blackChance = 0.2f;


    /// <summary>
    /// Создаёт данные гоблина
    /// </summary>
    public static MonsterData GetGoblin()
    {
        var monster = Monsters.Where(x => x.Name == "Goblin").ToArray();
        return new MonsterData(monster[0]);
    }

    /// <summary>
    /// Создаёт данные волка
    /// </summary>
    public static MonsterData GetWolf()
    {
        var monster = Monsters.Where(x => x.Name == "Wolf").ToArray();
        return new MonsterData(monster[0]);
    }

    /// <summary>
    /// Создаёт данные орка
    /// </summary>
    public static MonsterData GetOrc()
    {
        var monster = Monsters.Where(x => x.Name == "Orc").ToArray();
        return new MonsterData(monster[0]);
    }

    /// <summary>
    /// Создаёт данные гарпии
    /// </summary>
    public static MonsterData GetHarpy()
    {
        var monster = Monsters.Where(x => x.Name == "Harpy").ToArray();
        return new MonsterData(monster[0]);
    }

    public static MonsterData GetMutatedEnemy(MonsterData data)
    {
        var chance = Random.value;
        var newData = new MonsterData(data);
        if (chance < blackChance && GameTimer.GetSeconds() > 60)
            MakeEnemyBlack(newData);
        if (chance > 1 - purpleChance && GameTimer.GetSeconds() > 300)
            MakeEnemyPurple(newData);
        return newData;
    }

    public static void MakeEnemyPurple(MonsterData data)
    {
        //Debug.Log("Wait for purple");
        data.Name = data.Name+"_purple";
        data.Health = data.Health * 2;
        data.Movement = data.Movement * 1.5f;
        data.Damage = data.Damage * 4;
        data.AttackSpeed = data.AttackSpeed * 1.5f;
        data.Money = data.Money * 4;
    }

    public static void MakeEnemyBlack(MonsterData data)
    {
        //Debug.Log("Wait for black");
        data.Name = data.Name + "_black";
        data.Health = data.Health * 4;
        data.Movement = data.Movement * 2f;
        data.Damage = data.Damage * 8;
        data.AttackSpeed = data.AttackSpeed * 2f;
        data.Money = data.Money * 8;
    }

    public static void SwitchDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.easy:
                purpleChance = 0f;
                blackChance = 0.1f;
                break;
            case Difficulty.normal:
                purpleChance = 0.1f;
                blackChance = 0.2f;
                break;
            case Difficulty.hard:
                purpleChance = 0.2f;
                blackChance = 0.3f;
                break;
        }
    }
}
