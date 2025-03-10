using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateBar : MonoBehaviour
{
    [Header("照片对象")]
    public Image HealthBar;
    public Image ManaBar;

    public void ChangeHealth(float percentage)
    {
        HealthBar.fillAmount = percentage;
    }
    public void ChangeMana(float percentage)
    {
        ManaBar.fillAmount = percentage;
    }
}
