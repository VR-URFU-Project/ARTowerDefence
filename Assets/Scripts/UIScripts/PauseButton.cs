using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    //[SerializeField] Button pauseButton;
    //[SerializeField] Button startButton;
    //private int flag = 0;

    private void Start()
    {
        gameObject.GetComponentInChildren<Text>().text = (Time.timeScale == 0) ? "Resume" : "Pause";
    }

    /// <summary>
    /// При нажатии на кнопку
    /// </summary>
    public void PressButton()
    {
        PauseManager.TogglePause();
        gameObject.GetComponentInChildren<Text>().text = (Time.timeScale == 0) ? "Resume" : "Pause";
    }
}
