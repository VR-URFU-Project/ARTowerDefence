using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TowerInteractionPanelLogic : MonoBehaviour
{
    [Header("Panel Buttons")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button noButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button yesButton;

    [Header("Panel Texts")]
    [SerializeField] private TMP_Text questionPlace;
    [SerializeField] private TMP_Text upgradePricePlace;
    [SerializeField] private TMP_Text sellingPricePlace;

    [Header("Shop Parts")]
    [SerializeField] GameObject CloseShop;
    [SerializeField] GameObject OpenShop;
    [SerializeField] GameObject Shop;
    private bool shopState;

    private int counter = 0;
    private GameObject tower;
    private TowerData healthData;

    [Header("Sprites")]
    public Sprite maxSprite;
    public Sprite noMoneySprite;
    public Sprite normSprite;

    public void Activate(GameObject tower)
    {
        this.tower = tower;
        healthData = tower.GetComponent<TowerHealthLogic>().Tdata;

        DoMakeUp();

        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(() =>
        {
            //counter++;
            //if (counter != 1) return;
            MoneySystem.ChangeMoney(healthData.SellPrice);
            Destroy(tower);
            gameObject.SetActive(false);
        });

        deleteButton.onClick.RemoveAllListeners();
        deleteButton.onClick.AddListener(() => {
            SetQuestionText(LocalizationManager.Localize("Towers.ConfirmSelling"));
        });

        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(() => {
            //counter++;
            //if (counter != 1) return;
            gameObject.SetActive(false);
        });

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(() => {
            //counter++;
            //if (counter != 1) return;
            MoneySystem.ChangeMoney(-healthData.UpdatePrice);
            healthData.Upgrade();
            var canonData = tower.GetComponent<Canon>();
            canonData.Tdata.Upgrade();

            //Destroy(gameObject);
        });

        HideShop();
        gameObject.SetActive(true);
    }

    public void Update()
    {
        DoMakeUp();
    }

    public void OnDisable()
    {
        upgradeButton.interactable = true;
        upgradePricePlace.color = Color.black;
        questionPlace.color = Color.black;
        CameraHandler.ChangeShopItemSelectedStage(false);
        ShowShop();
    }

    private void DoMakeUp()
    {
        sellingPricePlace.text = healthData.SellPrice.ToString();
        if (!UpdateController.CanUpgradeTower(healthData.Type, healthData.Level))
        {
            upgradeButton.interactable = false;
            upgradePricePlace.transform.parent.gameObject.GetComponent<Image>().sprite = maxSprite;
            SetUpdatePriceText("", Color.black);
        }
        else if (healthData.UpdatePrice > MoneySystem.GetMoney())
        {
            upgradeButton.interactable = false;
            upgradePricePlace.transform.parent.gameObject.GetComponent<Image>().sprite = noMoneySprite;
            SetUpdatePriceText(healthData.UpdatePrice.ToString(), Color.red);
        }
        else
        {
            upgradeButton.interactable = true;
            upgradePricePlace.transform.parent.gameObject.GetComponent<Image>().sprite = normSprite;
            SetUpdatePriceText(healthData.UpdatePrice.ToString(), Color.black);
        }
    }

    private void SetUpdatePriceText(string text, Color color)
    {
        upgradePricePlace.color = color;
        upgradePricePlace.text = text;
    }

    private void SetQuestionText(string text)
    {
        questionPlace.text = text;
    }

    private void SetQuestionText(string text, Color color)
    {
        questionPlace.color = color;
        questionPlace.text = text;
    }

    private void HideShop()
    {
        shopState = Shop.activeSelf;
        Shop.SetActive(false);
        CloseShop.SetActive(false);
        OpenShop.SetActive(false);
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

public enum TowerInterractionButton
{
    Upgrade,
    Exit,
    Delete
}
