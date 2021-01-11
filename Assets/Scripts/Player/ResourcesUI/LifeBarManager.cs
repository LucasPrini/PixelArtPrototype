using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LifeBarManager : MonoBehaviour
{
    public GameObject[] _lifeBarUnit;
    GameObject _playerOB;
    GameSceneManager _gameSceneManager;
    Image[] _lifeBarUnitImage;
    PlayerStats _playerStats;

    private void Awake()
    {
        _playerOB = GameObject.FindGameObjectWithTag("Player");
        _gameSceneManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        _lifeBarUnitImage = new Image[_lifeBarUnit.Length];
        for (int i = 0; i < _lifeBarUnit.Length; i++)
        {
            Debug.Log("got " + i);
            _lifeBarUnitImage[i] = _lifeBarUnit[i].GetComponent<Image>();

        }
    }


    private void Update()
    {
        switch (_playerStats.currentHealth)
        {
            case 4:

                break;

            case 3:

                _lifeBarUnitImage[3].CrossFadeAlpha(0, 0.5f, false);
                break;

            case 2:

                _lifeBarUnitImage[2].CrossFadeAlpha(0, 0.5f, false);
                _lifeBarUnitImage[1].color = Color.yellow;
                _lifeBarUnitImage[0].color = Color.yellow;
                break;

            case 1:

                _lifeBarUnitImage[1].CrossFadeAlpha(0, 0.5f, false);
                _lifeBarUnitImage[0].color = Color.red;
                break;

            case 0:
                _lifeBarUnitImage[0].CrossFadeAlpha(0, 0.5f, false);
                _playerOB.SetActive(false);
                _gameSceneManager._deathUI.SetActive(true);

                break;
        }
    }
}
