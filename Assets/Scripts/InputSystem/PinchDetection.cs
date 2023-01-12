using System.Collections;
using UnityEngine;
using Cinemachine;


public class PinchDetection : MonoBehaviour
{
    private ZoomActions actions;
    private Coroutine zoomCoroutine;
    public CinemachineFreeLook cineCamera;
    private CinemachineCameraOffset camOffset;
    
    [SerializeField] float zoomMin = 0f;
    [SerializeField] float zoomMax = 1.4f;
    [SerializeField] float zoomSpeed;

    private void Awake()
    {
        actions = new ZoomActions();
        camOffset = cineCamera.GetComponent<CinemachineCameraOffset>();
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void Start()
    {
        actions.Touch.SecondaryTouchContact.started += _ => ZoomStart();
        actions.Touch.SecondaryTouchContact.canceled += _ => ZoomEnd();
    }

    private void ZoomStart()
    {
        zoomCoroutine = StartCoroutine(ZoomDetection());
    }

    private void ZoomEnd()
    {
        StopCoroutine(zoomCoroutine);
    }

    IEnumerator ZoomDetection()
    {
        float prevDist = 0f, curDist = 0f;

        while (true)
        {
            curDist = Vector2.Distance(
                actions.Touch.PrimaryFingerPos.ReadValue<Vector2>(), 
                actions.Touch.SecondaryFingerPos.ReadValue<Vector2>());
            
            // detection
            //if (Vector2.Dot(primaryDelta, secondaryDelta) < -.9f)

            if (curDist > prevDist)
            { // zoom out
                Vector3 targetPos = camOffset.transform.position;
                targetPos.z = zoomMax;
                camOffset.transform.position = Vector3.Slerp(camOffset.transform.position, 
                                                            targetPos, 
                                                            Time.deltaTime * zoomSpeed);
            }
            else if (curDist < prevDist)
            { // zoom in
                Vector3 targetPos = camOffset.transform.position;
                targetPos.z = zoomMin;
                camOffset.transform.position = Vector3.Slerp(camOffset.transform.position,
                                                            targetPos,
                                                            Time.deltaTime * zoomSpeed);
            }

            // keep dist for the next loop
            prevDist = curDist;

            yield return null;
        }
    }
}
