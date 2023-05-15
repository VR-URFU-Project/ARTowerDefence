using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using CI.QuickSave;

public class CheckAR : MonoBehaviour
{
    [SerializeField] private Button AR_button;
    //[SerializeField] private GameObject text;
    [SerializeField] private Sprite ripAR_sprite;
    [SerializeField] private GameObject arSession;

    private Sprite old_sprite;

    public int supportAR;

    private void Awake()
    {
        DoCheck();
        //arSession.SetActive(false);
        var writer = QuickSaveWriter.Create("supportAR");
        writer.Write("supportAR", supportAR);
        writer.Commit();
    }

    private void DoCheck()
    {
        old_sprite = AR_button.image.sprite;
        try {
            ARSession.CheckAvailability();
            if (ARSession.state == ARSessionState.Unsupported || ARSession.state == ARSessionState.None)
            {
                supportAR = 0;
                AR_button.GetComponent<Button>().interactable = false;
                AR_button.image.sprite = ripAR_sprite;
            }
            else
            {
                AR_button.GetComponent<Button>().interactable = true;
                supportAR = 1;
            }
                    
        } 
        catch{
            AR_button.image.sprite = ripAR_sprite;
            AR_button.GetComponent<Button>().interactable = false;
            supportAR = 0;
        }
        

        
    }
}
