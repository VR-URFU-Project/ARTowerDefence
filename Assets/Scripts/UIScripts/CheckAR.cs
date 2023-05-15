using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class CheckAR : MonoBehaviour
{
    [SerializeField] private Button AR_button;
    //[SerializeField] private GameObject text;
    [SerializeField] private Sprite ripAR_sprite;
    [SerializeField] private GameObject arSession;

    private Sprite old_sprite;

    private void Awake()
    {
        DoCheck();
        arSession.SetActive(false);

    }

    private void DoCheck()
    {
        old_sprite = AR_button.image.sprite;
        try {
            ARSession.CheckAvailability();
            if (ARSession.state == ARSessionState.Unsupported || ARSession.state == ARSessionState.None)
            {
                AR_button.GetComponent<Button>().interactable = false;
                AR_button.image.sprite = ripAR_sprite;
            }
            else
                AR_button.GetComponent<Button>().interactable = true;
                    
        } 
        catch{
            AR_button.image.sprite = ripAR_sprite;
            AR_button.GetComponent<Button>().interactable = false;
        }
        

        
    }
}
