using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System.Collections;

public static class CSVReader
{
    private static Dictionary<string, TowerType> towerMapping = new Dictionary<string, TowerType>()
        {
            {"Crystal", TowerType.Crystal },
            {"Balista", TowerType.Ballista },
            {"Archer tower", TowerType.TreeHouse },
            {"Shroom tower", TowerType.Mushroom },
            {"Laser tower", TowerType.LazerTower }
        };

    /// <summary>
    /// Возвращает список существующих монстров с характеристиками
    /// </summary>
    public static List<MonsterData> ReadMonsterData()
    {
        var strData = Resources
            .Load<TextAsset>("MobStats")
            .text
            .Split('\n');

        var mobsData = new List<MonsterData>();
        var mas = new List<List<string>>();

        foreach (var line in strData)
        {
            mas.Add(line.Split(';').Select(x => x.Trim()).ToList());
        }

        for (int i = 1; i < mas[0].Count; ++i)
        {
            var mobData = new MonsterData();
            for (int k = 0; k < mas.Count; ++k)
            {
                switch (mas[k][0])
                {
                    case var str when str.Contains("Name"):
                        mobData.Name = mas[k][i];
                        break;
                    case var str when str.Contains("Health"):
                        mobData.Health = int.Parse(mas[k][i]);
                        break;
                    case var str when str.Contains("Movement"):
                        mobData.Movement = float.Parse(mas[k][i], CultureInfo.InvariantCulture);
                        break;
                    case var str when str.Contains("Damage"):
                        mobData.Damage = int.Parse(mas[k][i]);
                        break;
                    case var str when str.Contains("Attack Speed"):
                        mobData.AttackSpeed = float.Parse(mas[k][i], CultureInfo.InvariantCulture);
                        break;
                    case var str when str.Contains("Attack Range"):
                        mobData.AttacRange = double.Parse(mas[k][i], CultureInfo.InvariantCulture);
                        break;
                    case var str when str.Contains("Flight"):
                        mobData.Flight = mas[k][i] == "No" ? false : true;
                        break;
                    case var str when str.Contains("Money"):
                        mobData.Money = int.Parse(mas[k][i]);
                        break;
                }
            }
            mobsData.Add(mobData);
        }

        return mobsData;
    }

    /// <summary>
    /// Возвращает список волн с информацией по ним
    /// </summary>
    public static List<WaveData> ReadWaveData()
    {
        var strData = Resources
            .Load<TextAsset>("Waves")
            //.Load<TextAsset>("Waves debug")
            .text
            .Split('\n').Skip(1).ToArray();
        var waves = new List<WaveData>();

        foreach (var line in strData)
        {
            var wave = new WaveData();
            var parts = line.Split(',').Skip(1).Select(x => x.Trim()).ToArray();
            foreach (var part in parts)
            {
                if (part != "")
                    wave.AddSubwave(part);
            }
            waves.Add(wave);
        }
        return waves;
    }

    public static List<int> ReadWaveTimeData()
    {
        var strData = Resources
            .Load<TextAsset>("Waves")
            .text
            .Split('\n')
            .Skip(1)
            .Select(x => x.Split(',')[0])
            .ToArray();

        List<int> wavesTime = new List<int>();
        foreach (var line in strData)
        {
            var parts = line.Split(':').Select(x => x.Trim()).ToArray();
            int time = 0;
            foreach (var part in parts)
            {
                if (part == "")
                {
                    Debug.LogError("ошибка парсинга времени");
                    continue;
                }

                time = (time + int.Parse(part)) * 60;   
            }
            time /= 60;
            wavesTime.Add(time);
        }
        return wavesTime;
    }

    public static List<TowerData> ReadTowerData()
    {
        var strData = Resources
            .Load<TextAsset>("TowerStats")
            .text
            .Split('\n');
        
        var towersStats = new List<TowerData>();
        var mas = new List<List<string>>();

        

        foreach(var line in strData)
        {
            mas.Add(line.Split(',').Select(x => x.Trim()).ToList());
        }

        for (int i = 1; i < mas[0].Count; i++)
        {
            var towerData = new TowerData();

            for (int k = 0; k < mas.Count; ++k)
            {
                switch (mas[k][0])
                {
                    case var str when str.Contains("Name"):
                        towerData.Type = towerMapping[ mas[k][i] ];
                        break;
                    case var str when str.Contains("Range"):
                        towerData.Range = double.Parse(mas[k][i], CultureInfo.InvariantCulture);
                        break;
                    case var str when str.Contains("Health"):
                        towerData.Health = int.Parse(mas[k][i]);
                        break;
                    case var str when str.Contains("Damage"):
                        towerData.Damage = int.Parse(mas[k][i]);
                        break;
                    case var str when str.Contains("Attack speed"):
                        towerData.AtackSpeed = double.Parse(mas[k][i], CultureInfo.InvariantCulture);
                        break;
                    case var str when str.Contains("Projectile speed"):
                        towerData.ProjectileSpeed = double.Parse(mas[k][i], CultureInfo.InvariantCulture);
                        break;
                    case var str when str.Contains("Targets amount"):
                        towerData.TargetsAmount = int.Parse(mas[k][i]);
                        break;
                    case var str when str.Contains("Can shoot up"):
                        towerData.PVO_enabled = mas[k][i] == "Yes";
                        break;
                    case var str when str.Contains("Price"):
                        towerData.Price = int.Parse(mas[k][i]);
                        break;
                }
                
            }
            towersStats.Add(towerData);
        }
        return towersStats;
    }

    public static Dictionary<TowerType, List<string>> ReadUpdateData()
    {
        var strData = Resources
            .Load<TextAsset>("UpdateStats")
            .text
            .Split('\n');

        var updateStrings = new Dictionary<TowerType, List<string>>();

        var updateLists = new List<string>[strData.Length-1];

        for(int i = 1; i< strData.Length; ++i)
        {
            updateLists[i - 1] = strData[i]
                                .Split(',')
                                .Skip(1)
                                .Select(x => x.Trim())
                                .ToList();
        }

        var names = strData[0].Split(',').Skip(1).Select(x => x.Trim()).ToArray();
        for (int i = 0; i < names.Length; ++i)
        {
            updateStrings.Add(towerMapping[names[i]], new List<string>());
            for(int k = 0; k< updateLists.Length; ++k)
            {
                updateStrings[towerMapping[names[i]]].Add(updateLists[k][i]);
            }
        }

        return updateStrings;
    }
}
