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
    private int towersNearby = 0;
    private int layerMask;

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
        GameObject.Find("Question").SetActive(false);
        NoButton = GameObject.FindGameObjectWithTag("No").GetComponent<Button>();
        gamingPlace = GameObject.FindWithTag("GamingPlace");
        YesNoPanel.SetActive(false);
        mainCanvas.SetActive(false);
        layerMask = LayerMask.GetMask("Surface");
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!mouseButtonUped && Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            gameObject.layer = 2;
            MaterialToChange_plane.GetComponent<Renderer>().material = GreenMatPlane;
            MaterialToChange_sphere.GetComponent<Renderer>().material = GreenMatSphere;
            transform.position = hit.point;
            if (hit.collider.gameObject.name == "Plane" && towersNearby == 0)
            {
                canPlaceTheTower = true;
            }
            else
            {
                canPlaceTheTower = false;
                MaterialToChange_plane.GetComponent<Renderer>().material = RedMatPlane;
                MaterialToChange_sphere.GetComponent<Renderer>().material = RedMatSphere;
            } 
        }

        if (Input.GetMouseButtonUp(0))
        {
            gameObject.layer = 0;
            YesNoPanel.SetActive(true);
            mouseButtonUped = true;
            YesButton.onClick.AddListener(() =>
            {
                if (canPlaceTheTower)
                {
                    shop.CreateTower(transform, gamingPlace.transform);
                    mainCanvas.SetActive(true);
                    Destroy(gameObject);
                }
                //Debug.Log("yes");
            });
            NoButton.onClick.AddListener(() =>
            {
                mainCanvas.SetActive(true);
                Destroy(gameObject);
                //Debug.Log("no");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TowerHealthLogic>() == null) return;
        towersNearby++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TowerHealthLogic>() == null) return;
        towersNearby--;
    }
}
