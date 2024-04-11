using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationhealthBar : MonoBehaviour
{
    private Transform _target;

    private void Awake()
    {
        _target = FindObjectOfType<Camera>().gameObject.transform;
    }

    private void Update()
    {
        LockOnTarget();
    }

    private void LockOnTarget()
    {
        Vector3 dir = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 1000000).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }
}
