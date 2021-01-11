using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpManager : MonoBehaviour
{
    PlayerDamager _playerDamager;
    [SerializeField] GameObject _shieldPowerUp;
    [SerializeField] GameObject _shieldTimerSlider;
    [SerializeField] GameObject _shieldTimerSliderPrefab;

    [SerializeField] GameObject _healthBarCanvas;


    private void Awake()
    {
        _playerDamager = GetComponent<PlayerDamager>();
    }

    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ShieldPickUp")
        {
            Destroy(other.gameObject);
            GameObject shieldPU = Instantiate(_shieldPowerUp, transform.position, Quaternion.identity);
            shieldPU.transform.parent = this.transform;
            Instantiate(_shieldTimerSlider, _healthBarCanvas.transform.position + new Vector3(-400, -240, 0), Quaternion.identity, _healthBarCanvas.transform);
            _playerDamager.isPlayerShielded = true;


        }
    }
}
