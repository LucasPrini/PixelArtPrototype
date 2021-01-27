using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    float _sceneIndex;
    public float startingMaxResources;
    public float maxResources;
    public float currentResources;

    [SerializeField] GameObject _resourcesBar;
    TextMeshProUGUI _resourcesBarText;

    private void Awake()
    {
        _resourcesBarText = _resourcesBar.GetComponent<TextMeshProUGUI>();
    }



    private void Start()
    {

        _sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (_sceneIndex == 1)
        {

        }
        else
        {
            maxResources = PlayerPrefs.GetFloat("maxResources");
        }
        //maxResources = PlayerPrefs.GetFloat("maxResources") + startingMaxResources;
        currentResources = maxResources;
    }

    private void Update()
    {
        _resourcesBarText.SetText(currentResources + "/" + maxResources);
        PlayerPrefs.SetFloat("maxResources", maxResources);
    }
}
