using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//This is basically just a script that u can put to an Object, and itll be considered an Object that can be interacted with, hence the name InteractableObject.t.
public class InteractableObject : MonoBehaviour
{
    [field: SerializeField] public bool playerInRange { get; private set; } = false;
    [field: SerializeField] public string ItemName { get; private set; }
    private GameObject _itemPickUp;
    private TextMeshProUGUI _itemPickUpText;
    private HidingTheText _hidingTheText;
    private LayerMask _pickUpCheck;

    private void Start()
    {
        _itemPickUp = GameObject.Find("ItemPickUp");
        _itemPickUp.TryGetComponent(out _hidingTheText);
        _itemPickUpText = _itemPickUp.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        //Checks for 4 things
            //1. Left mouse button is pressed
            //2. Player is in range
            //3. Target is selected
            //4. The object is on the interactableObject layer
        if(Input.GetKeyDown(KeyCode.Mouse0) && playerInRange && SelectionManager.Instance.onTarget && gameObject.layer == LayerMask.NameToLayer("interactableObject"))
        {
            //If inventory isnt full
            if(!InventorySystem.Instance.CheckIfFull())
            {
                InventorySystem.Instance.AddItemToInventory(ItemName);
                Debug.Log("Picked up " + ItemName);
                _hidingTheText.HideText(ItemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory is full, cannot pick up " + ItemName);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            _itemPickUp.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            _itemPickUp.SetActive(false);
        }
    }
}

//TODO
    //The "next slot", is the next slot in the hierachy, not the actual next slot in the inventory, its an issue of the duplicating slots while making the UI
    //Pick up 2 items and youll see
    //First thought is to add a sorting alg at the start
    //No time to do that rn