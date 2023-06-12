using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private GameObject gamingPlace;
    //private TowerSpawner towerSpawner;
    [Header("Text Fields")]
    [SerializeField] private TMP_Text ballistaPrice;
    [SerializeField] private TMP_Text treeHousePrice;
    [SerializeField] private TMP_Text mushroomPrice;
    [SerializeField] private TMP_Text lazerTowerPrice;

    private void Start()
    {
        gamingPlace = GameObject.FindWithTag("GamingPlace");
        //towerSpawner = new TowerSpawner();
        ballistaPrice.text = TowerManager.GetBallista().Price.ToString();
        treeHousePrice.text = TowerManager.GetTreeHouse().Price.ToString();
        mushroomPrice.text = TowerManager.GetMushroom().Price.ToString();
        lazerTowerPrice.text = TowerManager.GetLazerTower().Price.ToString();
    }

    private TowerData data;


    // обновление и деактивация кнопок на те башни, на которые не хватает денег
    private void Update()
    {
        //ChangeInteractableState(ballistaPrice.transform.parent.gameObject.GetComponent<Button>(), TowerManager.GetBallista());
        //ChangeInteractableState(treeHousePrice.transform.parent.gameObject.GetComponent<Button>(), TowerManager.GetTreeHouse());
        //ChangeInteractableState(mushroomPrice.transform.parent.gameObject.GetComponent<Button>(), TowerManager.GetMushroom());
        //ChangeInteractableState(lazerTowerPrice.transform.parent.gameObject.GetComponent<Button>(), TowerManager.GetLazerTower());


        if (MoneySystem.GetMoney() < TowerManager.GetBallista().Price)
        {
            ballistaPrice.transform.parent.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            ballistaPrice.transform.parent.gameObject.GetComponent<Button>().interactable = true;
        }

        if (MoneySystem.GetMoney() < TowerManager.GetTreeHouse().Price)
        {
            treeHousePrice.transform.parent.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            treeHousePrice.transform.parent.gameObject.GetComponent<Button>().interactable = true;
        }

        if (MoneySystem.GetMoney() < TowerManager.GetMushroom().Price)
        {
            mushroomPrice.transform.parent.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            mushroomPrice.transform.parent.gameObject.GetComponent<Button>().interactable = true;
        }

        if (MoneySystem.GetMoney() < TowerManager.GetLazerTower().Price)
        {
            lazerTowerPrice.transform.parent.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            lazerTowerPrice.transform.parent.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    //private void ChangeInteractableState(Button button, TowerData td)
    //{
    //    if(MoneySystem.GetMoney() < td.Price)
    //    {
    //        button.interactable = false;
    //    }
    //    else
    //    {
    //        button.interactable = true;
    //    }
    //}

    public void Purchase(string type)
    {
        switch (type)
        {
            case "Ballista":
                data = TowerManager.GetBallista();
                if (MoneySystem.GetMoney() >= data.Price)
                    Instantiate(data.shapePrefab, gamingPlace.transform);
                else
                {
                    CameraHandler.ChangeShopItemSelectedStage(false);
                }
                break;

            case "TreeHouse":
                data = TowerManager.GetTreeHouse();
                if (MoneySystem.GetMoney() >= data.Price)
                    Instantiate(data.shapePrefab, gamingPlace.transform);
                else
                {
                    CameraHandler.ChangeShopItemSelectedStage(false);
                }
                break;

            case "Mushroom":
                data = TowerManager.GetMushroom();
                if (MoneySystem.GetMoney() >= data.Price)
                    Instantiate(data.shapePrefab, gamingPlace.transform);
                else
                {
                    CameraHandler.ChangeShopItemSelectedStage(false);
                }
                break;

            case "LazerTower":
                data = TowerManager.GetLazerTower();
                if (MoneySystem.GetMoney() >= data.Price)
                    Instantiate(data.shapePrefab, gamingPlace.transform);
                else
                {
                    CameraHandler.ChangeShopItemSelectedStage(false);
                }
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
    //    Debug.Log("�� ������ ��������");
    //    Instantiate(ballistaShapePrefab, gamingPlace.transform);
    //}
}
