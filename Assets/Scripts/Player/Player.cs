using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Inventory inventory;

    private void Awake()
    {
        inventory = new Inventory();
    }
}