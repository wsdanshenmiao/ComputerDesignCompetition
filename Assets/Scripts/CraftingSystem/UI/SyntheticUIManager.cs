using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyntheticUIManager : MonoBehaviour
{
    public enum ReminderMode : int
    {
        ChineseMagicMirror, Compass, Gunpowder, NumReminderMode
    }
    
    [SerializeField] private Transform craftingSystemUI;
    [SerializeField] private Transform[] reminderUI;
    private ReminderMode currReminderMode = ReminderMode.ChineseMagicMirror;
    
    public ReminderMode CurrReminderMode { get { return currReminderMode; } set { currReminderMode = value; } }

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
        reminderUI[(int)currReminderMode].gameObject.SetActive(false);
    }

    public void OpenReminderUI()
    {
        reminderUI[(int)currReminderMode].gameObject.SetActive(true);
    }
}
