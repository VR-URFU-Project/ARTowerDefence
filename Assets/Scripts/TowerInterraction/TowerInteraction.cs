using System;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class TowerInteraction : MonoBehaviour
{
    RaycastHit hit;

    [SerializeField]
    private TowerInteractionPanelLogic panelLogic;

    private int layerMask;
    private GameObject curTower = null;

    void Start()
    {
        layerMask = LayerMask.GetMask("Tower");
    }

    void Update()
    {
        if (!(Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            var tower = hit.collider.gameObject;
            if (tower.GetComponent<Canon>() == null)
            {
                curTower = null;
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                curTower = tower;
            }
            else
            {
                if(curTower!=null && curTower == tower)
                    panelLogic.Activate(tower);
                else
                    curTower = null;
            }
        }
        else
        {
            curTower = null;
        }     
    }
}
