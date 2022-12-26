using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [Header("GeneralSettings")]
    [SerializeField] GameObject virtualCamera;
    [SerializeField] string shopTag;
    [SerializeField] string shapeTag;

    GameObject shop;
    GameObject shape;

    [Header("ZoomSettings")]
    float fieldOfView;
    [SerializeField] float zoomMin;
    [SerializeField] float zoomMax;
    [SerializeField] float sensivity;
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
        fieldOfView = virtualCamera.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView;
        fieldOfView = 40f;
    }

    private void Update()
    {
        CheckCameraCanMove();

#if UNITY_ANDROID
        AndroidZoom();
#endif
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
        if(Input.touchCount == 2)
        {
            touch1 = Input.GetTouch(0);
            touch2 = Input.GetTouch(1);
            touch1_direction = touch1.position - touch1.deltaPosition;
            touch2_direction = touch2.position - touch2.deltaPosition;

            distanceBetweenPositions = Vector2.Distance(touch1.position, touch2.position);
            distanceBetweenDirections = Vector2.Distance(touch1_direction, touch2_direction);

            zoom = distanceBetweenPositions - distanceBetweenDirections;

            var currentZoom = fieldOfView - zoom * sensivity;
            fieldOfView = Mathf.Clamp(currentZoom, zoomMin, zoomMax);
        }
    }
}
