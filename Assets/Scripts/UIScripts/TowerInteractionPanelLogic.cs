using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TowerInteractionPanelLogic : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button noButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private TMP_Text questionPlace;
    [SerializeField] private TMP_Text upgradePricePlace;
    [SerializeField] private TMP_Text sellingPricePlace;

    private int counter = 0;

    public Sprite maxSprite;
    public Sprite noMoneySprite;
    public Sprite normSprite;

    // private void OnEnable()
    //{
    //noButton.onClick.AddListener(() => { Destroy(gameObject); });
    //}

    public void ChangeSprite(string str)
    {
        switch (str)
        {
            case "max":
                upgradePricePlace.transform.parent.gameObject.GetComponent<Image>().sprite = maxSprite;
                break;

            case "noMoney":
                upgradePricePlace.transform.parent.gameObject.GetComponent<Image>().sprite = noMoneySprite;
                break;

            case "norm":
                upgradePricePlace.transform.parent.gameObject.GetComponent<Image>().sprite = normSprite;
                break;
        }
    }

    public void SetSellingPriceText(string textPrice)
    {
        sellingPricePlace.text = textPrice;
    }

    public void SetUpgradePriceInText(string textPrice)
    {
        upgradePricePlace.text = textPrice;
    }

    public void SetText(string text)
    {
        questionPlace.text = text;
    }

    public void SetText(string text, Color color)
    {
        questionPlace.color = color;
        questionPlace.text = text;
    }

    public void SetYesAction(Func<bool> toExec)
    {
        upgradeButton.onClick.AddListener(() => {
            counter++;
            if (counter != 1) return;
            if (!toExec())
            {
                counter = 0;
                return;
            }
            Destroy(gameObject);
        });
    }

    public void SetNoAction(Action toExec)
    {
        noButton.onClick.AddListener(() => {
            counter++;
            if (counter != 1) return;
            toExec();
            Destroy(gameObject);
        });
    }

    public void SetDeleteAction(Action toExec)
    {
        deleteButton.onClick.AddListener(() => {
                SetText(LocalizationManager.Localize("Towers.ConfirmSelling"));
                
        });

        yesButton.onClick.AddListener(() =>
        {
            counter++;
            if (counter != 1) return;
            toExec();
            Destroy(gameObject);
        });
    }

    public void DisableButton(TowerInterractionButton button)
    {
        switch (button)
        {
            case TowerInterractionButton.Yes:
                upgradeButton.interactable = false;
                break;
            case TowerInterractionButton.No:
                noButton.interactable = false;
                break;
            case TowerInterractionButton.Delete:
                deleteButton.interactable = false;
                break;
        }
    }
}

public enum TowerInterractionButton
{
    Yes,
    No,
    Delete
}
