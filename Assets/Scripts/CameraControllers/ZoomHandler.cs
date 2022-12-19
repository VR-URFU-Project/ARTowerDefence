using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ZoomHandler : MonoBehaviour
{
    [SerializeField]
    float Speed = 1f;

    private InputActions actions;
    private Coroutine zoomCoroutine;
    private Transform cameraTransform;

    [SerializeField] private Transform ClosePosition;
    [SerializeField] private Transform FarPosition;

    private Vector3 min;
    private Vector3 max;
    //private Vector3 Ypos_min;
    //private Vector3 Zpos_min;

    void Awake()
    {
        actions = new InputActions();
        cameraTransform = Camera.main.transform;
        min = ClosePosition.position;
        max = FarPosition.position;
    }

    void OnEnable()
    {
        actions.Enable();
    }

    void OnDisable()
    {
        actions.Disable();
    }

    void Start()
    {
        actions.Touch.SecondFingerContact.started += _ => ZoomStart();
        actions.Touch.SecondFingerContact.canceled += _ => ZoomEnd();
    }

    void ZoomStart()
    {
        zoomCoroutine = StartCoroutine(ZoomDetection());
    }

    void ZoomEnd()
    {
        StopCoroutine(zoomCoroutine);
    }

    IEnumerator ZoomDetection()
    {
        float previousDist = 0f, dist = 0f;

        while (true)
        {
            dist = Vector2.Distance(actions.Touch.FirstFingerPosition.ReadValue<Vector2>(),
                actions.Touch.SecondFingerPosition.ReadValue<Vector2>());

            // zooming out
            if (dist > previousDist)
            {
                //Vector3 targetPos = cameraTransform.position;
                //targetPos.z -= 1;
                //if (FarPosition.transform.forward >= transform.forward)
                Vector3 targetPos = cameraTransform.position + transform.forward;
                //targetPos = Mathf.Clamp((cameraTransform.position + transform.forward).z, min.normalized.z, max.normalized.z);
                cameraTransform.position =
                    //Vector3.Slerp(cameraTransform.position, targetPos, Time.deltaTime * Speed) ;
                    Vector2.MoveTowards(cameraTransform.position, targetPos, Time.deltaTime * Speed);
            }
            // zooming in
            else if (previousDist > dist)
            {
                //Vector3 targetPos = cameraTransform.position;
                //targetPos;
                Vector3 targetPos = cameraTransform.position + transform.forward;
                cameraTransform.position =
                    //Vector3.Slerp(cameraTransform.position, targetPos, Time.deltaTime * Speed);
                    Vector2.MoveTowards(cameraTransform.position, targetPos, Time.deltaTime * Speed);
            }

            // keep track of previous distance for the next loop
            previousDist = dist;
            yield return null;
        }
    }
}
