using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookOnEnemy : MonoBehaviour
{

    public Transform partToRotate;
    public float turnSpeed = 5f;
    public Canon canonScript;

    private Transform target = null;

    void Update()
    {
        target = canonScript.GetTarget();
        LookOnTarget();
    }

    private void LookOnTarget()
    {
        if (target == null) return;
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotationVector = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotationVector.y, 0f);
    }
}
