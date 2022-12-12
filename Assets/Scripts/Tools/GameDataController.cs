using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CI.QuickSave;

public static class GameDataController
{
    public static void SaveGameData()
    {
        //var writer = QuickSaveWriter.Create("GameData");
        //writer.Write("status", 1);
        //writer.Commit();
        GameTimer.Save();
        MoneySystem.Save();
        var obj = GameObject.FindGameObjectsWithTag("MainPrefab");
        if (obj == null) Debug.LogError("�� ������� ������ ��� ����������");
        obj[0].GetComponent<StartWawe>().Save();
        var writer = QuickSaveWriter.Create("SceneData");
        writer.Write("scene", SceneManager.GetActiveScene().buildIndex);
        writer.Commit();

        var towers = GameObject.FindGameObjectsWithTag("Towers");
        writer = QuickSaveWriter.Create("ToverData");
        writer.Write("amount", towers.Length);
        writer.Commit();
        for (int i = 0; i < towers.Length; i++)
        {
            writer.Write("health" + i.ToString(), towers[i].GetComponent<TowerHealthLogic>().Tdata.Health)
                .Write("position" + i.ToString(), towers[i].transform.position)
                .Write("type" + i.ToString(), towers[i].GetComponent<TowerHealthLogic>().type);
            //writer.Write("tower" + i.ToString(), towers[i]);
            writer.Commit();
        }
    }

    public static void LoadGameData()
    {
        GameTimer.Load();
        MoneySystem.Load();
        var mainPref = GameObject.FindGameObjectsWithTag("MainPrefab");
        if (mainPref == null) Debug.LogError("�� ������� ������ ��� �������� ����");
        mainPref[0].GetComponent<StartWawe>().Load();

        var wrapper = GameObject.FindGameObjectsWithTag("GamingPlace");
        if (wrapper == null) Debug.LogError("�� ������� ������ ��� �������� �����");
        wrapper[0].GetComponent<LoadTowers>().Load();
    }
}
