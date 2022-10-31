using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public static class CSVReader
{
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
                        mobData.Movement = double.Parse(mas[k][i], CultureInfo.InvariantCulture);
                        break;
                    case var str when str.Contains("Damage"):
                        mobData.Damage = int.Parse(mas[k][i]);
                        break;
                    case var str when str.Contains("Attack Speed"):
                        mobData.AttacSpeed = double.Parse(mas[k][i], CultureInfo.InvariantCulture);
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
}
