using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverTextElement : MonoBehaviour
{
    private TMP_Text tmp_text;
    [SerializeField] private StatisticsCollector statsCollector;

    private void OnEnable()
    {
        tmp_text = GetComponent<TMP_Text>();
        tmp_text.text = "Time: " + GameTimer.GetFormatedTime()+"\n"+
                        "High Score: " + statsCollector.GetFormatedRecordTime();
    }
}
