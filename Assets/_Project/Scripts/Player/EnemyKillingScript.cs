using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EnemyKillingScript : MonoBehaviour
{
    public bool isPlayerInRange = false;
    private int _counter = 0;
    private int _killTarget = 10;
    private GameObject _enemy;
    [field: SerializeField] private GameObject _gameEndText;
    [field: SerializeField] private TextMeshProUGUI _counterText;

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
    _counterText.text = "Enemies killed: " + _counter;
           if(_counter >= _killTarget)
        {
            _gameEndText.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
