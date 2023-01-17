using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class CheckAR : MonoBehaviour
{
    [SerializeField] private Button AR_button;
    [SerializeField] private GameObject text;

    private void Awake()
    {
        ARSession.CheckAvailability();

        if (ARSession.state == ARSessionState.Unsupported)
        {
            if (text != null)
                text.SetActive(true);

            if (AR_button != null)
                AR_button.GetComponent<Button>().interactable = false;
        }
        else
            if (AR_button != null)
                AR_button.GetComponent<Button>().interactable = true;
    }
}
