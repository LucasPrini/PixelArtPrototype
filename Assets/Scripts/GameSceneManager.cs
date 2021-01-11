using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject _deathUI;

    private void Awake()
    {
        _deathUI.SetActive(false);
    }

}
