using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIToggle : MonoBehaviour
{

    private static InventoryUIToggle Instance { get; set; }

    public GameObject InventoryScreenUI;

    [SerializeField] private KeyCode _toggleInventoryBtn = KeyCode.I;
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
        InventoryScreenUI.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(_toggleInventoryBtn))
        {
            if (!InventoryScreenUI.activeSelf)
            {
                InventoryScreenUI.SetActive(true);
            }
            else
            {
                InventoryScreenUI.SetActive(false);
            }
        }
    }
}