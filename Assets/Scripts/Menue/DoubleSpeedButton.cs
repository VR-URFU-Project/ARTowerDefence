using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleSpeedButton : MonoBehaviour
{
    private Button button;
    private float speed;
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        speed = 2f;
    }

    public void ToggleSpeed()
    {
        if (Time.timeScale == 0) return;

        Time.timeScale = speed;

        speed = (speed == 1) ?  2 : 1;
    }
}
