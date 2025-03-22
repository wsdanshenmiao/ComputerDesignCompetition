using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyntheticUIManager : MonoBehaviour
{
    private Transform craftingSystemUI;
    private Transform reminderUI;

    private void Awake()
    {
        craftingSystemUI = transform.Find("CraftingSystemUI");
        reminderUI = transform.Find("QASystemUI");
    }

    public void CloseCraftingSystemUI()
    {
        craftingSystemUI.gameObject.SetActive(false);
    }

    public void OpenCraftingSystemUI()
    {
        craftingSystemUI.gameObject.SetActive(true);
    }

    public void CloseReminderUI()
    {
        reminderUI.gameObject.SetActive(false);
    }

    public void OpenReminderUI()
    {
        reminderUI.gameObject.SetActive(true);
    }
}
