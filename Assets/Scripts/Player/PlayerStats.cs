using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerStats : GenericStats
{
    public float dashForce;
    public float startingCritMultiplier;
    public float critMultiplier;
    [SerializeField] GameObject _critMultiUIOB;
    TextMeshProUGUI _critMultiUIText;
    float _sceneIndex;

    private void Awake()
    {
        _critMultiUIText = _critMultiUIOB.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (_sceneIndex == 1)
        {
            critMultiplier = 2;
        }
        else
        {
            critMultiplier = PlayerPrefs.GetFloat("CritMulti");
        }

        currentHealth = maxHealth;
        //critMultiplier = PlayerPrefs.GetFloat("CritMulti") + startingCritMultiplier;

    }

    private void Update()
    {
        PlayerPrefs.SetFloat("CritMulti", critMultiplier);
        _critMultiUIText.SetText("CritMultiplier: " + critMultiplier + "x");
    }
}
