using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shape : MonoBehaviour
{
    RaycastHit hit;
    //[SerializeField] private GameObject originTower;
    private GameObject gamingPlace;
    [SerializeField] GameObject YesNoPanel;

    private bool mouseButtonUped = false;

    private Button YesButton;
    private Button NoButton;

    private Shop shop;

    private GameObject mainCanvas;


    [Header("Materials")]
    [SerializeField] private Material GreenMatPlane;
    [SerializeField] private Material GreenMatSphere;

    [SerializeField] private Material RedMatPlane;
    [SerializeField] private Material RedMatSphere;

    [SerializeField] private GameObject MaterialToChange_plane;
    [SerializeField] private GameObject MaterialToChange_sphere;

    void Start()
    {
        shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>();
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        YesButton = GameObject.FindGameObjectWithTag("Yes").GetComponent<Button>();
        NoButton = GameObject.FindGameObjectWithTag("No").GetComponent<Button>();
        gamingPlace = GameObject.FindWithTag("GamingPlace");
        YesNoPanel.SetActive(false);
        mainCanvas.SetActive(false);

        //MaterialToChange_plane = gameObject.GetComponent<Renderer>().material;
        //MaterialToChange_sphere = gameObject.GetComponent<Renderer>().material;

        //this_mat_plane = gameObject.FindC<Renderer>().material;
        //this_mat_sphere = gameObject.GetComponentInChildren<Renderer>().material;

        //Green = new Color(0, 255, 20, 0.5f);
        //Red = new Color(255, 3, 0, 0.5f);
        //Green.r = 0f; Green.g = 255f; Green.b = 20f; Green.a = 0;
        //Red.r = 255f; Red.g = 2f; Red.b = 0f; Red.a = 0;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!mouseButtonUped && Physics.Raycast(ray, out hit))
        {
            MaterialToChange_plane.GetComponent<Renderer>().material = GreenMatPlane;
            MaterialToChange_sphere.GetComponent<Renderer>().material = GreenMatSphere;
            if (hit.collider.gameObject.name == "Plane")
                transform.position = hit.point;
            else
            {
                transform.position = hit.point;
                MaterialToChange_plane.GetComponent<Renderer>().material = RedMatPlane;
                MaterialToChange_sphere.GetComponent<Renderer>().material = RedMatSphere;
            }
        }
        else
        {
            if (!mouseButtonUped)
            {
                MaterialToChange_plane.GetComponent<Renderer>().material = RedMatPlane;
                MaterialToChange_sphere.GetComponent<Renderer>().material = RedMatSphere;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            YesNoPanel.SetActive(true);
            mouseButtonUped = true;
            YesButton.onClick.AddListener(() =>
            {
                shop.CreateTower(transform, gamingPlace.transform);
                mainCanvas.SetActive(true);
                Destroy(gameObject);
                Debug.Log("yes");
            });
            NoButton.onClick.AddListener(() =>
            {
                mainCanvas.SetActive(true);
                Destroy(gameObject);
                Debug.Log("no");
            });
        }
    }
}
