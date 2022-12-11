using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerShow : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Text>().text = "00:00";
        //GameTimer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = GameTimer.GetFormatedTime();
    }
}
