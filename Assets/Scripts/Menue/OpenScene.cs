using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
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
}
