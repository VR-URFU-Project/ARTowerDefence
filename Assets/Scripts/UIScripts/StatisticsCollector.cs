using CI.QuickSave;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatisticsCollector : MonoBehaviour
{

    private static int BallistaDamage { get; set; }
    private static int TreeHouseDamage { get; set; }
    private static int MushroomDamage { get; set; }
    private static int LazerTowerDamage { get; set; }

    private static int CurrentTimeRecord { get; set; }

    public void AddDamageToStatistics(TowerType type, int damage)
    {
        switch (type)
        {
            case TowerType.Ballista:
                BallistaDamage += damage;
                break;
            case TowerType.TreeHouse:
                TreeHouseDamage += damage;
                break;
            case TowerType.Mushroom:
                MushroomDamage += damage;
                break;
            case TowerType.LazerTower:
                LazerTowerDamage += damage;
                break;
        }
    }
    
    public string GetDamage(TowerType type)
    {
        string ret = "";
        switch(type)
        {
            case TowerType.Ballista:
                ret = BallistaDamage.ToString();
                break;
            case TowerType.TreeHouse:
                ret =  TreeHouseDamage.ToString();
                break;
            case TowerType.Mushroom:
                ret = MushroomDamage.ToString();
                break;
            case TowerType.LazerTower:
                ret =  LazerTowerDamage.ToString();
                break;
        }
        return ret;
    }

    public int GetCurrentTimeRecord()
    {
        return CurrentTimeRecord;
    }

    public void ChangeTimeRecord(int newRecord)
    {
        CurrentTimeRecord = newRecord;
    }

    public string GetFormatedRecordTime()
    {
        int min = CurrentTimeRecord / 60;
        int sec = CurrentTimeRecord % 60;
        string formated = ((min < 10) ? "0" + min.ToString() : min.ToString()) +
                    ":" +
                    ((sec < 10) ? "0" + sec.ToString() : sec.ToString());
        return formated;
    }

    public static void SaveStatistics()
    {
        var timeWriter = QuickSaveWriter.Create("TimeStatistics");
        timeWriter.Write("TimeRecord", CurrentTimeRecord);

        var damageWriter = QuickSaveWriter.Create("DamageStatistics");
        damageWriter.Write("Ballista", BallistaDamage);
        damageWriter.Write("TreeHouse", TreeHouseDamage);
        damageWriter.Write("Mushroom", MushroomDamage);
        damageWriter.Write("LazerTower", LazerTowerDamage);
        damageWriter.Commit();
    }

    public static void LoadStatistics()
    {
        var reader = QSReader.Create("DamageStatistics");
        if (reader.Exists("Ballista")) BallistaDamage = reader.Read<int>("Ballista");
        if (reader.Exists("TreeHouse")) TreeHouseDamage = reader.Read<int>("TreeHouse");
        if (reader.Exists("Mushroom")) MushroomDamage = reader.Read<int>("Mushroom");
        if (reader.Exists("LazerTower")) LazerTowerDamage = reader.Read<int>("LazerTower");

        reader = QSReader.Create("TimeStatistics");
        if (reader.Exists("TimeRecord")) CurrentTimeRecord = reader.Read<int>("TimeRecord");
    }
}
