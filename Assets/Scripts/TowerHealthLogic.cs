using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealthLogic : MonoBehaviour
{
    [SerializeField] public GameObject EndgameMessage;
    [SerializeField] public Button pauseButton;
    [SerializeField] public TowerType type;
    [SerializeField] private StatisticsCollector statsCollector;
    public TowerData Tdata;

    [Header("Audio")]
    [SerializeField] AudioClip DeathSound;
    new AudioSource audio;

    void Start()
    {
        audio= GetComponent<AudioSource>();
        if (Tdata != null) return;
        switch (type)
        {
            case TowerType.Ballista:
                Tdata = TowerManager.GetBallista();
                break;
            case TowerType.Crystal:
                Tdata = TowerManager.GetCrystal();
                break;
            case TowerType.LazerTower:
                Tdata = TowerManager.GetLazerTower();
                break;
            case TowerType.Mushroom:
                Tdata = TowerManager.GetMushroom();
                break;
            case TowerType.TreeHouse:
                Tdata = TowerManager.GetTreeHouse();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Tdata.Health <= 0)
        {
            if (type == TowerType.Crystal)
            {
                GameOver();
            }
            else
            {
                audio.PlayOneShot(DeathSound, 1f);
                Destroy(gameObject);
            }
        }
    }

    private void GameOver()
    {
        TimescaleManager.Pause(true);
        pauseButton.enabled = false;

        int curTime = GameTimer.GetSeconds();
        if (curTime > statsCollector.GetCurrentTimeRecord())
        {
            statsCollector.ChangeTimeRecord(curTime);
        }
        
        EndgameMessage.SetActive(true);
    }
}
