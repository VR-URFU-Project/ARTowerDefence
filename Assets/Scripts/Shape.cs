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

    private bool mouseButtonDowned = false;
    private bool mouseButtonUped = false;
    private bool canPlaceTheTower = false;

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
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!mouseButtonUped && Physics.Raycast(ray, out hit))
        {
            gameObject.layer = 2;
            MaterialToChange_plane.GetComponent<Renderer>().material = GreenMatPlane;
            MaterialToChange_sphere.GetComponent<Renderer>().material = GreenMatSphere;
            if (hit.collider.gameObject.name == "Plane")
            {
                transform.position = hit.point;
                canPlaceTheTower = true;
            }
            else
            {
                canPlaceTheTower = false;
                transform.position = hit.point;
                MaterialToChange_plane.GetComponent<Renderer>().material = RedMatPlane;
                MaterialToChange_sphere.GetComponent<Renderer>().material = RedMatSphere;
            } 
        }

        if (canPlaceTheTower & Input.GetMouseButtonUp(0))
        {
            gameObject.layer = 0;
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

        if (Physics.Raycast(ray, out hit) && mouseButtonUped && Input.GetMouseButtonDown(0))
        {
            if (hit.collider.gameObject.GetComponent<Shape>())
            {
                mouseButtonUped = false;
            }
        }
    }
}
