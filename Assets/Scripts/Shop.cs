using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ballistaShapePrefab;
    private GameObject gamingPlace;

    private void Start()
    {
        gamingPlace = GameObject.FindWithTag("GamingPlace");
    }
    
    private TowerData data;

    public void Purchase(string type)
    {
        switch (type)
        {
            case "Ballista":
                data = TowerManager.GetBallista();
                Instantiate(data.shapePrefab, gamingPlace.transform);
                break;

            case "TreeHouse":
                data = TowerManager.GetTreeHouse();
                Instantiate(data.shapePrefab, gamingPlace.transform);
                break;

            case "Mushroom":
                data = TowerManager.GetMushroom();
                Instantiate(data.shapePrefab, gamingPlace.transform);
                break;

            case "LazerTower":
                data = TowerManager.GetLazerTower();
                Instantiate(data.shapePrefab, gamingPlace.transform);
                break;
        }
    }

    public void CreateTower(Transform shape, Transform gamingPlace)
    {
        var obj = Instantiate(data.prefab, shape.position, shape.rotation, gamingPlace);
        obj.GetComponent<Canon>().Tdata = this.data;
    }

    //public void PurchaseBallista()
    //{
    //    Debug.Log("�� ������ ��������");
    //    Instantiate(ballistaShapePrefab, gamingPlace.transform);
    //}
}
