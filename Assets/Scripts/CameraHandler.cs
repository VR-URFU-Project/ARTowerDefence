using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

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
    CinemachineFreeLook freeLookCam;
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
    private static bool shopItem_selected;
    bool zoom_activated;
    float X_speed;

    private void Start()
    {
        freeLookCam = virtualCamera.GetComponent<CinemachineFreeLook>();
        camOffset = virtualCamera.GetComponent<CinemachineCameraOffset>();
        shopItem_selected = false;
        zoom_activated = false;
        X_speed = freeLookCam.m_XAxis.m_MaxSpeed;
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
        if (shape != null || gamePause_enabled || settingsMenu_enabled || shopItem_selected)
        {
            virtualCamera.SetActive(false);
        }
        else
        {
            virtualCamera.SetActive(true);
        }

        if (zoom_activated)
        {
            freeLookCam.m_XAxis.m_MaxSpeed = 0;//X_speed / 2;
        }
        else
        {
            freeLookCam.m_XAxis.m_MaxSpeed = X_speed;
        }
    }

    private void AndroidZoom()
    {
        if (Input.touchCount == 2)
        {
            zoom_activated = true;
            //virtualCamera.GetComponent<CinemachineFreeLook>().enabled = false;
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
        else { zoom_activated = false; }
        //virtualCamera.GetComponent<CinemachineFreeLook>().enabled = true;        
    }

    public static void ChangeShopItemSelectedStage(bool B)
    {
        shopItem_selected = B;
    }
}
