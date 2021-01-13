using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
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
        currentResources = maxResources;
    }

    private void Update()
    {
        _resourcesBarText.SetText(currentResources + "/" + maxResources);
    }
}
