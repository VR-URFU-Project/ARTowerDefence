using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class YesNoPanelLogic : MonoBehaviour
{
    private Button yesButton;
    private Button noButton;
    private TMP_Text textPlace;

    private int counter=0;

    private void OnEnable()
    {
        yesButton = GameObject.FindGameObjectWithTag("Yes")?.GetComponent<Button>();
        noButton = GameObject.FindGameObjectWithTag("No")?.GetComponent<Button>();
        //textPlace = GameObject.Find("Question")?.GetComponent<TMP_Text>();
        noButton.onClick.AddListener(() => { Destroy(gameObject); });
    }

    public void SetText(string text)
    {
        textPlace.text = text;
    }

    public void SetText(string text, Color color)
    {
        textPlace.color = color;
        textPlace.text = text;
    }

    public void SetYesAction(Func<bool> toExec)
    {
        yesButton.onClick.AddListener(() => {
            counter++;
            if (counter != 1) return;
            if (!toExec())
            {
                counter = 0;
                return;
            }
            Destroy(gameObject);
        });
    }

    public void SetNoAction(Action toExec)
    {
        noButton.onClick.AddListener(() => {
            counter++;
            if (counter != 1) return;
            toExec();
        });
    }
}
