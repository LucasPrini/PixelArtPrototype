using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    public void UpgradeCritDamage()
    {
        transform.parent.gameObject.SetActive(false);
        PauseMenuManager.ResumeGame();
    }

    public void UpgradeMaxResources()
    {
        transform.parent.gameObject.SetActive(false);
        PauseMenuManager.ResumeGame();
    }
}
