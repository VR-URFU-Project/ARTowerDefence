using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        layerMask = LayerMask.GetMask("Tower");
    }

    void Update()
    {
        //if (!(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began || Input.GetMouseButtonDown(0))) return;
        if (!(
            Input.GetMouseButtonUp(0) || 
            (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            )) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
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
}
