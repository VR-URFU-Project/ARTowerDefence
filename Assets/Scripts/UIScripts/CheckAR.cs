using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class CheckAR : MonoBehaviour
{
    [SerializeField] private Button AR_button;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject arSession;

    private void Awake()
    {
        DoCheck();
        arSession.SetActive(false);
    }

    private void DoCheck()
    {
        text.SetActive(false);

        try {
            ARSession.CheckAvailability();
            if (ARSession.state == ARSessionState.Unsupported || ARSession.state == ARSessionState.None)
            {
                AR_button.GetComponent<Button>().interactable = false;
                text.SetActive(true);
            }
            else
                AR_button.GetComponent<Button>().interactable = true;
                    
        } 
        catch{
            text.SetActive(false);
            AR_button.GetComponent<Button>().interactable = false;
        }
        

        
    }
}
