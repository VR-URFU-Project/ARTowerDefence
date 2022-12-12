using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

public class SaveGame : MonoBehaviour
{
    public void SaveAll()
    {
        GameDataController.SaveGameData();
    }
}
