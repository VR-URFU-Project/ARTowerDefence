using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScript : MonoBehaviour
{
    private Button button;
    private TMP_Text text;

    bool changeColor;

    Color brightCol;

    Color darkCol;

    private void Start()
    {
        button = GetComponent<Button>();
        text = gameObject.GetComponentInChildren<TMP_Text>();

        brightCol = new Color(r: 229, g: 203, b: 172);
        darkCol = new Color(r: 50, g: 38, b: 23);

    }

    public void MakeBright()
    {
        text.color = brightCol;
    }

    public void MakeDark()
    {
        text.color = darkCol;
    }
}
