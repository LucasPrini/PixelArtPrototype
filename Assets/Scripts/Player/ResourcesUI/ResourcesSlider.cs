using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ResourcesSlider : MonoBehaviour
{
    public Slider _slider;
    PlayerResources _playerResources;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
    }


    private void Update()
    {
        _slider.maxValue = _playerResources.maxResources;
        ConsumeResourcesBar();
    }

    void ConsumeResourcesBar()
    {
        _slider.value = _playerResources.currentResources;
    }
}
