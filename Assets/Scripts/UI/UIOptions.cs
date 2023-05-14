using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIOptions : MonoBehaviour
{
    public UIDocument document;

    private Button backButton;

    private void OnEnable()
    {
        backButton = document.rootVisualElement.Q<Button>("back");

        backButton.clicked += CloseWindow;
    }

    private void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}
