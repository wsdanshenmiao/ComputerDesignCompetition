using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class IReminder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            ShowReminder();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) {
            ShowReminder();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            HideReminder();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) {
            HideReminder();
        }
    }

    abstract protected void ShowReminder();
    abstract protected void HideReminder();
}
