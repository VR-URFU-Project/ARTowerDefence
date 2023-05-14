using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerShow : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<TMP_Text>().text = "00:00";
        //GameTimer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TMP_Text>().text = GameTimer.GetFormatedTime();
    }
}
