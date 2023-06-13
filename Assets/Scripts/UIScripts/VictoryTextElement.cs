using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryTextElement : MonoBehaviour
{
    public TowerType type;

    [SerializeField] private StatisticsCollector statsCollector;

    private static TMP_Text tmp_text;

    private void OnEnable()
    {
        tmp_text = GetComponent<TMP_Text>();

        SetText(statsCollector);
    }

    private void SetText(StatisticsCollector statsCollector)
    {
        tmp_text.text = "Damage: " + statsCollector.GetDamage(type);
    }
}
