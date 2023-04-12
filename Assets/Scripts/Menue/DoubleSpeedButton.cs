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
        if (Time.timeScale == 0) return;

        Time.timeScale = speed;
        GameTimer.StartTimer((int)(1000 / speed));

        speed = (speed == 1) ?  2 : 1;
    }
}
