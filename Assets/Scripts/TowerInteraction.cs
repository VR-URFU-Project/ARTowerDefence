using System;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class TowerInteraction : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] GameObject InteractionPanelPrefab;

    [SerializeField]
    private TowerInteractionPanelLogic panelLogic;

    [SerializeField] GameObject CloseShop;
    [SerializeField] GameObject OpenShop;
    [SerializeField] GameObject Shop;
    private bool shopState;

    private int layerMask;

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonUp(0)) mouseButtonUped = true;
        else mouseButtonUped = false;

        if (Input.GetMouseButtonDown(0)) mouseButtonDowned = true;
        else mouseButtonDowned = false;

        if (!(
            (/*cinemachineFreeLook.m_XAxis.m_InputAxisValue == 0 && mouseButtonUped &&*/ 
            Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
            //|| (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            )) return;

        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            var tower = hit.collider.gameObject;
            var healthItem = tower.GetComponent<TowerHealthLogic>();
            if (healthItem == null) return;

            //HideShop();

            //var askPanel = Instantiate(InteractionPanelPrefab);
            //var askPanelLogic = askPanel.GetComponent<TowerInteractionPanelLogic>();
            //askPanel.GetComponentInChildren<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            //askPanel.GetComponentInChildren<Canvas>().planeDistance = 0.19f;

            //InteractionPanel.SetActive(true);
            panelLogic.Activate(tower);

            
            //askPanelLogic.SetSellingPriceText(healthItem.Tdata.SellPrice.ToString()); 
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
