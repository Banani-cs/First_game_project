using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyKillingScript : MonoBehaviour
{
    public bool isPlayerInRange = false;
    private int _counter = 0;
    private GameObject _enemy;

    //Checks if the player is in range and if the Enemy GameObject is not null, then destroys the Enemy GameObject then kills it
    private void Update()
    {
        if(isPlayerInRange && _enemy != null)
        {
            Destroy(_enemy);
            Debug.Log("Enemy has been killed.");
            Counter();
        }
    }

    //Checks if player is in the collider range
    private void OnTriggerEnter(Collider other)
    {
        _enemy = other.gameObject;
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
    
    private void Counter()
    {
    _counter++;
    Debug.Log("Counter: " + _counter);
    }
}
