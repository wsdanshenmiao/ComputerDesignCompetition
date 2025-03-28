using System;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public SavePointSO savePointData;

    private void Update()
    {
        savePointData.pos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")) {
            SaveAtCheckpoint();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            SaveAtCheckpoint();
        }
    }

    private void SaveAtCheckpoint()//存档
    {
        //Debug.Log("Saving at savepoint");
        savePointData.isActivated = true;
        GameManager.Instance.UpdateLastCheckpointId(savePointData.savePointId);
    }

}

