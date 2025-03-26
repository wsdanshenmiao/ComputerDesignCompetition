using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotationMoving : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f;

    void Update()
    {
        transform.RotateAround(transform.parent.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
