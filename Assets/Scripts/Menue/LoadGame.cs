using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadSavedGame()
    {
        var reader = QSReader.Create("SceneData");
        if (!reader.Exists("scene"))
        {
            return;
        }
        var index = reader.Read<int>("scene");

        var writer = QuickSaveWriter.Create("Temp");
        reader = QSReader.Create("DataExists");
        if (reader.Exists("HasSavedData") && reader.Read<int>("HasSavedData") == 1)
            writer.Write("needsLoad", 1);
        else
            writer.Write("needsLoad", 0);
        writer.Commit();

        SceneManager.LoadScene(index);
    }

    public void NotLoadSavedGame()
    {
        var writer = QuickSaveWriter.Create("Temp");
        writer.Write("needsLoad", 0);
        writer.Commit();
    }
}
