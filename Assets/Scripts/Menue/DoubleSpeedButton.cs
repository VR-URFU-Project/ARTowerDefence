using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleSpeedButton : MonoBehaviour
{
    private float speed;

    void Start()
    {
        speed = 2f;
    }

    public void ToggleSpeed()
    {

        Time.timeScale = speed;
        GameTimer.SetTimer((int)(1000 / speed));
        Debug.Log(speed);
        speed = (speed == 1) ?  2 : 1;
    }
}
