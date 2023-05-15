using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleSpeedButton : MonoBehaviour
{
    private float speed;

    [SerializeField] Sprite buttonIsActive;
    [SerializeField] Sprite buttonIsInactive;

    void Start()
    {
        speed = 2f;
    }

    public void ToggleSpeed()
    {
        TimescaleManager.ScaleTime(speed);
        if (speed > 1)
        {
            gameObject.GetComponent<Image>().sprite = buttonIsActive;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = buttonIsInactive;
        }
        //Debug.Log(speed);
        speed = (Math.Abs(speed-1f) < 0.001f) ?  2f : 1f;
    }
}
