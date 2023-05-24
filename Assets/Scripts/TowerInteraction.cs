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

    private bool mouseButtonUped = false;
    private bool mouseButtonDowned = false;
    public CinemachineFreeLook cinemachineFreeLook;

    void Start()
    {
        layerMask = LayerMask.GetMask("Tower");
    }

    void Update()
    {
        //if (!(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began || Input.GetMouseButtonDown(0))) return;
        if (!(Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Input.GetMouseButtonUp(0)) mouseButtonUped = true;
        //else mouseButtonUped = false;

        //if (Input.GetMouseButtonDown(0)) mouseButtonDowned = true;
        //else mouseButtonDowned = false;

        //if (!(
        //    (/*cinemachineFreeLook.m_XAxis.m_InputAxisValue == 0 && mouseButtonUped &&*/ 
        //    Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        //    //|| (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        //    )) return;

        /*else*/ if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
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

    private bool ifMouseButtonClickedTheSameTower(Ray ray, GameObject tempGameObject)
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            var tower = hit.collider.gameObject;
            var healthItem = tower.GetComponent<TowerHealthLogic>();
            if (healthItem != null)
            {
                if (tempGameObject == null) tempGameObject = tower;
                else if (tower == tempGameObject)
                {
                    tempGameObject = null;
                    return true;
                }
            }
        }
        return false;
    }
}
