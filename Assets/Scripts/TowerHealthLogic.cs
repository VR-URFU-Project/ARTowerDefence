using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealthLogic : MonoBehaviour
{
    [SerializeField] public GameObject gg;
    [SerializeField] public TowerType type;
    public TowerData Tdata;

    [Header("Audio")]
    [SerializeField] AudioClip DeathSound;
    AudioSource audio;

    void Start()
    {
        audio= GetComponent<AudioSource>();
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
                Time.timeScale = 0;
                gg.SetActive(true);
            }
            else
            {
                audio.PlayOneShot(DeathSound, 1f);
                Destroy(gameObject);
            }
        }
    }
}
