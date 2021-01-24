using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : GenericStats
{
    public float dashForce;
    public float startingCritMultiplier;
    public float critMultiplier;
    [SerializeField] GameObject _critMultiUIOB;
    TextMeshProUGUI _critMultiUIText;

    private void Awake()
    {
        _critMultiUIText = _critMultiUIOB.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        critMultiplier = PlayerPrefs.GetFloat("CritMulti") + startingCritMultiplier;
        //critMultiplier = 2;
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("CritMulti", critMultiplier);
        _critMultiUIText.SetText("CritMultiplier: " + critMultiplier + "x");
    }
}
