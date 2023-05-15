using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using CI.QuickSave;

public static class GameDataController
{
    private static EnemySpawner enemySpawner;

    public static void SaveGameData()
    {
        var writer = QuickSaveWriter.Create("DataExists");
        writer.Write("HasSavedData", 1);
        writer.Commit();

        GameTimer.Save();
        MoneySystem.Save();

        var waweData = GameObject.FindGameObjectsWithTag("MainPrefab");
        if (waweData == null) Debug.LogError("Не находит wrapper при сохранении");
        waweData[0].GetComponent<StartWawe>().Save();

        writer = QuickSaveWriter.Create("SceneData");
        writer.Write("scene", SceneManager.GetActiveScene().buildIndex);
        writer.Commit();

        SaveTowers();
        SaveEnemies();
    }

    public static void LoadGameData()
    {
        var writer = QuickSaveWriter.Create("DataExists");
        writer.Write("HasSavedData", 0);
        writer.Commit();

        GameTimer.Load();
        MoneySystem.Load();

        var waweData = GameObject.FindGameObjectWithTag("MainPrefab");
        if (waweData == null) Debug.LogError("Не находит wrapper при загрузке игры");
        enemySpawner = waweData.GetComponent<EnemySpawner>();
        waweData.GetComponent<StartWawe>().Load();

        //var wrapper = GameObject.FindGameObjectsWithTag("GamingPlace");
        //if (wrapper == null) Debug.LogError("Не находит префаб при загрузке башен");
        //wrapper[0].GetComponent<LoadTowers>().Load();

        LoadTowers();
        LoadEnemies();
    }

    private static void SaveTowers()
    {
        QuickSaveWriter writer;
        var towers = GameObject.FindGameObjectsWithTag("Towers");
        writer = QuickSaveWriter.Create("ToverData");
        writer.Write("amount", towers.Length);
        writer.Commit();

        for (int i = 0; i < towers.Length; i++)
        {
            var a = towers[i].GetComponent<TowerHealthLogic>().Tdata;
            writer.Write("health" + i.ToString(), towers[i].GetComponent<TowerHealthLogic>().Tdata.Health)
                .Write("position" + i.ToString(), towers[i].transform.localPosition)
                .Write("type" + i.ToString(), towers[i].GetComponent<TowerHealthLogic>().type)
                .Write("level" + i.ToString(), towers[i].GetComponent<TowerHealthLogic>().Tdata.Level);
            //writer.Write("tower" + i.ToString(), towers[i]);
            writer.Commit();
        }
    }

    private static void LoadTowers()
    {

        var wrapper = GameObject.FindGameObjectWithTag("GamingPlace");
        if (wrapper == null) Debug.LogError("Не находит префаб при загрузке башен");

        var reader = QSReader.Create("ToverData");
        var amount = reader.Read<int>("amount");
        for (int i = 0; i < amount; i++)
        {
            var type = reader.Read<TowerType>("type" + i.ToString());
            TowerData data = null;
            switch (type)
            {
                case TowerType.Ballista:
                    data = TowerManager.GetBallista();
                    break;

                case TowerType.TreeHouse:
                    data = TowerManager.GetTreeHouse();
                    break;

                case TowerType.Mushroom:
                    data = TowerManager.GetMushroom();
                    break;

                case TowerType.LazerTower:
                    data = TowerManager.GetLazerTower();
                    break;
            }

            var obj = MonoBehaviour.Instantiate(data.prefab, wrapper.transform);
            obj.transform.localPosition = reader.Read<Vector3>("position" + i.ToString());

            var level = reader.Read<int>("level" + i.ToString());
            for (int k = 1; k < level; ++k)
                data.Upgrade();
            data.Health = reader.Read<int>("health" + i.ToString());

            obj.GetComponent<TowerHealthLogic>().Tdata = data;
            var items = obj.GetComponentsInChildren<Canon>();
            foreach (var item in items)
            {
                item.Tdata = data;
            }
        }
    }

    private static void SaveEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy").Concat(GameObject.FindGameObjectsWithTag("Fly")).ToArray();
        QuickSaveWriter writer;
        writer = QuickSaveWriter.Create("Enemies");
        writer.Write("amount", enemies.Length);
        writer.Commit();

        for(int i =0; i<enemies.Length; ++i)
        {
            var a = enemies[i].GetComponent<EnemyScript>().BasicData;
            writer.Write("position" + i.ToString(), enemies[i].transform.localPosition)
                .Write("type" + i.ToString(), a.Name)
                .Write("health" + i.ToString(), a.Health);
            writer.Commit();
        }

    }

    private static void LoadEnemies()
    {

        var wrapper = GameObject.FindGameObjectWithTag("GamingPlace");
        if (wrapper == null) Debug.LogError("Не находит префаб при загрузке башен");

        var reader = QSReader.Create("Enemies");
        var amount = reader.Read<int>("amount");
        for (int i = 0; i < amount; i++)
        {
            var type = reader.Read<string>("type" + i.ToString());
            MonsterData data = null;
            switch (type)
            {
                case var str when str.Contains("Goblin"):
                    data = MonsterController.GetGoblin();
                    break;

                case var str when str.Contains("Wolf"):
                    data = MonsterController.GetWolf();
                    break;

                case var str when str.Contains("Orc"):
                    data = MonsterController.GetOrc();
                    break;

                case var str when str.Contains("Harpy"):
                    data = MonsterController.GetHarpy();
                    break;
            }

            if (type.Contains("purple"))
                MonsterController.MakeEnemyPurple(data);

            if (type.Contains("black"))
                MonsterController.MakeEnemyBlack(data);

            data.Health = reader.Read<int>("health" + i.ToString());

            var coord = wrapper.transform.TransformPoint(reader.Read<Vector3>("position" + i.ToString()));
            var obj = MonoBehaviour.Instantiate(data.prefab, coord, new Quaternion(), wrapper.transform);
            obj.transform.localPosition = reader.Read<Vector3>("position" + i.ToString());

            //enemySpawner.SetVariables(coord, data, wrapper.transform);
            //EnemyScript obj = enemySpawner.enemyPool.Get();


            obj.GetComponent<EnemyScript>().BasicData = data;
        }
    }
}
