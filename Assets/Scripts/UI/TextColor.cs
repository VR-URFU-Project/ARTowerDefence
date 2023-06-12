using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextColor : MonoBehaviour
{
    public Color enableColor;
    public Color disableColor;

    private Button parentButton;

    private void Start()
    {
        parentButton = transform.parent.GetComponent<Button>();
    }

    private void Update()
    {
        if (parentButton.interactable)
        {
            GetComponent<TMP_Text>().color = enableColor;
        }
        else
        {
            GetComponent<TMP_Text>().color = disableColor;
        }
    }
}
