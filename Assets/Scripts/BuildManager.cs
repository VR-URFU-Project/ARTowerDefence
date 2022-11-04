using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager GameObject in this scene!");
            return; 
        }
        instance = this;
    }

    public GameObject canonPrefab;
    public GameObject airCanonPrefab;

    private GameObject towerToBuild;

    public GameObject GetCanon()
    {
        return towerToBuild;
    }

    public void SetTowerToBuild(GameObject tower)
    {
        towerToBuild = tower;
    }
}
