using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyKillingScript : MonoBehaviour
{
    public bool isPlayerInRange = false;
    private GameObject _enemy;

    //Find the Enemy GameObject in the scene and assign it to the Enemy variable
    private void Start()
    {
        _enemy = GameObject.Find("Enemy");
    }
    //Checks if the player is in range and if the Enemy GameObject is not null, then destroys the Enemy GameObject then kills it
    private void Update()
    {
        if(isPlayerInRange && _enemy != null)
        {
            Destroy(_enemy);
            Debug.Log("Enemy has been killed.");
        }
    }

    //Checks if player is in the collider range
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isPlayerInRange = true;
        }
    }
    //Left the collider zone, so the player is no longer in range
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isPlayerInRange = false;
        }
    }
}
