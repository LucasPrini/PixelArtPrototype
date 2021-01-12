using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerPowerUpManager : MonoBehaviour
{
    PlayerDamager _playerDamager;
    [SerializeField] GameObject _shieldPowerUp;
    [SerializeField] GameObject _shieldTimerSlider;
    [SerializeField] GameObject _upgradeCanvas;
    ShieldTimerSlider _shieldTimerScript;


    [SerializeField] GameObject _healthBarCanvas;


    private void Awake()
    {
        _playerDamager = GetComponent<PlayerDamager>();
        _shieldTimerScript = _shieldTimerSlider.GetComponent<ShieldTimerSlider>();
    }

    private void Update()
    {

    }

    bool _triggeredPickup = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ShieldPickUp" && _triggeredPickup == false)
        {
            Debug.Log("trigered");
            Destroy(other.gameObject);
            GameObject shieldPU = Instantiate(_shieldPowerUp, transform.position, Quaternion.identity);
            shieldPU.transform.parent = this.transform;
            //Instantiate(_shieldTimerSlider, Vector3.zero + new Vector3(285f, 0, 0), Quaternion.identity, _healthBarCanvas.transform);
            _playerDamager.isPlayerShielded = true;
            _shieldTimerSlider.SetActive(true);
            _shieldTimerScript._slider.value = 1;

            _triggeredPickup = true;


        }

        if (other.tag == "BatteryUpgrade")
        {
            _upgradeCanvas.SetActive(true);
            PauseMenuManager.PauseGame();
            Destroy(other.gameObject);
        }
    }
}
