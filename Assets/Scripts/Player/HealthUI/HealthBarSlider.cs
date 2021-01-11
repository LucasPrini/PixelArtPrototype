using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarSlider : MonoBehaviour
{
    PlayerStats _playerStats;
    Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        _slider.maxValue = _playerStats.maxHealth;

        _slider.value = _playerStats.currentHealth;
    }




}
