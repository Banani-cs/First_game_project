using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
public class ObjectCanBePickedUp : MonoBehaviour
{
    private bool _playerInRange;
    [field: SerializeField] public string ItemName { get; private set; }
    private GameObject _itemPickUp;
    private TextMeshProUGUI _itemPickUpText;
    private HidingTheText _hidingTheText;
    void Start()
    {
        _itemPickUp = GameObject.Find("ItemPickUp");
        _itemPickUp.TryGetComponent(out _hidingTheText);
        _itemPickUpText = _itemPickUp.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
            _itemPickUpText.text = ItemName;
            _itemPickUp.SetActive(true);
            _hidingTheText.HideText(ItemName);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
}