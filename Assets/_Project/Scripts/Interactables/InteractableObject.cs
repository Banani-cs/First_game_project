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
    [SerializeField] private Layer _interactableObject;

    private void Start()
    {
        _itemPickUp = GameObject.Find("ItemPickUp");
        _itemPickUp.TryGetComponent(out _hidingTheText);
        _itemPickUpText = _itemPickUp.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && playerInRange && SelectionManager.Instance.onTarget && _interactablesObject)
        {
            Debug.Log("Picked up " + ItemName);

            _hidingTheText.HideText(ItemName);
            Destroy(gameObject);
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