using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
public class Object_Interact : MonoBehaviour
{
    public bool playerInRange;
    [field: SerializeField] public string ItemName { get; private set; }
    GameObject ItemPickUp;
    TextMeshProUGUI ItemPickUpText;
    HidingTheText hidingTheText;
    void Start()
    {
        ItemPickUp = GameObject.Find("ItemPickUp");
        ItemPickUp.TryGetComponent(out hidingTheText);
        ItemPickUpText = ItemPickUp.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            ItemPickUpText.text = ItemName;
            ItemPickUp.SetActive(true);
            hidingTheText.HideText(ItemName);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
