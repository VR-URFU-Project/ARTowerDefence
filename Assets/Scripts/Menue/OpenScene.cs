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
}
