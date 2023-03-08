using TMPro;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour
{
    public string LocalizationKey;

    void Start()
    {
        Localize();
        LocalizationManager.LocalizationChanged += Localize;
    }

    public void OnDestroy()
    {
        LocalizationManager.LocalizationChanged -= Localize;
    }

    private void Localize()
    {
        if(GetComponent<Text>() == null)
            GetComponent<TMP_Text>().text = LocalizationManager.Localize(LocalizationKey);
        else
            GetComponent<Text>().text = LocalizationManager.Localize(LocalizationKey);
    }
}
