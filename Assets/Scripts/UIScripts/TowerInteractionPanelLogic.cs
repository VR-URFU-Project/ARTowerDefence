using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TowerInteractionPanelLogic : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private TMP_Text textPlace;

    private int counter = 0;
    private int deleteCounter = 0;

    // private void OnEnable()
    //{
    //noButton.onClick.AddListener(() => { Destroy(gameObject); });
    //}

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
            Destroy(gameObject);
        });
    }

    public void SetDeleteAction(Action toExec)
    {
        deleteButton.onClick.AddListener(() => {
            deleteCounter++;
            if (deleteCounter == 1)
            {
                yesButton.gameObject.SetActive(false);
                SetText(LocalizationManager.Localize("Towers.ConfirmDelete"), Color.red);
            }
            else if (deleteCounter == 2)
            {
                toExec();
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        });
    }
}
