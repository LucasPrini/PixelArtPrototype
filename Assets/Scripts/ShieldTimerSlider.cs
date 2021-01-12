using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShieldTimerSlider : MonoBehaviour
{
    public Slider _slider;

    [SerializeField] float _sliderFillTime;
    PlayerDamager _playerDamager;

    private void Awake()
    {
        _playerDamager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamager>();
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.value = 1;
    }


    private void Update()
    {
        if (_playerDamager.isPlayerShielded)
        {

            _slider.value = Mathf.Lerp(1, 0, (Time.time / _sliderFillTime));

        }

        if (_playerDamager.isPlayerShielded == false)
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }


    }
}
