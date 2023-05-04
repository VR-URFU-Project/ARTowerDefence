using UnityEngine;
using UnityEngine.UI;

public class TowerInteraction : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] GameObject InteractionPanelPrefab;

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
        if (!(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began || Input.GetMouseButtonDown(0))) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            var tower = hit.collider.gameObject;
            var healthItem = tower.GetComponent<TowerHealthLogic>();
            if (healthItem == null) return;

            HideShop();

            var askPanel = Instantiate(InteractionPanelPrefab);
            var askPanelLogic = askPanel.GetComponent<TowerInteractionPanelLogic>();
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

                return true;
            });

            askPanelLogic.SetNoAction(() => {
                CameraHandler.ChangeShopItemSelectedStage(false);
                ShowShop();
            });

            askPanelLogic.SetDeleteAction(() =>
            {
                CameraHandler.ChangeShopItemSelectedStage(false);
                MoneySystem.ChangeMoney(healthItem.Tdata.SellPrice);
                Destroy(tower);
                ShowShop();
            });

            if (healthItem.Tdata.UpdatePrice > MoneySystem.GetMoney())
            {
                askPanelLogic.SetText(LocalizationManager.Localize("UpgradeTower.NoMoney") + " " + healthItem.Tdata.UpdatePrice.ToString(), Color.red);
                askPanelLogic.DisableButton(TowerInterractionButton.Yes);
            }
            else
            {
                askPanelLogic.SetText(LocalizationManager.Localize("UpgradeTower.OK") + " " + (healthItem.Tdata.Level + 1).ToString()
                    + "\n" + LocalizationManager.Localize("UpgradeTower.Cost") + " " + healthItem.Tdata.UpdatePrice.ToString());
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
