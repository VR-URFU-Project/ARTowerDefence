using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shape : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    [SerializeField] private GameObject originTower;
    private GameObject gamingPlace;
    [SerializeField] GameObject YesNoPanel;
    //[SerializeField] Button yes;
    //[SerializeField] Button no;

    private bool mouseButtonUped = false;

    //public static bool yes_pressed = false;
    //public static bool no_pressed = false;

    private Button YesButton;
    private Button NoButton;

    private GameObject mainCanvas;


    void Start()
    {
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        YesButton = GameObject.FindGameObjectWithTag("Yes").GetComponent<Button>();
        NoButton = GameObject.FindGameObjectWithTag("No").GetComponent<Button>();
        gamingPlace = GameObject.FindWithTag("GamingPlace");
        YesNoPanel.SetActive(false);
        mainCanvas.SetActive(false);
        //yes_pressed = false;
        //no_pressed = false;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!mouseButtonUped && Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "Plane")
                transform.position = hit.point;
        }

        if (Input.GetMouseButtonUp(0))
        {
            YesNoPanel.SetActive(true);
            mouseButtonUped = true;
            YesButton.onClick.AddListener(() =>
            {
                Instantiate(originTower, transform.position, transform.rotation, gamingPlace.transform);
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
