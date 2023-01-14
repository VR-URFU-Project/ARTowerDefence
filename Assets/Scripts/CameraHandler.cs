using UnityEngine;
using Cinemachine;
//using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

public class CameraHandler : MonoBehaviour
{
    [Header("GeneralSettings")]
    [SerializeField] GameObject virtualCamera;
    [SerializeField] string shopTag;
    [SerializeField] string shapeTag;    

    GameObject shop;
    GameObject shape;

    [Header("ZoomSettings")]
    //float fieldOfView;
    CinemachineCameraOffset camOffset;
    [SerializeField] float zoomMin;
    [SerializeField] float zoomMax;
    [SerializeField] float zoomSpeed;
    float zoom;
    Touch touch1;
    Touch touch2;
    Vector2 touch1_direction;
    Vector2 touch2_direction;
    float distanceBetweenPositions;
    float distanceBetweenDirections;

    [Header("Menus")]
    [SerializeField] GameObject gamePause;
    [SerializeField] GameObject settingsMenu;

    bool gamePause_enabled;
    bool settingsMenu_enabled;

    private void Start()
    {
        camOffset = virtualCamera.GetComponent<CinemachineCameraOffset>();
        //fieldOfView = virtualCamera.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView;
        //fieldOfView = 40f;
    }

    private void Update()
    {
        CheckCameraCanMove();

        AndroidZoom();
    }
    private void CheckCameraCanMove()
    {
        gamePause_enabled = gamePause.activeSelf;
        settingsMenu_enabled = settingsMenu.activeSelf;
        shop = GameObject.FindGameObjectWithTag(shopTag);
        shape = GameObject.FindGameObjectWithTag(shapeTag);
        if (shop != null || shape != null || gamePause_enabled || settingsMenu_enabled)
        {
            virtualCamera.SetActive(false);
        }
        else
        {
            virtualCamera.SetActive(true);
        }
    }

    private void AndroidZoom()
    {
        if (Input.touchCount == 2)
        {
            virtualCamera.GetComponent<CinemachineFreeLook>().enabled = false;
            touch1 = Input.GetTouch(0);
            touch2 = Input.GetTouch(1);
            touch1_direction = touch1.position - touch1.deltaPosition;
            touch2_direction = touch2.position - touch2.deltaPosition;

            distanceBetweenPositions = Vector2.Distance(touch1.position, touch2.position);
            distanceBetweenDirections = Vector2.Distance(touch1_direction, touch2_direction);

            zoom = distanceBetweenDirections - distanceBetweenPositions;

            var curZoom = camOffset.m_Offset.z - zoom * zoomSpeed;
            camOffset.m_Offset.z = Mathf.Clamp(curZoom, zoomMin, zoomMax);

            //var currentZoom = fieldOfView - zoom * sensivity;
            //fieldOfView = Mathf.Clamp(currentZoom, zoomMin, zoomMax);
        }
        virtualCamera.GetComponent<CinemachineFreeLook>().enabled = true;        
    }
}
