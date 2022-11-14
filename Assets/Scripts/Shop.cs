using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
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
                if (MoneySystem.GetMoney() >= data.Price)
                    Instantiate(data.shapePrefab, gamingPlace.transform);
                break;

            case "TreeHouse":
                data = TowerManager.GetTreeHouse();
                Instantiate(data.shapePrefab, gamingPlace.transform);
                break;

            case "Mushroom":
                data = TowerManager.GetMushroom();
                Instantiate(data.shapePrefab, 
                    //data.shapePrefab.transform.parent.position, 
                    //data.shapePrefab.transform.parent.rotation, 
                    gamingPlace.transform);
                break;

            case "LazerTower":
                data = TowerManager.GetLazerTower();
                Instantiate(data.shapePrefab, gamingPlace.transform);
                break;
        }
    }

    public void CreateTower(Transform shape, Transform gamingPlace)
    {
        MoneySystem.ChangeMoney(-data.Price);
        var obj = Instantiate(data.prefab, shape.position, shape.rotation, gamingPlace);
        obj.GetComponent<Canon>().Tdata = this.data;
    }

    //public void PurchaseBallista()
    //{
    //    Debug.Log("Вы купили баллисту");
    //    Instantiate(ballistaShapePrefab, gamingPlace.transform);
    //}
}
