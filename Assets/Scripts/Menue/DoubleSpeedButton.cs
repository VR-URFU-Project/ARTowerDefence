using System;
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
        TimescaleManager.ScaleTime(speed);
        //Debug.Log(speed);
        speed = (Math.Abs(speed-1f) < 0.001f) ?  2f : 1f;
    }
}
