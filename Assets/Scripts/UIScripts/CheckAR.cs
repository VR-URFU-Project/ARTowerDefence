using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using CI.QuickSave;
using System.Collections;

public class CheckAR : MonoBehaviour
{
    [SerializeField] private Button AR_button;
    [SerializeField] private Sprite ripAR_sprite;

    public int supportAR;

    IEnumerator Start()
    {

        if (ARSession.state == ARSessionState.None ||
            ARSession.state == ARSessionState.CheckingAvailability)
        {
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state == ARSessionState.Unsupported
            || ARSession.state == ARSessionState.NeedsInstall)
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

        var writer = QuickSaveWriter.Create("supportAR");
        writer.Write("supportAR", supportAR);
        writer.Commit();
    }
}