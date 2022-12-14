using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    
    private void Update()
    {
        if (Time.timeScale == 0)
        {
            gameObject.GetComponentsInChildren<Image>()[0].sprite = Resources.Load<Sprite>("play icon");
            gameObject.GetComponentsInChildren<Image>()[0].SetNativeSize();
        }
            
        else
        {
            gameObject.GetComponentsInChildren<Image>()[0].sprite = Resources.Load<Sprite>("pause icon");
            gameObject.GetComponentsInChildren<Image>()[0].SetNativeSize();
        }
            
    }

    /// <summary>
    /// При нажатии на кнопку
    /// </summary>
    public void PressButton()
    {
        PauseManager.TogglePause(true);
        //gameObject.GetComponentInChildren<Text>().text = (Time.timeScale == 0) ? "Resume" : "Pause";
    }
}
