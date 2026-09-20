using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
public class ObjectCanBePickedUp : MonoBehaviour
{
    [field: SerializeField] public bool playerInRange { get; private set; } = false;
    [field: SerializeField] public string ItemName { get; private set; }
    private GameObject _itemPickUp;
    private TextMeshProUGUI _itemPickUpText;
    private HidingTheText _hidingTheText;

    private void Start()
    {
        _itemPickUp = GameObject.Find("ItemPickUp");
        _itemPickUp.TryGetComponent(out _hidingTheText);
        _itemPickUpText = _itemPickUp.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && playerInRange && SelectionManager.Instance.onTarget)
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
            //_itemPickUpText.text = ItemName;
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