using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverTextElement : MonoBehaviour
{
    public enum TimeType{
        CurrentTime,
        RecordTime
    }
    private TMP_Text tmp_text;
    [SerializeField] private StatisticsCollector statsCollector;

    public TimeType type;

    private void OnEnable()
    {
        tmp_text = GetComponent<TMP_Text>();
        if (type == TimeType.CurrentTime)
            tmp_text.text = GameTimer.GetFormatedTime();
        else
            tmp_text.text = statsCollector.GetFormatedRecordTime();
    }
}
