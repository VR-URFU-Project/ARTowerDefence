using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalLogic : MonoBehaviour
{
    public TowerData Tdata;
    [SerializeField] public GameObject gg;
    
    void Start()
    {
        Tdata = TowerManager.GetCrystal();
    }

    // Update is called once per frame
    void Update()
    {
        if (Tdata.Health <= 0)
        {
            Time.timeScale = 0;
            gg.SetActive(true);
        }
    }
}
