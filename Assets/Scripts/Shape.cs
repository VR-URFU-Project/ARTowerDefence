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

    private bool mouseButtonUped = false;

    public static bool yes_pressed = false;
    public static bool no_pressed = false;

    private Button YesButton;
    private Button NoButton;


    void Start()
    {
        YesButton = GameObject.FindGameObjectWithTag("Yes").GetComponent<Button>();
        NoButton = GameObject.FindGameObjectWithTag("No").GetComponent<Button>();
        gamingPlace = GameObject.FindWithTag("GamingPlace");
        YesNoPanel.SetActive(false);
        yes_pressed = false;
        no_pressed = false;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!mouseButtonUped)
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Plane")
                    transform.position = hit.point;
            }

        if (Input.GetMouseButtonUp(0))
        {
            YesNoPanel.SetActive(true);
            mouseButtonUped = true;
        }

        if (yes_pressed)
        {
            Instantiate(originTower, transform.position, transform.rotation, gamingPlace.transform);
            yes_pressed = false;
            Destroy(gameObject);
        }
        if (no_pressed)
        {
            no_pressed = false;
            Destroy(gameObject);
            
        }
    }


}
