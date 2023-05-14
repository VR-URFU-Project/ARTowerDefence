using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuButtons : MonoBehaviour
{
    public UIDocument document;

    private Button optionsButton;
    private Button newGame;
    private Button loadGame;
    public GameObject optionsMenu;

    void Start()
    {
        optionsButton = document.rootVisualElement.Q<Button>("optionsButton");
        newGame = document.rootVisualElement.Q<Button>("newGame");
        loadGame = document.rootVisualElement.Q<Button>("loadGame");
        

        //optionsButton.RegisterCallback<ClickEvent, VisualElement>(Clicked, optionsMenu);
        optionsButton.clicked += Clicked;
    }

    private void Clicked()
    {
        optionsMenu.SetActive(true);
    }
}
