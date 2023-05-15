using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuButtons : MonoBehaviour
{
    public UIDocument document;

    private Button optionsButton;
    private Button newGame;
    private Button loadGame;
    
    public GameObject optionsMenu;
    public GameObject viewMode;

    //private OpenScene openScene;
    private LoadGame loadData;


    void Start()
    {
        optionsButton = document.rootVisualElement.Q<Button>("optionsButton");
        newGame = document.rootVisualElement.Q<Button>("newGame");
        loadGame = document.rootVisualElement.Q<Button>("loadGame");

/*        loadData = GetComponent<LoadGame>();
        openScene = GetComponent<OpenScene>();*/

        optionsButton.clicked += new Action(() => OpenOptions(optionsMenu));
        newGame.clicked += new Action(() => OpenOptions(viewMode));
        loadGame.clicked += new Action(() => loadData.LoadSavedGame());
        //loadGame.clicked += new Action(() => OpenScene(1));
    }

    private void OpenScene(int v)
    {
        SceneManager.LoadScene(v);
    }

    private void OpenOptions(GameObject gO)
    {
        gO.SetActive(true);
    }
}
