using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public void SelectDifficulty(int difficulty)
    {
        MonsterController.SwitchDifficulty((Difficulty)difficulty);

        var writer = QuickSaveWriter.Create("GameDifficulty");
        writer.Write("difficulty", difficulty);
        writer.Commit();
    }
}
