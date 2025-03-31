using System;
using TMPro;
using UnityEngine;

public class TextReminder : IReminder
{
    public string reminderText;
    
    private TMP_Text textComponent;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        GetComponent<Collider2D>().isTrigger = true;
        textComponent.text = reminderText;
        HideReminder();
    }

    protected override void ShowReminder()
    {
        textComponent.enabled = true;
    }

    protected override void HideReminder()
    {
        textComponent.enabled = false;
    }
}