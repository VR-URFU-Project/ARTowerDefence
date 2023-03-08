using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTower : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] GameObject YesNoPanel;

    [SerializeField] GameObject CloseShop;
    [SerializeField] GameObject OpenShop;
    [SerializeField] GameObject Shop;
    private bool shopState;

    private int layerMask;
    //private bool panelActive = false;

    //private GameObject mainCanvas;

    void Start()
    {
        //mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        //YesNoPanel.SetActive(false);
        //mainCanvas.SetActive(false);
        layerMask = LayerMask.GetMask("Tower");
    }

    void Update()
    {
        if (!(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began || Input.GetMouseButtonDown(0))) return;
        Debug.Log("entered Hitted tower");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("hit tower");
            var tower = hit.collider.gameObject;
            var healthItem = tower.GetComponent<TowerHealthLogic>();
            if (healthItem == null /*|| panelActive*/) return;

            HideShop();
            //panelActive = true;

            var askPanel = Instantiate(YesNoPanel);
            var askPanelLogic = askPanel.GetComponent<YesNoPanelLogic>();
            askPanel.GetComponentInChildren<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            askPanel.GetComponentInChildren<Canvas>().planeDistance = 0.19f;
            askPanelLogic.SetYesAction(() =>
            {
                CameraHandler.ChangeShopItemSelectedStage(false);
                MoneySystem.ChangeMoney(-healthItem.Tdata.UpdatePrice);
                healthItem.Tdata.Upgrade();
                var towerDatas = tower.GetComponentsInChildren<Canon>();
                foreach (var data in towerDatas)
                {
                    data.Tdata.Upgrade();
                }

                ShowShop();
                //panelActive = false;

                return true;
            });

            askPanelLogic.SetNoAction(() => {
                CameraHandler.ChangeShopItemSelectedStage(false);
                ShowShop();
                //panelActive = false;
            });

            if (healthItem.Tdata.UpdatePrice > MoneySystem.GetMoney())
            {
                askPanelLogic.SetText(LocalizationManager.Localize("UpgradeTower.NoMoney") + " " + healthItem.Tdata.UpdatePrice.ToString(), Color.red);
                GameObject.FindGameObjectWithTag("Yes").GetComponent<Button>().interactable = false;
            }
            else
            {
                askPanelLogic.SetText(LocalizationManager.Localize("UpgradeTower.OK") + " " + (healthItem.Tdata.Level + 1).ToString()
                    + "\n"+ LocalizationManager.Localize("UpgradeTower.Cost") +" "+ healthItem.Tdata.UpdatePrice.ToString());
            }
        }
    }

    private void HideShop()
    {
        shopState = Shop.activeSelf;
        Shop.SetActive(false);
        CloseShop.SetActive(false);
        OpenShop.SetActive(true);
        OpenShop.GetComponent<Button>().interactable = false;
    }

    private void ShowShop()
    {
        Shop.SetActive(shopState);
        CloseShop.SetActive(shopState);
        OpenShop.SetActive(!shopState);
        OpenShop.GetComponent<Button>().interactable = true;
    }
}
