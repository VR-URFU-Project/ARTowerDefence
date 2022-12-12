using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

public class SaveGame : MonoBehaviour
{
    public void SaveGameData()
    {
        //var writer = QuickSaveWriter.Create("GameData");
        //writer.Write("status", 1);
        //writer.Commit();
        GameTimer.Save();
        MoneySystem.Save();
        var obj = GameObject.FindGameObjectsWithTag("tag");
        if (obj == null) Debug.LogError("Не находит префаб при сохранении");
        obj[0].GetComponent<StartWawe>().Save();
    }

    public void LoadGameData()
    {
        GameTimer.Load();
        MoneySystem.Load();
        var obj = GameObject.FindGameObjectsWithTag("tag");
        if (obj == null) Debug.LogError("Не находит префаб при загрузке");
        obj[0].GetComponent<StartWawe>().Load();
    }
}
