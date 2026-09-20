using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }

    public GameObject InventoryScreenUI;

    public List<GameObject> slotList = new List<GameObject>();

    public List<string> itemList = new List<string>();

    private GameObject _itemToAdd;

    private GameObject _slotToAdd;

    //private bool _isFull;

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

    private void Start()
    {
        InventoryScreenUI.SetActive(false);
        //isFull = false;
        AddToSlotList();
    }

    private void AddToSlotList()
    {
        foreach(Transform child in InventoryScreenUI.transform)
        {
            if(child.CompareTag("Slots"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(_toggleInventoryBtn))
        {
            if (!InventoryScreenUI.activeSelf)
            {
                InventoryScreenUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                InventoryScreenUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void AddItemToInventory(string itemName)
    {
            _slotToAdd = FindNextEmptySlot();
            _itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), _slotToAdd.transform.position, _slotToAdd.transform.rotation);
            _itemToAdd.transform.SetParent(_slotToAdd.transform);

            itemList.Add(itemName);
    }

    public bool CheckIfFull()
    {
        int counter = 0;

        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                counter+=1;
            }
        }

        if(counter == slotList.Count)
        {
                return true;
        }
        else
        {
        return false;
        }
    }

    public GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount == 0)
            {
                return slot;
            }
        }

        return new GameObject();
    }

}