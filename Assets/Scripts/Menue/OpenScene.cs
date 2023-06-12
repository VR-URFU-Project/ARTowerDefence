using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    private static string selectedMode;
    public void OpenNewScene(int sceneId)
    {
        //var oldScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneId);
        //SceneManager.UnloadSceneAsync(oldScene);
    }

    public void ChangeMode()
    {
        var currentId = SceneManager.GetActiveScene().buildIndex;
        if(currentId == 1)
            SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SelectMode(string mode)
    {
        selectedMode = mode;
    }

    public void OpenSelectedScene()
    {
        int sceneId = selectedMode == "AR" ? 1 : 2;
        SceneManager.LoadScene(sceneId);
    }
}
