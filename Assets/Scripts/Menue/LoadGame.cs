using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadSavedGame()
    {
        //var reader = QSReader.Create("SceneData");
        //if(!reader.Exists("scene"))
        //{
        //    return;
        //}
        //var index = reader.Read<int>("scene");

        var writer = QuickSaveWriter.Create("Temp");
        writer.Write("needsLoad", 1);
        writer.Commit();

        //SceneManager.LoadScene(index);
    }
}
