using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    PlayerStats _playerStats;
    PlayerResources _playerResources;

    private void Awake()
    {
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        _playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
    }
    public void UpgradeCritDamage()
    {
        _playerStats.critMultiplier += 0.5f;
        transform.parent.gameObject.SetActive(false);
        PauseMenuManager.ResumeGame();
    }

    public void UpgradeMaxResources()
    {
        _playerResources.maxResources += 30;
        _playerResources.currentResources += 30;
        transform.parent.gameObject.SetActive(false);
        PauseMenuManager.ResumeGame();
    }
}
