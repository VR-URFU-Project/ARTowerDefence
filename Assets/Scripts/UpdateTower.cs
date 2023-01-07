using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTower : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] GameObject YesNoPanel;

    private int layerMask;

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
        //Debug.Log("entered Hitted tower");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            
            var tower = hit.collider.gameObject;
            var healthItem = tower.GetComponent<TowerHealthLogic>();
            if (healthItem == null) return;

            var askPanel = Instantiate(YesNoPanel);
            var askPanelLogic = askPanel.GetComponent<YesNoPanelLogic>();
            askPanel.GetComponentInChildren<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            askPanelLogic.SetYesAction(() =>
            {
                MoneySystem.ChangeMoney(-healthItem.Tdata.UpdatePrice);
                healthItem.Tdata.Upgrade();
                var towerDatas = tower.GetComponentsInChildren<Canon>();
                foreach (var data in towerDatas)
                {
                    data.Tdata.Upgrade();
                }
                return true;
            });

            if (healthItem.Tdata.UpdatePrice > MoneySystem.GetMoney())
            {
                askPanelLogic.SetText("Not enough money", Color.red);
                GameObject.FindGameObjectWithTag("Yes").GetComponent<Button>().interactable = false;
            }
            else
            {
                askPanelLogic.SetText("Upgrade tower to level " + (healthItem.Tdata.Level + 1).ToString()
                    + "\nCost: " + healthItem.Tdata.UpdatePrice.ToString());
            }
        }
    }
}
