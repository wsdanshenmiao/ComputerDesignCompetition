using System;
using UnityEngine;

public class EnterSceneDialog : MonoBehaviour
{
    [SerializeField] private int dialogIndex;
    
    // 进入场景后立马开始剧情
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            DialogManager.Instance.OpenDialog(dialogIndex);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            DialogManager.Instance.OpenDialog(dialogIndex);
            Destroy(gameObject);
        }
    }
}