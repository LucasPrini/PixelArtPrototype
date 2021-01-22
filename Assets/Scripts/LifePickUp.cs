using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickUp : MonoBehaviour
{

    bool _isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player" && _isTriggered == false)
        {
            PlayerStats _playerStats = other.GetComponent<PlayerStats>();
            if (_playerStats.currentHealth < _playerStats.maxHealth)
            {
                _playerStats.currentHealth += 1;
                Destroy(this.gameObject);
                _isTriggered = true;
            }

        }
    }
}
