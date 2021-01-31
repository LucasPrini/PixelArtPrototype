using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    [SerializeField] GameObject _creditsCanvas;
    // Start is called before the first frame update
    void Start()
    {
        _creditsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _creditsCanvas.SetActive(false);
        }
    }

    public void OpenCredits()
    {
        _creditsCanvas.SetActive(true);
    }
}
