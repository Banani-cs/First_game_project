using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;

    [SerializeField] private KeyCode toggleInventoryBtn = KeyCode.I;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        inventoryScreenUI.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(toggleInventoryBtn))
        {
            if (!inventoryScreenUI.activeSelf)
            {
                inventoryScreenUI.SetActive(true);
            }
            else
            {
                inventoryScreenUI.SetActive(false);
            }
        }
    }
}