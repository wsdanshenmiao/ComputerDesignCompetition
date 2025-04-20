using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    private Transform player = null;
    [Header("按顺序移动的点")]
    public Vector3[] movePoints;

    public float moveSpeed = 1;
    public float stopDistance = .2f;
    public float waitTime = .5f;
    private float currWaitTime = 0;
    private uint currIndex = 0;

    private void Start()
    {
        currIndex = 0;
        currWaitTime = waitTime;
    }

    private void Update()
    {
        MoveToNextPoint();
    }

    private void OnDrawGizmosSelected()
    {
        foreach(var point in movePoints){
            Gizmos.DrawWireSphere(point, 1f);
        }
    }

    private void MoveToNextPoint()
    {
        if (movePoints.Length <= 0) return;
        
        float moveStep = moveSpeed * Time.deltaTime;
        Vector3 prePos = transform.position;
        if(Vector3.Distance(transform.position, movePoints[currIndex]) < stopDistance){
            // 等待一定时间
            if(currWaitTime < 0){
                currIndex = (uint)((currIndex + 1) % movePoints.Length);
                currWaitTime = waitTime;
            }
            else{
                currWaitTime -= Time.deltaTime;
            }
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, movePoints[currIndex], moveStep);
        }
        if(player){
            player.position += transform.position - prePos;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player"){
            player = coll.GetComponent<Transform>();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player"){
            player= null;
        }
    }
}
