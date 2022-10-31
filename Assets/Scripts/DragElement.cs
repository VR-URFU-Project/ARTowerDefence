using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler //, IPointerDownHandler, IPointerUpHandler
{
    
    [SerializeField] private GameObject towerPrefab;
    //[SerializeField] private GameObject holoTower;
    private RectTransform rect;
    private Canvas parentCanvas;
    private GameObject gamingPlace;

    //  нужны для костылей ниже
    //private bool ifTowerInstantiate = false;
    //private bool ifTowerBuilt = false;

    //private CanvasGroup canvasGroup;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        parentCanvas = transform.parent.parent.parent.parent.gameObject.GetComponent<Canvas>();
        gamingPlace = GameObject.FindWithTag("GamingPlace");
    }

    private Transform defaultParentTransform;
    /// <summary>
    /// Трансформ объекта, к которому он прикрепляется во время перетаскивания
    /// </summary>
    public Transform DefaultParentTransform
    {
        get { return defaultParentTransform; }
        set
        {
            if (value != null)
                defaultParentTransform = value;
        }
    }

    private Transform dragParentTransform;
    /// <summary>
    /// Трансформ объекта, к которому он прикрепляется во время перетаскивания
    /// </summary>
    public Transform DragParentTransform
    {
        get { return dragParentTransform; }
        set
        {
            if (value != null)
                dragParentTransform = value;
        }
    }

    private int siblingIndex;
    /// <summary>
    /// Номер индекса внутри родительского элемента
    /// </summary>
    public int SiblingIndex
    {
        get { return siblingIndex; }
        set
        {
            if (value > 0)
                siblingIndex = value;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(DragParentTransform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / parentCanvas.scaleFactor;//Camera.main.(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        //Debug.Log($"X: {transform.localPosition.x}\tY: {transform.localPosition.y}\tZ: {transform.localPosition.z}");

        //  дальше костыли, простите
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        //if (Physics.Raycast(ray, out hit))
        //{
        //    if (hit.collider.gameObject.name == "Plane")
        //    {
        //        transform.SetParent(DefaultParentTransform);
        //        transform.SetSiblingIndex(SiblingIndex);
        //        if (!ifTowerInstantiate)
        //        {
        //            Instantiate(holoTower, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity, gamingPlace.transform);
        //            ifTowerInstantiate = true;
        //        }
        //        if (ifTowerInstantiate && !ifTowerBuilt) { 
        //            holoTower.transform.Translate(new Vector3(hit.point.x, hit.point.y, hit.point.z), Space.World);
        //        }
        //    }
        //}
    }

    public void OnEndDrag(PointerEventData eventData)
    { 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "Plane")
            {
                transform.SetParent(DefaultParentTransform);
                transform.SetSiblingIndex(SiblingIndex);
                //Debug.Log($"X: {hit.point.x}, Y: {hit.point.y}, Z:{hit.point.z}");
                Instantiate(towerPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity, gamingPlace.transform);
                Destroy(gameObject);
            }
            else
            {
                transform.SetParent(DefaultParentTransform);
                transform.SetSiblingIndex(0);
            }
        }
        else
        {
            transform.SetParent(DefaultParentTransform);
            transform.SetSiblingIndex(0);
        }
    }
}
