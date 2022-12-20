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
            //Debug.Log("Hitted tower" + hit.collider.gameObject.name);
            var tower = hit.collider.gameObject;
            var healthItem = tower.GetComponent<TowerHealthLogic>();

            //mainCanvas.SetActive(false);
            var askPanel = Instantiate(YesNoPanel);
            GameObject.FindGameObjectWithTag("Yes").GetComponent<Button>().onClick.AddListener(() =>
            {
                MoneySystem.ChangeMoney(-healthItem.Tdata.UpdatePrice);
                healthItem.Tdata.Upgrade();
                var towerDatas = tower.GetComponentsInChildren<Canon>();
                foreach (var data in towerDatas)
                {
                    data.Tdata.Upgrade();
                }
                //mainCanvas.SetActive(true);
                Destroy(askPanel);
                //Debug.Log("yes");
            });
            GameObject.FindGameObjectWithTag("No").GetComponent<Button>().onClick.AddListener(() =>
            {
                //mainCanvas.SetActive(true);
                Destroy(askPanel);
                //Debug.Log("no");
            });

            if (healthItem.Tdata.UpdatePrice > MoneySystem.GetMoney())
            {
                GameObject.Find("Question").GetComponent<TMP_Text>().color = Color.red;
                GameObject.Find("Question").GetComponent<TMP_Text>().text = "Not enough money";
                GameObject.FindGameObjectWithTag("Yes").GetComponent<Button>().gameObject.SetActive(false);
            }
            else
            {
                GameObject.Find("Question").GetComponent<TMP_Text>().text = "Upgrade tower to level " + (healthItem.Tdata.Level + 1).ToString()
                    + "\nCost: " + healthItem.Tdata.UpdatePrice.ToString();
            }
        }
    }
}
