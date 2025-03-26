using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLinearMoving : MonoBehaviour
{
    [SerializeField] private GameObject[] points; //点的引用
    [SerializeField] private float speed = 5f; //平台的移动速度
    private int pointIndex = 0;
    private float waitTimeCounter = 0.5f;
    [SerializeField] private float waitTime = 0.5f;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            points[pointIndex].transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, points[pointIndex].transform.position) < 0.1f)
        {
            if (waitTimeCounter > 0)
            {
                waitTimeCounter -= Time.deltaTime;
                return;
            }
            
            pointIndex++;
            if (pointIndex >= points.Length)
            {
                pointIndex = 0;
            }
            waitTimeCounter = waitTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
