using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] Button pauseButton;
    [SerializeField] Button startButton;
    private int flag = 0;

    private void Start()
    {
        pauseButton.GetComponentInChildren<Text>().text = (flag == 0) ? "Pause" : "Resume";
    }

    /// <summary>
    /// При нажатии на кнопку
    /// </summary>
    public void PressButton()
    {
        Time.timeScale = flag;
        if(flag == 0)
        {
            flag = 1;
            pauseButton.GetComponentInChildren<Text>().text = "Resume";
            startButton.gameObject.SetActive(false);
        }
        else
        {
            flag = 0;
            pauseButton.GetComponentInChildren<Text>().text = "Pause";
            startButton.gameObject.SetActive(true);
        }
    }
}
