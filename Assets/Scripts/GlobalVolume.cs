using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GlobalVolume : MonoBehaviour
{
    Slider _volumeSlider;
    [SerializeField] Toggle _muteToggle;

    private void Awake()
    {
        _volumeSlider = GetComponent<Slider>();

    }

    // Start is called before the first frame update
    void Start()
    {
        _muteToggle.isOn = false;
        _volumeSlider.value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_muteToggle.isOn)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = _volumeSlider.value;
        }

    }
}
