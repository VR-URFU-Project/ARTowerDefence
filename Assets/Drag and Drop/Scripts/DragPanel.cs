using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPanel : MonoBehaviour
{
    public int Count;

    [Tooltip("—сылка на префаб перемещаемого элемента")]
    [SerializeField] private GameObject dragObjectPrefab;

    [Tooltip("—сылка на Content внутри ScrollView")]
    [SerializeField] private Transform scrollViewContent;

    private void Start()
    {
        for (int i = 0; i < Count; i++)
        {
            var dragObj = Instantiate(dragObjectPrefab, scrollViewContent);
            var script = dragObj.GetComponent<DragElement>();

            script.DefaultParentTransform = scrollViewContent;
            script.DragParentTransform = transform.parent;
            script.SiblingIndex = i;
        }
    }
}
